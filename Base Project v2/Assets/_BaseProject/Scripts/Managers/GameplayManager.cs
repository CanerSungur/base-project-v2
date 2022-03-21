using UnityEngine;
using ZestGames.Utility;
using System;

/// <summary>
/// Decides how the level is won, what should player do.
/// Checks if game is over, success or fail.
/// Put in any condition you want according to gameplay.
/// </summary>
[RequireComponent(typeof(GameManager))]
public class GameplayManager : MonoBehaviour
{
    public enum WinType { Direct, Phase }
    public WinType _WinType;

    private GameManager gameManager;
    private Player player;

    [Header("-- WIN CONDITIONS --")]
    [SerializeField, Tooltip("Amount of coin needed to win level.")]private int coinNeededToWin = 5;
    private int currentCoinCount = 0;

    [Header("-- FAIL CONDITIONS --")]
    [SerializeField, Tooltip("Amount of trap needed to fail level.")] private int trapNeededTofail = 2;
    private int currentTrapCount = 0;

    [Header("-- PHASE CONDITIONS --")]
    [SerializeField, Tooltip("Amount of phase needed to win level.")] private int phaseCountToWin = 4;
    [SerializeField, Tooltip("Amount needed for phase 1")] private int coinNeededForPhase_1 = 1;
    [SerializeField, Tooltip("Amount needed for phase 2")] private int coinNeededForPhase_2 = 3;
    [SerializeField, Tooltip("Amount needed for phase 3")] private int coinNeededForPhase_3 = 5;
    private int currentPhaseCount = 1;

    [Header("-- SETUP --")]
    [SerializeField, Tooltip("Delay in seconds that will take to invoke level success or level fail after game ends.")] private float gameEndAfterDelay = 1f;

    public bool PhaseIsFinished { get { return currentPhaseCount >= phaseCountToWin; } }
    public int CurrentPhaseCount => currentPhaseCount;

    private void Awake()
    {
        gameManager = GetComponent<GameManager>();
        player = FindObjectOfType<Player>();
    }

    private void Start()
    {
        player.OnPickedUpSomething += UpdatePickUpCondition;

        player.OnKill += LevelFail;
    }

    private void OnDisable()
    {
        player.OnPickedUpSomething -= UpdatePickUpCondition;

        player.OnKill -= LevelFail;
    }

    private void LevelFail()
    {
        //gameManager.EndGameTrigger();
        Delayer.DoActionAfterDelay(this, gameEndAfterDelay, () => GameEvents.OnGameEnd?.Invoke());
        Delayer.DoActionAfterDelay(this, gameEndAfterDelay * 2f, () => GameEvents.OnLevelFail?.Invoke());
    }

    private void LevelWin()
    {
        //gameManager.EndGameTrigger();
        Delayer.DoActionAfterDelay(this, gameEndAfterDelay, () => GameEvents.OnGameEnd?.Invoke());
        Delayer.DoActionAfterDelay(this, gameEndAfterDelay * 2f, () => GameEvents.OnLevelSuccess?.Invoke());
    }

    private void UpdatePickUpCondition(CollectableEffect collectableEffect)
    {
        if (collectableEffect == CollectableEffect.Positive)
            currentCoinCount++;
        else if (collectableEffect == CollectableEffect.Negative)
            currentTrapCount++;

        CheckLevelStatus(collectableEffect);
    }

    private void CheckLevelStatus(CollectableEffect collectableEffect)
    {
        if (currentTrapCount >= trapNeededTofail)
            LevelFail();

        if (collectableEffect == CollectableEffect.Negative) return;

        if (_WinType == WinType.Direct)
        {
            if (currentCoinCount >= coinNeededToWin)
                LevelWin();
        }
        else if (_WinType == WinType.Phase)
        {
            if (currentCoinCount == coinNeededForPhase_1 || currentCoinCount == coinNeededForPhase_2 || currentCoinCount == coinNeededForPhase_3)
            {
                currentPhaseCount++;
                GameEvents.OnChangePhase?.Invoke();
            }

            if (currentPhaseCount >= phaseCountToWin)
                LevelWin();
        }
    }
}
