using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkeletonMovement : MonoBehaviour
{
    [SerializeField] private float moveSpeed;
    [SerializeField] private float walkSpeed;
    [SerializeField] private float runSpeed;
    [SerializeField] private float rotationSpeed;
    [SerializeField] private float jumpForce;
    float speedMultiplier = 1;

    private Vector3 moveDirection;
    private Vector3 velocity;
    RaycastHit hit;

    [SerializeField] private bool isGrounded;
    [SerializeField] private float groundCheckDistance;
    [SerializeField] private LayerMask groundMask;
    [SerializeField] private float gravity;
    [SerializeField] private Transform cameraTransform;

    [SerializeField] private float jumpHeight;
    float jumpMultiplier = 1;

    private CharacterController controller;
    private Animator animator;
    private Rigidbody skeleton;
    public Collider swordCollider;
    float hitGracePeriod = 1.0f;
    bool canTakeDamage = true;
   
    float superSpeedTime = 20;
    float superJumpTime = 10;

    ParticleSystem speedParticles;
    ParticleSystem jumpParticles;

    Coroutine speedTimer;
    Coroutine jumpTimer;

    //public GameObject sword;
    //private Collider sword_collider;

    bool m_jump;
    bool m_attack;


    private void OnCollisionEnter(Collision c)
    {
        if ((c.gameObject.tag == "BoneSpider" || c.gameObject.tag == "HeartSpider"  )&& canTakeDamage == true)
        {
            canTakeDamage = false;
            StartCoroutine(GracePeriod());
            PlayerHealth player_health = GetComponent<PlayerHealth>();
            player_health.TakeDamage(1);
           
        }
    }

    
    

    // Start is called before the first frame update
    void Start()
    {
        controller = GetComponent<CharacterController>();
        animator = GetComponent<Animator>();
        skeleton = GetComponent<Rigidbody>();
        speedParticles = transform.Find("Speed Boost Particles").GetComponent<ParticleSystem>();
        jumpParticles = transform.Find("Super Jump Particles").GetComponent<ParticleSystem>();
        m_jump = false;
        m_attack = false;

        //sword_collider = sword.GetComponent<Collider>();

    }

    // Update is called once per frame
    void Update()
    {
        if (Memory.isPause) return;

        
        Move();

       
        //if (Input.GetKeyDown(KeyCode.Mouse0))
        //{
        //    attack_1();
        //}
        

        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            m_attack = true;
           
        }
        else
        {
            m_attack = false;
        }


        if (m_attack == false)
        {
            animator.SetBool("isAttacking", false);
        }

        if (m_attack == true)
        {
            combo_attack();
        }

    }

    private void Move()
    {
        Ray ray = new Ray(transform.position, -Vector3.up);

        Debug.DrawRay(transform.position, Vector3.down * jumpHeight, Color.red);

        isGrounded = Physics.CheckSphere(transform.position, groundCheckDistance, groundMask);

        //isGrounded = true;

        float moveZ = Input.GetAxis("Vertical");
        float moveX = Input.GetAxis("Horizontal");

        moveDirection = new Vector3(moveX, 0, moveZ);
        //moveDirection = transform.TransformDirection(moveDirection);
        moveDirection = Quaternion.AngleAxis(cameraTransform.rotation.eulerAngles.y, Vector3.up) * moveDirection;
        moveDirection.Normalize();

        if (isGrounded)
        {
            if (moveDirection != Vector3.zero && !Input.GetKey(KeyCode.LeftShift))
            {
                run();
             
            }
            else if (moveDirection != Vector3.zero && Input.GetKey(KeyCode.LeftShift))
            {
                walk();
            
            }
            else if (moveDirection == Vector3.zero)
            {
                idle();
                
            }

            moveDirection *= moveSpeed*speedMultiplier;
           

        }


        if (Input.GetKeyDown(KeyCode.Space) && isGrounded)
        {
            m_jump = true;
            
        }
        else
        {
            m_jump = false;
        }

        if (m_jump == false)
        {
            animator.SetBool("isJumping", false);
        }

        if (m_jump== true)
        {
            jump();
        }


        if (isGrounded && Input.GetKeyDown(KeyCode.Space) && Input.GetKey(KeyCode.W) && moveDirection != Vector3.zero)
        {
            m_jump = true;
        }
        else
        {
            m_jump = false;
            
        }

        if (m_jump == false)
        {
            animator.SetBool("isJumping", false);
        }

        if (m_jump == true)
        {
            jump_forward();
        }

        controller.Move(moveDirection * Time.deltaTime);

        if (moveDirection != Vector3.zero)
        {
            Quaternion toRotation = Quaternion.LookRotation(moveDirection, Vector3.up);

            transform.rotation = Quaternion.RotateTowards(transform.rotation, toRotation, rotationSpeed * Time.deltaTime);
        }


        velocity.y += gravity * Time.deltaTime;
        controller.Move(velocity * Time.deltaTime);
    }


    private void walk()
    {
        moveSpeed = walkSpeed;
        animator.SetFloat("speed", 0.75f, 0.15f, Time.deltaTime);
        animator.speed = speedMultiplier;
    }

    private void idle()
    {
        animator.SetFloat("speed", 0, 0.15f, Time.deltaTime);
    }

    private void run()
    {
        moveSpeed = runSpeed;
        animator.SetFloat("speed", 1,0.15f,Time.deltaTime);
        animator.speed = speedMultiplier;
  
    }

    private void jump() 
    {
        float z = velocity.z;
        velocity.y = Mathf.Sqrt(jumpHeight * -2 * gravity *jumpMultiplier);
        skeleton.AddForce(new Vector3(0,jumpForce,0),ForceMode.Impulse);
        //animator.SetTrigger("jump");
        animator.SetBool("isJumping", true);

    }

    private void jump_forward()
    {   
        if (isGrounded)
        {
            //velocity = new Vector3(0, Mathf.Sqrt(jumpHeight * -2 * gravity), Mathf.Sqrt(jumpHeight * -2 * gravity));
            velocity = transform.TransformDirection(velocity);
        }
        
    }

    private void combo_attack()
    {
        animator.SetBool("isAttacking", true);
        //animator.SetTrigger("attack");
    }

    private void kick()
    {
        animator.SetTrigger("Kick");
    }

    public void dead() 
    {
        
        animator.SetTrigger("dead");
    }
    public void ColliderOn()
    {
        swordCollider.enabled = true;
    }
    public void ColliderOff()
    {
        swordCollider.enabled = false;
    }
    IEnumerator GracePeriod()
    {
        yield return new WaitForSeconds(hitGracePeriod);
        canTakeDamage = true;

    }
    public void SetSuperSpeed()
    {
        speedMultiplier = 2;
        speedParticles.Play();
        if (speedTimer != null)
        {
            StopCoroutine(speedTimer);
        }
        speedTimer = StartCoroutine(SpeedTimer());
    }
    public void SetSuperJump()
    {
        jumpMultiplier = 4;
        jumpParticles.Play();
        if (jumpTimer != null)
        {
            StopCoroutine(jumpTimer);
        }
        jumpTimer = StartCoroutine(JumpTimer());
    }
    void SuperSpeedOff()
    {
        speedMultiplier = 1;
        speedParticles.Stop();
    }
    void SuperJumpOff()
    {
        jumpMultiplier = 1;
        jumpParticles.Stop();
    }
    IEnumerator SpeedTimer()
    {
        yield return new WaitForSeconds(superSpeedTime);
        SuperSpeedOff();
    }
    IEnumerator JumpTimer()
    {
        yield return new WaitForSeconds(superJumpTime);
        SuperJumpOff();
    }
}
