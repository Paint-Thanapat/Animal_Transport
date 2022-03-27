using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [Header("---- Movement ----")]
    public float speedMultiplierOnHolding = 1f;
    public float jumpMultiplierOnHolding = 1f;

    [Header("Movement")]
    public float normalSpeed = 5;
    public float slowSpeed = 2;
    public float currentSpeed { get; set; }
    public bool isFreeze;
    public float stunDuration;

    [Header("Rotation")]
    public float turnSmoothTime = 0.1f;
    float turnSmoothVelocity;

    [Header("Jump")]
    public bool canJump = true;
    public float jumpHeight = 3;
    public Transform groundCheck;
    public float groundDistance = 0.4f;
    public LayerMask groundMask;
    bool isGrounded;

    [Header("Declare Component Movement")]
    public Transform moveDirection;
    [HideInInspector] public IPlayerMovementState _normalMovementState, _slowMovementState, _stunMovementState, _stopMovementState;
    [HideInInspector] public PlayerStateContext _playerStateContext;
    private Vector3 movementVector;


    //-----------------------------------------------------------

    [Header("---- Interact ----")]
    public Transform holdingTransform;
    public bool isHolding;
    public AnimalAI animalHolder;
    public float pickupDistance = 3f;
    public LayerMask pickupMask;
    [Header("Declare Component Interact")]
    [HideInInspector] public IPlayerInteractState _emptyHandState, _onHoldingState;
    [HideInInspector] public PlayerInteractStateContext _playerInteractStateContext;

    //-------------------------------------------
    [Header("Other")]
    public bool isNearTruck;
    public Transform stunParticlePoint;
    public Rigidbody rb;
    public Animator anim;
    void Awake()
    {
        rb = GetComponent<Rigidbody>();
        anim = GetComponent<Animator>();
    }

    void Start()
    {
        //movement
        _playerStateContext = new PlayerStateContext(this);

        _normalMovementState = gameObject.AddComponent<PlayerNormalMovementState>();
        _slowMovementState = gameObject.AddComponent<PlayerSlowMovementState>();
        _stunMovementState = gameObject.AddComponent<PlayerStunMovementState>();
        _stopMovementState = gameObject.AddComponent<PlayerStopMovementState>();

        _playerStateContext.Transition(_normalMovementState);

        //interact
        _playerInteractStateContext = new PlayerInteractStateContext(this);

        _emptyHandState = gameObject.AddComponent<PlayerEmptyHandState>();
        _onHoldingState = gameObject.AddComponent<PlayerOnHoldingState>();

        _playerInteractStateContext.Transition(_emptyHandState);

    }
    void Update()
    {
        //jump
        isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask);

        if (Input.GetKeyDown(KeyCode.Space) && isGrounded && canJump)
        {
            rb.AddForce(transform.up * jumpHeight * jumpMultiplierOnHolding, ForceMode.VelocityChange);
        }

        //walk
        float horizontal = Input.GetAxisRaw("Horizontal");
        float vertical = Input.GetAxisRaw("Vertical");
        movementVector = new Vector3(horizontal, 0f, vertical).normalized;

        //Pickup Animal
        if (Input.GetMouseButtonDown(0))
        {
            if (!isHolding)
            {
                PickupAnimal();
            }
            else
            {
                DropAnimal();
            }
        }

        //Test
        if (Input.GetKeyDown(KeyCode.F1))
        {
            _playerStateContext.Transition(_normalMovementState);
        }
        if (Input.GetKeyDown(KeyCode.F2))
        {
            _playerStateContext.Transition(_slowMovementState);
        }
        if (Input.GetKeyDown(KeyCode.F3))
        {
            _playerStateContext.Transition(_stunMovementState);
        }
        if (Input.GetKeyDown(KeyCode.F4))
        {
            _playerStateContext.Transition(_stopMovementState);
        }
    }
    void FixedUpdate()
    {
        MoveCharacter();
    }
    void MoveCharacter()
    {
        if (movementVector.magnitude >= 0.1f && !isFreeze)
        {
            float targetAngle = Mathf.Atan2(movementVector.x, movementVector.z) * Mathf.Rad2Deg + moveDirection.eulerAngles.y;
            float angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle, ref turnSmoothVelocity, turnSmoothTime);
            transform.rotation = Quaternion.Euler(0f, angle, 0f);

            Vector3 moveDir = Quaternion.Euler(0f, targetAngle, 0f) * Vector3.forward;

            rb.MovePosition((Vector3)transform.position + (moveDir * currentSpeed * speedMultiplierOnHolding * Time.deltaTime));
        }
    }

    public void PickupAnimal()
    {
        RaycastHit hit;
        if (Physics.Raycast(transform.position + (Vector3.up * 0.1f), transform.forward, out hit, pickupDistance, pickupMask))
        {
            AnimalAI animal = hit.collider.gameObject.GetComponent<AnimalAI>();

            if (animal != null)
            {
                animalHolder = animal;

                animalHolder._animalStateContext.Transition(animalHolder._onHoldingState);
                _playerInteractStateContext.Transition(_onHoldingState);

                PlayerMultiplierOnHolding playerMultiplierOnHolding = animalHolder.gameObject.GetComponent<PlayerMultiplierOnHolding>();

                if (playerMultiplierOnHolding != null)
                {
                    speedMultiplierOnHolding = playerMultiplierOnHolding.speedMultiplierOnHolding;
                    jumpMultiplierOnHolding = playerMultiplierOnHolding.jumpMultiplierOnHolding;
                }
            }
        }
    }

    public void DropAnimal()
    {
        if (animalHolder == null)
            return;

        animalHolder._animalStateContext.Transition(animalHolder._runAwayState);
        _playerInteractStateContext.Transition(_emptyHandState);

        speedMultiplierOnHolding = 1f;
        jumpMultiplierOnHolding = 1f;

        if (isNearTruck)
        {
            GameObject.Find("Mission Controller").GetComponent<MissionController>().CheckSendAnimal(animalHolder.iconAnimal);

            animalHolder.gameObject.GetComponent<AnimalPool>().SentAnimalToTruck();

            ClientObjectPool[] clientObjectPool = GameObject.FindObjectsOfType<ClientObjectPool>();
            for (int i = 0; i < clientObjectPool.Length; i++)
            {
                if (clientObjectPool[i].objectToPool.GetComponent<AnimalAI>().iconAnimal ==
                 animalHolder.gameObject.GetComponent<AnimalAI>().iconAnimal)
                {
                    clientObjectPool[i].Spawn(1);
                    break;
                }
            }
        }

        animalHolder = null;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Mud"))
        {
            _playerStateContext.Transition(_slowMovementState);
        }

        if (other.gameObject.CompareTag("Truck"))
        {
            isNearTruck = true;
        }
    }
    private void OnTriggerExit(Collider other)
    {
        ExitMud(other);

        if (other.gameObject.CompareTag("Truck"))
        {
            isNearTruck = false;
        }
    }

    void ExitMud(Collider other)
    {
        if (other.gameObject.CompareTag("Mud"))
        {
            Collider[] collidersToMud = Physics.OverlapSphere(groundCheck.position, groundDistance, groundMask);
            foreach (Collider nearbyObject in collidersToMud)
            {
                if (nearbyObject.CompareTag("Mud"))
                {
                    _playerStateContext.Transition(_slowMovementState);
                    return;
                }
            }
            _playerStateContext.Transition(_normalMovementState);
        }
    }
}
