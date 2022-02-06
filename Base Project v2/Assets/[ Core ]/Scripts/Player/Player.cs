using UnityEngine;

public class Player : MonoBehaviour
{
    [Header("-- COMPONENTS --")]
    internal Animator animator;
    internal Collider coll;
    internal Rigidbody rb;
    
    [Header("-- SCRIPT REFERENCES --")]
    internal PlayerInput playerInput;
    internal PlayerCollision playerCollision;

    [Header("-- MOVEMENT SCRIPT REFERENCES --")]
    internal PlayerRBMovement playerRBMovement;
    internal PlayerChrMovement playerChrMovement;
    internal PlayerAnimMovement playerAnimMovement;

    [Header("-- MOVEMENT SETUP --")]
    [SerializeField] private float maxMovementSpeed = 3f;
    [SerializeField] private float minMovementSpeed = 1f;
    [SerializeField, Range(0.1f, 3f)] private float accelerationRate = 0.5f;
    private float currentMovementSpeed = 1f;
    [SerializeField] private float turnSmoothTime = 0.5f;
    [SerializeField] private float jumpForce = 50f;
    [SerializeField] private float jumpCooldown = 2f;

    [Header("-- GROUNDED SETUP --")]
    [SerializeField, Tooltip("Select layers that you want player to be grounded.")] private LayerMask groundLayerMask;
    [SerializeField, Tooltip("Height that player will be considered grounded when above groundable layers.")] private float groundedHeightLimit = 0.1f;

    #region Properties

    public float CurrentMovementSpeed => currentMovementSpeed;
    public float TurnSmoothTime => turnSmoothTime;
    public float JumpForce => jumpForce;
    public float JumpCooldown => jumpCooldown;
    public Vector3 AirVelocity { get; set; }
    public Vector3 CurrentVelocity => IsGrounded ? rb.velocity : AirVelocity;

    // Controls
    public bool IsControllable => GameManager.GameState == GameState.Started;
    public bool IsMoving => playerInput.InputValue.magnitude > 0.05f;
    public bool IsGrounded => Physics.Raycast(coll.bounds.center, Vector3.down, coll.bounds.extents.y + groundedHeightLimit, groundLayerMask) && !playerInput.JumpPressed;
    public bool CanJump => IsControllable && IsGrounded;

    #endregion

    private void Awake()
    {
        animator = GetComponent<Animator>();
        coll = GetComponent<Collider>();
        TryGetComponent(out rb); // Maybe it uses character controller, not rigidbody.

        playerInput = GetComponent<PlayerInput>();
        playerCollision = GetComponent<PlayerCollision>();

        TryGetComponent(out playerRBMovement);
        TryGetComponent(out playerChrMovement);
        TryGetComponent(out playerAnimMovement);

        currentMovementSpeed = minMovementSpeed;
    }

    private void Start()
    {
        playerCollision.OnHitSomethingBack += () => currentMovementSpeed = minMovementSpeed;
    }

    private void OnDisable()
    {
        playerCollision.OnHitSomethingBack -= () => currentMovementSpeed = minMovementSpeed;
    }

    private void Update()
    {
        if (!IsMoving && IsGrounded) rb.velocity = Vector3.zero;

        if (IsMoving)
            currentMovementSpeed = Mathf.MoveTowards(currentMovementSpeed, maxMovementSpeed, accelerationRate * Time.deltaTime);
        else
            currentMovementSpeed = minMovementSpeed;

        //Debug.Log("Is Moving: " + IsMoving);
        //Debug.Log("Is Grounded: " + IsGrounded);
    }
}
