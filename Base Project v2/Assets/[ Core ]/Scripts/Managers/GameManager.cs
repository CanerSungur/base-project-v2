using UnityEngine;
using System;

public class GameManager : MonoBehaviour
{
    [Header("-- MANAGER REFERENCES --")]
    internal DataManager dataManager;
    internal UIManager uiManager;
    internal LevelManager levelManager;

    public GameState GameState { get; private set; }
    public GameEnd GameEnd { get; private set; }

    public event Action OnGameStart, OnGameEnd, OnLevelSuccess, OnLevelFail, OnChangeScene;

    private void Awake()
    {
        TryGetComponent(out dataManager);
        TryGetComponent(out uiManager);
        TryGetComponent(out levelManager);
        //dataManager = GetComponent<DataManager>();
        //uiManager = GetComponent<UIManager>();
        //levelManager = GetComponent<LevelManager>();

        ChangeState(GameState.WaitingToStart);
    }

    private void Start()
    {
        OnGameStart += () => ChangeState(GameState.Started);
        OnGameEnd += () => ChangeState(GameState.Finished);
        OnLevelSuccess += () => GameEnd = GameEnd.Win;
        OnLevelFail += () => GameEnd = GameEnd.Fail;
    }

    private void OnDisable()
    {
        OnGameStart -= () => ChangeState(GameState.Started);
        OnGameEnd -= () => ChangeState(GameState.Finished);
        OnLevelSuccess -= () => GameEnd = GameEnd.Win;
        OnLevelFail -= () => GameEnd = GameEnd.Fail;
    }

    private void ChangeState(GameState newState)
    {
        if (GameState != newState) GameState = newState;
    }

    // Event Trigger Functions
    public void StartGameTrigger() => OnGameStart?.Invoke();
    public void EndGameTrigger() => OnGameEnd?.Invoke();
    public void LevelSuccessTrigger() => OnLevelSuccess?.Invoke();
    public void LevelFailTrigger() => OnLevelFail?.Invoke();
    public void ChangeSceneTrigger() => OnChangeScene?.Invoke();
}

public enum GameState
{
    WaitingToStart,
    Started,
    Paused,
    PlatformIsOver,
    Finished
}

public enum GameEnd { Fail, Win }