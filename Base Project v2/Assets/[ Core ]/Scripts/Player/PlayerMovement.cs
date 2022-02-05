using UnityEngine;

[RequireComponent(typeof(Player))]
public class PlayerMovement : MonoBehaviour
{
    private Player player;
    private float cameraRotationY;

    private void Awake()
    {
        player = GetComponent<Player>();
    }

    private void Start()
    {
        player.playerInput.OnJumpPressed += DoJump;
    }

    private void OnDisable()
    {
        player.playerInput.OnJumpPressed -= DoJump;
    }

    private void FixedUpdate()
    {
        if (!player ) return;

        if (!player.IsControllable || !player.IsMoving) return;
        
        cameraRotationY = Camera.main.transform.rotation.eulerAngles.y;
        DoMovement(cameraRotationY);
        DoRotation(cameraRotationY);
    }

    private void DoMovement(float cameraRotationY)
    {
        player.rb.MovePosition(transform.position + Quaternion.Euler(0f, cameraRotationY, 0f) * player.playerInput.InputValue.normalized * Time.fixedDeltaTime * player.MovementSpeed);
    }

    private void DoRotation(float cameraRotationY)
    {
        Vector3 direction = (Quaternion.Euler(0f, cameraRotationY, 0f) * player.playerInput.InputValue).normalized;

        float targetAngle = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.Euler(transform.rotation.eulerAngles.x, targetAngle, transform.rotation.eulerAngles.z), player.TurnSmoothTime);
    }

    private void DoJump()
    {
        player.rb.AddForce(new Vector3(0f, player.JumpForce, 0f), ForceMode.Impulse);
    }
}
