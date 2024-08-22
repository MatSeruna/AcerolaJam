using System;
using System.Collections;
using UnityEngine;
using UnityEngine.AI;

public class Partygoer : MonoBehaviour
{
    [Header("Stats")]
    [SerializeField] int hp;
    [SerializeField] bool isAlive;
    [SerializeField] float speed;
    [SerializeField] float speedPanic;
    [SerializeField] bool isAgressive;
    [SerializeField] float maxMoveDistanceCalm;
    [SerializeField] float maxMoveDistancePanic;
    [SerializeField] float distanceRun;
    [SerializeField] float distanceExit;
    [SerializeField] float currDistanceToPlayer;
    [SerializeField] float currDistanceToExit;
    public int HP { get { return hp; } }

    public Animator animator;
    NavMeshAgent agent;
    public float minMoveTime;
    public float maxMoveTime;
    public Transform playerTrans;
    public Transform centrePoint;
    public Transform exitTrans;
    public Vector3 destination;
    public AudioSource audioSourceScream;
    public AudioClip[] screams;
    public AudioSource walkSound;

    public delegate void PartygoerDieHandler(Partygoer partygoer);
    public event PartygoerDieHandler OnPartygoerDie;
    public event PartygoerDieHandler OnPartygoerExit;

    public enum State { calm, panic, runningAway, runToExit };
    public State currentState = 0;

    // Start is called before the first frame update
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        //animator = GetComponentInChildren<Animator>();
        centrePoint = transform;
        agent.speed = speed;
        StartCoroutine(MoveEverySeconds());
    }

    // Update is called once per frame
    void Update()
    {
        if (playerTrans != null)        
            currDistanceToPlayer = Vector3.Distance(transform.position, playerTrans.position);           
        
        if (exitTrans != null)
            currDistanceToExit = Vector3.Distance(transform.position, exitTrans.position);


        if (hp > 0)
        {
            isAlive = true;

            RunAway();
            CheckExitIsNear();

            if (currentState == State.panic)
            {
                FindDestination();
            }

            if (currentState == State.calm && !agent.hasPath)
                animator.SetFloat("Speed", 0);
            else
                animator.SetFloat("Speed", agent.speed);

            if (destination != null)
                MoveToTarget(destination);
        }
        else
            isAlive = false;


    }

    public void Init(Transform playerTrans, Transform exitTrans)
    {
        this.playerTrans = playerTrans;
        this.exitTrans = exitTrans;
    }

    IEnumerator MoveEverySeconds()
    {
        while (currentState == State.calm)
        {
            float moveTime = UnityEngine.Random.Range(minMoveTime, maxMoveTime);
            
            FindDestination();
            yield return new WaitForSeconds(moveTime);
        }       
    }

    public void FindDestination()
    {
        if (agent.remainingDistance < 0.1f || !agent.hasPath)
        {
            if (currentState == State.calm)
            {
                if (RandomPoint(transform.position, maxMoveDistanceCalm)) //pass in our centre point and radius of area
                {
                    Debug.DrawRay(destination, Vector3.up, Color.green, 1.0f); //so you can see with gizmos
                }
            }
            else if (currentState == State.panic)
            {
                if (RandomPoint(transform.position, maxMoveDistancePanic)) //pass in our centre point and radius of area
                {
                    Debug.DrawRay(destination, Vector3.up, Color.red, 1.0f); //so you can see with gizmos
                }
            }
        }
    }

    bool RandomPoint(Vector3 center, float distance)
    {
        Vector3 randomPoint = center + UnityEngine.Random.insideUnitSphere * distance; //random point in a sphere 
        NavMeshHit hit;
        if (NavMesh.SamplePosition(randomPoint, out hit, distance, NavMesh.AllAreas)) //documentation: https://docs.unity3d.com/ScriptReference/AI.NavMesh.SamplePosition.html
        {
            //the 1.0f is the max distance from the random point to a point on the navmesh, might want to increase if range is big
            //or add a for loop like in the documentation
            destination = hit.position;
            return true;
        }
        return false;
    }

    void MoveToTarget(Vector3 pos)
    {
        agent.SetDestination(pos);

        
        
        
    }

    void RunAway()
    {
        if (currDistanceToPlayer <= distanceRun && currentState != State.runToExit)
        {
            currentState = State.runningAway;
            Vector3 dirToPlayer = transform.position - playerTrans.position;
            destination = transform.position + dirToPlayer;
            agent.speed = speedPanic;
            audioSourceScream.clip = screams[0];
            //audioSourceScream.Play();
        }
        else if (currDistanceToPlayer > distanceRun && currentState != State.runToExit && currentState == State.runningAway)
        {
            agent.speed = speedPanic;
            currentState = State.panic;
            audioSourceScream.clip = screams[0];
            //audioSourceScream.Play();
        }
        
    }

    void CheckExitIsNear()
    {
        if (currDistanceToExit <= distanceExit)
        {
            currentState = State.runToExit;
            destination = exitTrans.position;
        }
    }

    public void TakeDamage(int damage)
    {
        hp -= Mathf.Max(1, damage);       
        if (hp <= 0)
        {
            int i = UnityEngine.Random.Range(0, screams.Length);
            audioSourceScream.clip = screams[i];
            audioSourceScream.Play();
            hp = 0;
            OnPartygoerDie?.Invoke(this);
            animator.SetBool("isDying", true);
            agent.isStopped = true;          
            Invoke("DestroyEndAnim",3f);
        }
    }

    public void DestroyEndAnim()
    {
        Destroy(gameObject);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Exit")
        {
            OnPartygoerExit?.Invoke(this);
            //OnPartygoerDie?.Invoke(this);
            Destroy(gameObject);
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Partygoer")
        {
            FindDestination();
        }
    }
}
