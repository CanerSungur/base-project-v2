using UnityEngine;

public class Player : MonoBehaviour
{
    [Header("-- COMPONENTS --")]
    internal Animator animator;
    internal Collider coll;
    internal Rigidbody rb;

    [Header("-- SCRIPT REFERENCES --")]
    internal PlayerInput playerInput;
    internal PlayerMovement playerMovement;
    internal PhysicsHover hover;

    [Header("-- MOVEMENT SETUP --")]
    [SerializeField] private float movementSpeed = 1f;
    [SerializeField] private float turnSmoothTime = 0.5f;
    [SerializeField] private float jumpForce = 50f;
    [SerializeField] private float jumpCooldown = 2f;

    [Header("-- GROUNDED SETUP --")]
    [SerializeField, Tooltip("Select layers that you want player to be grounded.")] private LayerMask groundLayerMask;
    [SerializeField, Tooltip("Height that player will be considered grounded when above groundable layers.")] private float groundedHeightLimit = 0.75f;

    #region Properties

    public float MovementSpeed { get { return movementSpeed; } }
    public float TurnSmoothTime { get { return turnSmoothTime; } }
    public float JumpForce { get { return jumpForce; } }
    public float JumpCooldown { get { return jumpCooldown; } }

    // Controls
    public bool IsControllable { get { return GameManager.GameState == GameState.Started; } }
    public bool IsMoving { get { return playerInput.InputValue.magnitude > 0.1f; } }
    public bool IsGrounded { get { return Physics.Raycast(coll.bounds.center, Vector3.down, coll.bounds.extents.y + groundedHeightLimit, groundLayerMask); } }
    public bool CanJump { get { return IsControllable && IsGrounded; } }

    #endregion

    private void Awake()
    {
        playerInput = GetComponent<PlayerInput>();
        playerMovement = GetComponent<PlayerMovement>();
        hover = GetComponent<PhysicsHover>();

        animator = GetComponent<Animator>();
        coll = GetComponent<Collider>();
        rb = GetComponent<Rigidbody>();
    }

    private void Update()
    {
        if (!IsMoving && IsGrounded) rb.velocity = Vector3.zero;

        Debug.Log("Is Moving: " + IsMoving);
        Debug.Log("Is Grounded: " + IsGrounded);
    }
}
