using UnityEngine;
using System;

/// <summary>
/// Manages all the other managers. Holds game flow events.
/// If there will be a reward when level is finished, invoke OnCalculateReward instead of OnLevelSuccess.
/// </summary>
public class GameManager : MonoBehaviour
{
    [Header("-- MANAGER REFERENCES --")]
    internal DataManager dataManager;
    internal UIManager uiManager;
    internal LevelManager levelManager;
    internal GameplayManager gameplayManager;

    public static GameState GameState { get; private set; }
    public static GameEnd GameEnd { get; private set; }

    public static bool IsSoundOn, IsVibrationOn;

    private void Awake()
    {
        IsSoundOn = IsVibrationOn = true;

        TryGetComponent(out dataManager);
        TryGetComponent(out uiManager);
        TryGetComponent(out levelManager);
        TryGetComponent(out gameplayManager);

        ChangeState(GameState.WaitingToStart);
    }

    private void Start()
    {
        GameEvents.OnGameStart += () => ChangeState(GameState.Started);
        GameEvents.OnGameEnd += () => ChangeState(GameState.Finished);
        GameEvents.OnLevelSuccess += LevelSuccess;
        GameEvents.OnLevelFail += LevelFail;
    }

    private void OnDisable()
    {
        GameEvents.OnGameStart -= () => ChangeState(GameState.Started);
        GameEvents.OnGameEnd -= () => ChangeState(GameState.Finished);
        GameEvents.OnLevelSuccess -= LevelSuccess;
        GameEvents.OnLevelFail -= LevelFail;
    }

    private void LevelSuccess()
    {
        GameEnd = GameEnd.Win;
        AudioHandler.PlayAudio(AudioHandler.AudioType.Game_Win);
    }

    private void LevelFail()
    {
        GameEnd = GameEnd.Fail;
        AudioHandler.PlayAudio(AudioHandler.AudioType.Game_Fail);
    }

    private void ChangeState(GameState newState)
    {
        if (GameState != newState) GameState = newState;
    }
}

public enum GameState
{
    WaitingToStart,
    Started,
    Paused,
    PlatformIsOver,
    Finished
}

public enum GameEnd { NotDecided, Fail, Win }
