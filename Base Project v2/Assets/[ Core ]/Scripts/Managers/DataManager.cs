using UnityEngine;

/// <summary>
/// Holds all data about this game.
/// </summary>
[RequireComponent(typeof(GameManager))]
public class DataManager : MonoBehaviour
{
    private GameManager gameManager;

    public int PlayerTotalCoin { get; private set; }

    private void Awake()
    {
        gameManager = GetComponent<GameManager>();

        PlayerTotalCoin = 152;
    }
}
