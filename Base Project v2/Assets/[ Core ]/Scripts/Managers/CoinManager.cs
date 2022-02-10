using UnityEngine;
using System;

[RequireComponent(typeof(GameManager))]
public class CoinManager : MonoBehaviour
{
    private GameManager gameManager;
    public GameManager GameManager { get { return gameManager == null ? gameManager = GetComponent<GameManager>() : gameManager; } }

    [Header("-- SETUP --")]
    [SerializeField, Tooltip("Object that will be spawned as reward when an object is destroyed.")]private GameObject coinPrefab;
    [SerializeField, Tooltip("Offset relative to the destroyed object's position.")] private float spawnPointOffset = 2.75f;
    public Transform CoinHUDTransform => GameManager.uiManager.CoinHUDTransform;

    public static event Action<Vector3, int> OnSpawnCoins;

    private void Start()
    {
        OnSpawnCoins += SpawnCoins;
    }

    private void OnDisable()
    {
        OnSpawnCoins -= SpawnCoins;
    }

    private void SpawnCoins(Vector3 spawnPosition, int amount)
    {
        for (int i = 0; i < amount; i++)
        {
            Instantiate(coinPrefab,spawnPosition + (Vector3.up * spawnPointOffset), Quaternion.identity);
        }
    }

    public static void SpawnCoinsTrigger(Vector3 spawnPosition, int amount) => OnSpawnCoins?.Invoke(spawnPosition, amount);
}
