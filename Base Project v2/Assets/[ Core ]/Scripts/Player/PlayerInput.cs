using UnityEngine;
using System;

[RequireComponent(typeof(Player))]
public class PlayerInput : MonoBehaviour
{
    private Player player;

    public Vector3 InputValue { get; private set; }
    private bool jumpPressed;

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
            InputValue = new Vector3(Input.GetAxis("Horizontal"), 0f, Input.GetAxis("Vertical"));
    }

    public void JumpPressedTrigger() => OnJumpPressed?.Invoke();
}