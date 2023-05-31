
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

[RequireComponent(typeof(UnityEngine.AI.NavMeshAgent))]
public class SpiderAnimationController : MonoBehaviour
{
    public Rigidbody body;
    public GameObject wayPointPrefab;
    public GameObject heartPrefab;
    public GameObject waypointsParent;

    public bool releasedHeart = false;
    bool attactStart = false;
    float attackTimer;

    private Animator spiderAnimator;
    UnityEngine.AI.NavMeshAgent navMesh;
    //public List<GameObject> waypoints = new List<GameObject>();
    int currWaypoint;
    public int numObjects = 5;
    System.Random r = new System.Random(4567);
    Material transparentMaterial;
    public string spriderType;

    public const string DIE = "die";
    public const string ATTACK = "attack";
    public const string WALK = "walk";
    public const string JUMP = "jump";
    public const string IDLE = "Idle";

    //public bool createWayPointOnRuntime = true;
    public bool isDead = false;
    string id;
    float timer = 0.0f;    

    public bool chasingPlayer = false;
    public GameObject playerReference;

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
        updateAnimation(WALK, true);

        transparentMaterial = Resources.Load<Material>("TransparentMaterial");
        currWaypoint = 0;

        if (waypointsParent == null )
            addWayPoint();

        if (navMesh != null)
            setNextWaypoint();
    }

    private void addWayPoint()
    {
        waypointsParent = new GameObject("WayPointParent");
       // waypoints = new List<GameObject>();

        var center = this.transform.position;
        if (numObjects > 5)
            numObjects = 5;

        float[] angles = { 0f, 90f, 180f, 270f, 315f };
        for (int i = 0; i < numObjects; i++)
        {
            Vector3 pos = RandomCircle(center, 10, angles[i]);

            UnityEngine.AI.NavMeshHit hit;
            var blocked = UnityEngine.AI.NavMesh.Raycast(this.transform.position, pos, out hit, UnityEngine.AI.NavMesh.AllAreas);
            if (blocked)
            {
                pos = hit.position;

                if(hit.position.x > center.x)
                {
                    pos = pos + new Vector3(-2, 0, 0);
                }
                else
                {
                    pos = pos + new Vector3(2, 0, 0);
                }

                if (hit.position.z > center.z)
                {
                    pos = pos + new Vector3(0, 0, -2);
                }
                else
                {
                    pos = pos + new Vector3(0, 0, 2);
                }
            }

            GameObject child =  ((GameObject)Instantiate(wayPointPrefab, pos, Quaternion.identity));
            child.transform.parent = waypointsParent.transform;
        }
    }

    Vector3 RandomCircle(Vector3 center, float radius, float ang)
    {
        //float ang = Random.value * 360;
        Vector3 pos;
        pos.x = center.x + radius * Mathf.Sin(ang * Mathf.Deg2Rad);
        pos.y = center.y + 0.5f;// + radius * Mathf.Cos(ang * Mathf.Deg2Rad);
        pos.z = center.z + radius * Mathf.Cos(ang * Mathf.Deg2Rad);
        return pos;
    }

    void OnTriggerEnter(Collider collider)
    {
        if (!isDead && collider.attachedRigidbody != null && collider.gameObject.tag == "Player")
        {
            playerReference = collider.gameObject;
            //Debug.Log("changing spider destination to player position ");
            chasingPlayer = true;
            navMesh.SetDestination(collider.gameObject.transform.position);

            EnemyAudioManager enemyAudioManager = GetComponent<EnemyAudioManager>();
            enemyAudioManager.PlayChaseAlert();
        }
    }


    void OnCollisionEnter(Collision collision)
    {
        if (!isDead && collision.gameObject.tag == "Player" && !attactStart)
        {
            attactStart = true;
            //PlayerHealth health = collision.gameObject.GetComponent<PlayerHealth>();
            //health.TakeDamage(1);
        }
    }

    void OnTriggerStay(Collider collider)
    {
        if (!isDead && collider.gameObject.tag == "Player")
        {            
            chasingPlayer = true;
            navMesh.SetDestination(collider.gameObject.transform.position);
        }
    }

    void OnTriggerExit(Collider collider)
    {
        if (!isDead)
        {
            chasingPlayer = false;
            updateAnimation(WALK, true);
        }
    }

    void Update()
    {
        timer += Time.deltaTime;
        // do anything if spider is not dead 
        if (!isDead)
        {
            if (!Memory.isPause && spiderAnimator.GetBool(WALK))
            {
                if (navMesh != null)
                {
                    walk();
                }
            }
        }
        else
        {
            int seconds = (int)(timer % 60);
            if (seconds >= 3)
            {
                gameObject.SetActive(false);
                //Destroy(this.gameObject);
            }
        }

        if (attactStart)
        {
            attackTimer += Time.deltaTime;
            if (attackTimer >= 10)
            {
                attactStart = false;
                attackTimer = 0f;
            }
        }

    }

    void walk()
    {
        if (navMesh != null)
        {
            //this.transform.position = this.transform.position + new Vector3(-0.01f, 0, -0.01f);
            if (navMesh.pathPending != true && navMesh.remainingDistance <= 2)
            {
                setNextWaypoint();
            }
        }
    }


    public void setNextWaypoint()
    {
        if (chasingPlayer)
        {
            navMesh.SetDestination(playerReference.transform.position);
        }
        else
        {
            if (currWaypoint >= waypointsParent.transform.childCount)
            {
                currWaypoint = 0;
            }

            if (waypointsParent != null)
            {
                navMesh.SetDestination((waypointsParent.transform.GetChild(currWaypoint).position));
            }

            currWaypoint++;
        }

    }

    int strikeCount = 0;

    /* level1 for small attack and it will reduce player health by 1 
    spider should only flinch 

    level2 - spider die and it will reduce player health by 2 
    */
    public void receiveAttackFromSkeleton(string dangerLevel)
    {
        EnemyAudioManager enemyAudioManager = GetComponent<EnemyAudioManager>();
        enemyAudioManager.PlayPain();
        if (dangerLevel == "level1")
        {
            stopAllAnimation();
            updateAnimation(JUMP, true);
        }
        else if (dangerLevel == "level2")
        {
            stopAllAnimation();
            isDead = true;
            updateAnimation(DIE, true);
            Memory.enemyMemory.Add(id);
            timer = 0.0f;
            this.GetComponent<BoxCollider>().enabled = false;
            if (spriderType == "heart" && !releasedHeart)
            {
                Vector3 heartPosition = new Vector3(this.transform.position.x, this.transform.position.y+ 1f, this.transform.position.z);
                Instantiate(heartPrefab, heartPosition, Quaternion.Euler(new Vector3(-90, 0, 0)));
                releasedHeart = true;
            }
        }
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
