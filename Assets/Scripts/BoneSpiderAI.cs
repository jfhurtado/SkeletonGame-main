using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BoneSpiderAI : MonoBehaviour
{

    Rigidbody body;

    private Animator spiderAnimator;
    UnityEngine.AI.NavMeshAgent navMesh;
    public Transform waypointsParent;
    List<Transform> waypoints = new List<Transform>();
    int currWaypoint;
    float deathTimer;
    float attackTimer;
    bool moving;

    bool attactStart = false;
    public float runSpeed;
    public float animSpeed;

    private bool firstPlayerContact = true;

    private const string DIE = "die";
    private const string ATTACK = "attack";
    private const string WALK = "walk";
    private const string JUMP = "jump";
    private const string IDLE = "Idle";

    public string direction = "clockwise";
    public  string CLOCKWISE = "clockwise";
    public bool isDead = false;
    string id;
    Transform player;

    private void Awake()
    {
        id = SceneManager.GetActiveScene().name + "Spider" + transform.position;
        if (Memory.enemyMemory.Contains(id))
        {
            gameObject.SetActive(false);
        }
    }


    private void Start()
    {
        navMesh = GetComponent<UnityEngine.AI.NavMeshAgent>();
        spiderAnimator = GetComponent<Animator>();        

        currWaypoint = 0;
        player = GameObject.FindGameObjectWithTag("Player").transform;
        body = GetComponent<Rigidbody>();
    }

    void OnCollisionEnter(Collision collision)
    {
        if (!isDead && collision.gameObject.tag == "Player" && !attactStart && (collision.collider.GetType() is CapsuleCollider))
        {           
                attactStart = true;
                Debug.Log("Inside BoneSpiderAi : reducing player health ");                
        }
    }

    void OnCollisionExit(Collision collision)
    {
        if(collision.gameObject.tag == "Player" )
        {
            attactStart = false;
           // updateAnimation(IDLE, true);
        }
    }

    void OnTriggerStay(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            //Debug.Log(" bone spider player is staying  ");
            moving = true;
            spiderAnimator.speed = 1;
           // updateAnimation(WALK, true);
        }
    }
    void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            //Debug.Log(" bone spider detected player moving out of trigger  ");
            moving = false;
            spiderAnimator.speed = 1;            
            
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if(!isDead && other.gameObject.tag == "Player")
        {
            //Debug.Log("Player detected near bone spider ");
            moving = true;
            spiderAnimator.speed = animSpeed;
            navMesh.speed = runSpeed;
            updateAnimation(WALK, true);          
        }
    }

    void Update()
    {
        // do anything if spider is not dead 
        if (!isDead  )
        {
            if (!Memory.isPause && spiderAnimator.GetBool(WALK))
            {
                    walk();
            }

        }
        else
        {
            deathTimer += Time.deltaTime;
            this.GetComponent<BoxCollider>().enabled = false;
            if (deathTimer >= 5)
            {
                gameObject.SetActive(false);
                //Destroy(this.gameObject);
            }
        }
    }

    void walk()
    {
        if (navMesh != null)
        {
            //this.transform.position = this.transform.position + new Vector3(-0.01f , 0, -0.01f );

            // rbody.MoveRotation(rbody.rotation * Quaternion.AngleAxis(inputTurn * Time.deltaTime * turnMaxSpeed, Vector3.up));
            if (navMesh.pathPending != true && navMesh.remainingDistance <= 1)
            {
                if(direction == CLOCKWISE)
                {
                    moveClockWise();
                }
                else
                {
                    moveAntiClockWise();
                }
                
            }
            else if(!moving)
            {
                updateAnimation(IDLE, true);
            }
        }

    }


    private void setNextWaypoint()
    {
        Debug.Log($"next way point {(currWaypoint + 1) % waypointsParent.childCount}");
        int next = (currWaypoint + 1) % waypointsParent.childCount;
        int previous = (currWaypoint + waypointsParent.childCount - 1) % waypointsParent.childCount;

        if(firstPlayerContact)
        {
            next = 0;
            previous = waypointsParent.childCount - 1;
            firstPlayerContact = false;
        }
        
        Transform candidate1 = waypointsParent.GetChild(next);
        Transform candidate2 = waypointsParent.GetChild(previous);
        float distance1 = (candidate1.position - player.position).magnitude;
        float distance2 = (candidate2.position - player.position).magnitude;

        /*
        RaycastHit hit1;
        var blocked1 = Physics.Raycast(this.transform.position, candidate1.position, out hit1, UnityEngine.AI.NavMesh.AllAreas);

        RaycastHit hit2;
        var blocked2 = Physics.Raycast(this.transform.position, candidate2.position, out hit2, UnityEngine.AI.NavMesh.AllAreas); 
        if(blocked1 && hit1.collider.tag == "Player")
        {
            navMesh.SetDestination(candidate2.position);
            currWaypoint = previous;
        }
        else if(blocked2 && hit2.collider.tag == "Player")
        {
            navMesh.SetDestination(candidate1.position);
            currWaypoint = next;
        }// no point is blocked by player 
        else */
        if (distance1 >= distance2 )
        {
            navMesh.SetDestination(candidate1.position);
            currWaypoint = next;
            Debug.Log($"next {next} , previous : {previous} , selecting wayPoint {currWaypoint} , distance 1 from next : {distance1} , distance 2 from previous {distance2}. Selecting {distance1}");
        }
        else
        {
            navMesh.SetDestination(candidate2.position);
            currWaypoint = previous;
            Debug.Log($"next {next} , previous : {previous} ,selecting wayPoint {currWaypoint} , distance 1 from next : {distance1} , distance 2 from previous {distance2}. Selecting {distance2}");
        }
    }

    public void moveClockWise()
    {
        int next = (currWaypoint + 1) % waypointsParent.childCount;
        
        if (firstPlayerContact)
        {
            next = 0;            
            firstPlayerContact = false;
        }
        Debug.Log($"In clockwise ,moving from {currWaypoint} to  next point {next}");
        
        currWaypoint = next;
        Transform candidate1 = waypointsParent.GetChild(currWaypoint);
        navMesh.SetDestination(candidate1.position);       
    }


    public void moveAntiClockWise()
    {
       int previous = (currWaypoint + waypointsParent.childCount - 1) % waypointsParent.childCount;

        if (firstPlayerContact)
        {            
            previous = waypointsParent.childCount - 1;
            firstPlayerContact = false;
        }
       
        
        Debug.Log($"In anticlockwise ,moving from {currWaypoint} to previous point {previous}");
        currWaypoint = previous;
        Transform candidate1 = waypointsParent.GetChild(currWaypoint);        
        navMesh.SetDestination(candidate1.position);
    }


    public void receiveAttackFromSkeleton(string dangerLevel)
    {
        EnemyAudioManager enemyAudioManager = GetComponent<EnemyAudioManager>();
        enemyAudioManager.PlayPain();

        stopAllAnimation();
        isDead = true;
        spiderAnimator.speed = 1;
        this.GetComponent<BoxCollider>().enabled = false;
        updateAnimation(DIE, true);
        
        Memory.enemyMemory.Add(id);
        Debug.Log("bone spider ai , checking bone ");
        GameObject bone = transform.Find("Bone").gameObject;
        bone.transform.SetParent(null);
        bone.GetComponent<Collider>().enabled = true;

    }

    /*stop any other animation */
    public void stopAllAnimation()
    {
        spiderAnimator.SetBool(IDLE, false);
        spiderAnimator.SetBool(WALK, false);
        spiderAnimator.SetBool(JUMP, false);
        spiderAnimator.SetBool(DIE, false);
        spiderAnimator.SetBool(ATTACK, false);
    }

    public void stopNavMeshAgent(bool isStopped)
    {
        if (navMesh != null)
        {
            navMesh.isStopped = isStopped;
        }
    }

    public void updateAnimation(string animation, bool valueToUpdate)
    {
        stopAllAnimation();
        switch (animation)
        {
            case WALK:
                //stopAllAnimation();
                if (valueToUpdate)
                {
                    stopNavMeshAgent(false);

                    spiderAnimator.SetBool(WALK, valueToUpdate);
                }
                else
                {
                    stopNavMeshAgent(true);

                    spiderAnimator.SetBool(WALK, valueToUpdate);
                }
                break;
            case IDLE:
                //stopAllAnimation();
                if (valueToUpdate)
                {
                    stopNavMeshAgent(true);
                    spiderAnimator.SetBool(IDLE, valueToUpdate);
                }
                else
                {
                    spiderAnimator.SetBool(IDLE, valueToUpdate);
                }
                break;
            case DIE:
                //stopAllAnimation();
                if (valueToUpdate)
                {
                    stopNavMeshAgent(true);
                    spiderAnimator.SetBool(DIE, valueToUpdate);
                }
                else
                {
                    spiderAnimator.SetBool(IDLE, valueToUpdate);
                }

                break;
            case ATTACK:
                //stopAllAnimation();
                if (valueToUpdate)
                {
                    stopNavMeshAgent(true);
                    spiderAnimator.SetBool(ATTACK, valueToUpdate);
                }
                else
                {
                    spiderAnimator.SetBool(ATTACK, valueToUpdate);
                }
                break;
            case JUMP:
                //stopAllAnimation();
                if (valueToUpdate)
                {
                    stopNavMeshAgent(true);
                    spiderAnimator.SetBool(JUMP, valueToUpdate);
                }
                else
                {
                    spiderAnimator.SetBool(JUMP, valueToUpdate);
                }
                break;
        }
    }

}
