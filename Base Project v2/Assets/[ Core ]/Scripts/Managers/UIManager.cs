using UnityEngine;
using ZestGames.Utility;

[RequireComponent(typeof(GameManager))]
public class UIManager : MonoBehaviour
{
    private GameManager gameManager;
    public GameManager GameManager { get { return gameManager == null ? gameManager = GetComponent<GameManager>() : gameManager; } }

    [Header("-- UI REFERENCES --")]
    [SerializeField] private TouchToStartUI touchToStart;
    [SerializeField] private HUDUI hud;
    [SerializeField] private LevelSuccessUI levelSuccess;
    [SerializeField] private LevelFailUI levelFail;

    [Header("-- UI DELAY SETUP --")]
    [SerializeField, Tooltip("The delay in seconds between the game is won and the win screen is loaded.")] 
    private float winScreenDelay = 3.0f;
    [SerializeField, Tooltip("The delay in secods between the game is lost and the fail screen is loaded.")] 
    private float failScreenDelay = 3.0f;

    public Transform CoinHUDTransform => hud.CoinHUDTransform;

    private void Awake()
    {
        Init();
    }

    private void Start()
    {
        GameManager.OnGameStart += GameStarted;
        GameManager.OnGameEnd += GameEnded;
        GameManager.OnLevelSuccess += LevelSuccess;
        GameManager.OnLevelFail += LevelFail;

        GameManager.OnUpdateCoin += hud.UpdateCoinTrigger;
    }

    private void Update()
    {
        //if (Input.GetKeyDown(KeyCode.W))
        //{
        //    Debug.Log("Win");
        //    gameManager.EndGameTrigger();
        //    gameManager.LevelSuccessTrigger();
        //}
        //if (Input.GetKeyDown(KeyCode.F))
        //{
        //    Debug.Log("Fail");
        //    gameManager.EndGameTrigger();
        //    gameManager.LevelFailTrigger();
        //}
    }

    private void OnDisable()
    {
        GameManager.OnGameStart -= GameStarted;
        GameManager.OnGameEnd -= GameEnded;
        GameManager.OnLevelSuccess -= LevelSuccess;
        GameManager.OnLevelFail -= LevelFail;

        GameManager.OnUpdateCoin -= hud.UpdateCoinTrigger;
    }

    private void Init()
    {
        touchToStart.gameObject.SetActive(true);

        hud.gameObject.SetActive(false);
        levelSuccess.gameObject.SetActive(false);
        levelFail.gameObject.SetActive(false);
    }

    private void GameStarted()
    {
        hud.gameObject.SetActive(true);
        hud.UpdateCoinTrigger(GameManager.dataManager.PlayerTotalCoin);
        hud.UpdateLevelTrigger(GameManager.levelManager.Level);

        touchToStart.gameObject.SetActive(false);
    }

    private void GameEnded() => hud.gameObject.SetActive(false);
    private void LevelSuccess() => Utils.DoActionAfterDelay(this, winScreenDelay, () => levelSuccess.gameObject.SetActive(true));
    private void LevelFail() => Utils.DoActionAfterDelay(this, failScreenDelay, () => levelFail.gameObject.SetActive(true));

    // Functions for dependant classes
    public void StartGame() => GameManager.StartGameTrigger();
    public void ChangeScene() => GameManager.ChangeSceneTrigger();
}
