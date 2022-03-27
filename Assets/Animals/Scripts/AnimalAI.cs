using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class AnimalAI : MonoBehaviour
{
    [Header("Icon")]
    public Sprite iconAnimal;

    [Header("Movement")]
    public float normalSpeed = 1f;
    public float nearByPlayerSpeed = 2f;

    [Header("Wander")]
    public bool inWanderArea = true;
    public float minRandomWanderTime;
    public float maxRandomWanderTime;

    public WanderArea wanderArea;
    public Vector3 wanderTarget;

    private float wanderDistance = 2, wanderJitter = 2, wanderRadius = 2;

    [Header("Run Away")]
    public bool nearByPlayer;
    public float checkRadius = 3f;

    [Header("On Holdiing")]
    public bool onHolding;

    [Header("Declare Component")]
    public GameObject playerNearBy;
    [HideInInspector] public Animator anim;
    [HideInInspector] public Collider col;

    [HideInInspector] public NavMeshAgent navMeshAgent;

    [HideInInspector] public IAnimalState _wanderState, _runAwayState, _onHoldingState;
    [HideInInspector] public AnimalStateContext _animalStateContext;
    void Awake()
    {
        navMeshAgent = GetComponent<NavMeshAgent>();
        anim = GetComponent<Animator>();
        col = GetComponent<Collider>();

        _animalStateContext = new AnimalStateContext(this);
    }
    void Start()
    {

        _wanderState = gameObject.AddComponent<AnimalWanderState>();
        _runAwayState = gameObject.AddComponent<AnimalRunAwayState>();
        _onHoldingState = gameObject.AddComponent<AnimalOnHoldingState>();

        anim.SetFloat("CycleOffset", Random.Range(0f, 1f));

        _animalStateContext.Transition(_wanderState);
    }

    void OnEnable()
    {
        //Find Wander Area
        GameObject[] allWander;
        allWander = GameObject.FindGameObjectsWithTag("WanderArea");

        GameObject wanderAreaObject = null;

        float distance = 1000f;
        for (int i = 0; i < allWander.Length; i++)
        {
            float wanderDistance = Vector3.Distance(transform.position, allWander[i].transform.position);
            if (wanderDistance <= distance)
            {
                wanderAreaObject = allWander[i];
                distance = wanderDistance;
            }
        }

        wanderArea = wanderAreaObject.GetComponent<WanderArea>();
        inWanderArea = true;

        StartCoroutine(Wander());
    }

    void OnDisable()
    {
        StopAllCoroutines();
    }

    void Update()
    {
        if (!onHolding)
        {
            NearByPlayer();
        }
    }

    public void NearByPlayer()
    {
        Collider[] collidersToPlayer = Physics.OverlapSphere(transform.position, checkRadius);
        foreach (Collider nearbyObject in collidersToPlayer)
        {
            PlayerController playerController = nearbyObject.GetComponent<PlayerController>();

            if (playerController != null && !nearByPlayer)
            {
                playerNearBy = playerController.gameObject;

                _animalStateContext.Transition(_runAwayState);

                return;
            }
        }
    }

    public IEnumerator Wander()
    {
        while (true)
        {
            yield return new WaitForSeconds(0.1f);

            if (!nearByPlayer && !onHolding)
            {
                if (inWanderArea)
                {
                    wanderTarget = wanderArea.gameObject.transform.position
                                   + (new Vector3(Random.Range(-wanderArea.wanderDistance.x, wanderArea.wanderDistance.x),
                                     Random.Range(-wanderArea.wanderDistance.y, wanderArea.wanderDistance.y),
                                     Random.Range(-wanderArea.wanderDistance.z, wanderArea.wanderDistance.z)) / 2);

                    navMeshAgent.SetDestination(wanderTarget);
                }
                else
                {
                    wanderTarget += new Vector3(Random.Range(-1f, 1f) * wanderJitter,
                                                    0f,
                                                    Random.Range(-1f, 1f) * wanderJitter);

                    wanderTarget.Normalize();
                    wanderTarget *= wanderRadius;

                    Vector3 targetLocal = wanderTarget + new Vector3(0, 0, wanderDistance);
                    Vector3 targetWorld = gameObject.transform.TransformVector(targetLocal);

                    Vector3 pos = transform.position + targetWorld;

                    navMeshAgent.SetDestination(pos);
                }

                yield return new WaitForSeconds(Random.Range(minRandomWanderTime, maxRandomWanderTime));
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("WanderArea"))
        {
            inWanderArea = true;
            wanderArea = other.gameObject.GetComponent<WanderArea>();
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("WanderArea"))
        {
            inWanderArea = false;
        }
    }
}
