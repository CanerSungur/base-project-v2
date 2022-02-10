using UnityEngine;

/// <summary>
/// Holds all data about this game.
/// </summary>
[RequireComponent(typeof(GameManager))]
public class DataManager : MonoBehaviour
{
    private GameManager gameManager;
    public GameManager GameManager { get { return gameManager == null ? gameManager = GetComponent<GameManager>() : gameManager; } }

    public int PlayerTotalCoin { get; private set; }

    private void Awake()
    {
        PlayerTotalCoin = PlayerPrefs.GetInt("TotalCoin", 0);
    }

    private void Start()
    {
        GameManager.OnUpdateCoin += UpdateTotalCoin;
    }

    private void OnDisable()
    {
        GameManager.OnUpdateCoin += UpdateTotalCoin;
    }

    private void UpdateTotalCoin(int amount)
    {
        PlayerTotalCoin += amount;
        PlayerPrefs.SetInt("TotalCoin", PlayerTotalCoin);
        PlayerPrefs.Save();
    }
}
