using UnityEngine;

/// <summary>
/// Holds all data about this game.
/// </summary>
[RequireComponent(typeof(GameManager))]
public class DataManager : MonoBehaviour
{
    private GameManager gameManager;
    public GameManager GameManager => gameManager == null ? gameManager = GetComponent<GameManager>() : gameManager;

    public int TotalCoin { get; private set; }
    public int RewardCoin { get; private set; }

    private void Awake()
    {
        TotalCoin = PlayerPrefs.GetInt("TotalCoin", 0);
        RewardCoin = 0;
    }

    private void Start()
    {
        CollectableEvents.OnIncreaseCoin += IncreaseTotalCoin;
        GameEvents.OnCalculateReward += CalculateReward;
    }

    private void OnDisable()
    {
        CollectableEvents.OnIncreaseCoin -= IncreaseTotalCoin;
        GameEvents.OnCalculateReward -= CalculateReward;
    }

    private void IncreaseTotalCoin(int amount)
    {
        TotalCoin += amount;
        PlayerPrefs.SetInt("TotalCoin", TotalCoin);
        PlayerPrefs.Save();
    }

    private void CalculateReward() => RewardCoin = 55;
}
