using UnityEngine;
using System;

[RequireComponent(typeof(Player))]
public class JoystickInput : MonoBehaviour
{
    private Player player;

    [Header("-- INPUT SETUP --")]
    [SerializeField] private Joystick joystick;

    public Vector3 InputValue { get; private set; }
    private bool jumpPressed;
    public bool JumpPressed => jumpPressed;

    private float timer;

    public event Action OnJumpPressed;

    private void Awake()
    {
        player = GetComponent<Player>();
        jumpPressed = false;
        timer = player.JumpCooldown;
    }

    private void Update()
    {
        if (!player) return;

        if (Input.GetKeyDown(KeyCode.Space) && player.CanJump && !jumpPressed)
        {
            OnJumpPressed?.Invoke();
            jumpPressed = true;
        }

        if (jumpPressed)
        {
            timer -= Time.deltaTime;
            if (timer <= 0f)
            {
                timer = player.JumpCooldown;
                jumpPressed = false;
            }
        }

        if (player.IsControllable)
            InputValue = new Vector3(joystick.Horizontal, 0f, joystick.Vertical);
    }
}
