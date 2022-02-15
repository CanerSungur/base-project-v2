using UnityEngine;

public class Coin : CollectableBase
{
    private CoinManager coinManager;
    public CoinManager CoinManager { get { return coinManager == null ? coinManager = FindObjectOfType<CoinManager>() : coinManager; } }

    [Header("-- REFERENCES --")]
    [SerializeField] private CoinMovement coinMovement;

    [Header("-- PROPERTIES --")]
    [SerializeField, Tooltip("Value of this coin. This will be added to Player Coin Amount.")] private int value = 1;

    [Header("-- MOVEMENT SETUP --")]
    [SerializeField, Tooltip("Speed of this coin moving to HUD Coin Position.")] private float movementSpeed = 2f;

    // Properties
    public float MovementSpeed => movementSpeed;
    public int Value => value;

    public override void Apply()
    {
        if (CollectStyle == CollectStyle.OnSite)
        {
            // Apply instantly.
            CoinManager.GameManager.IncreaseCoinTrigger(Value);
        }
        else if (CollectStyle == CollectStyle.MoveToUI)
        {
            // Activate coin movement to UI.
            coinMovement.StartMovingTrigger();
        }
    }
}
