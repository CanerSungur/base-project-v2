using System;
using UnityEngine;

[RequireComponent(typeof(Player))]
public class PlayerAnimationController : MonoBehaviour
{
    private Player player;

	[Header("-- ANIMATION NAME SETUP --")]
	private readonly int animatorGrounded = Animator.StringToHash("Base Layer.Grounded");
    private readonly int turnID = Animator.StringToHash("Turn");
    private readonly int forwardID = Animator.StringToHash("Forward");

	// Extra tweaks for animations
	const float JumpPower = 5f;     // determines the jump force applied when jumping (and therefore the jump height)
	const float AirSpeed = 5f;      // determines the max speed of the character while airborne
	const float AirControl = 2f;    // determines the response speed of controlling the character while airborne
	const float StationaryTurnSpeed = 180f; // additional turn speed added when the player is stationary (added to animation root rotation)
	const float MovingTurnSpeed = 360f;     // additional turn speed added when the player is moving (added to animation root rotation)
	const float RunCycleLegOffset = 0.2f;   // animation cycle offset (0-1) used for determining correct leg to jump off

	private void Awake()
    {
        player = GetComponent<Player>();
		player.animator.applyRootMotion = false;
    }
}
