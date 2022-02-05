using UnityEngine;
using ZestGames.Utility;

[RequireComponent(typeof(GameManager))]
public class UIManager : MonoBehaviour
{
    private GameManager gameManager;
    
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

    private void Awake()
    {
        gameManager = GetComponent<GameManager>();

        Init();
    }

    private void Start()
    {
        gameManager.OnGameStart += GameStarted;
        gameManager.OnGameEnd += GameEnded;
        gameManager.OnLevelSuccess += LevelSuccess;
        gameManager.OnLevelFail += LevelFail;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.W))
        {
            Debug.Log("Win");
            gameManager.EndGameTrigger();
            gameManager.LevelSuccessTrigger();
        }
        if (Input.GetKeyDown(KeyCode.F))
        {
            Debug.Log("Fail");
            gameManager.EndGameTrigger();
            gameManager.LevelFailTrigger();
        }
    }

    private void OnDisable()
    {
        gameManager.OnGameStart -= GameStarted;
        gameManager.OnGameEnd -= GameEnded;
        gameManager.OnLevelSuccess -= LevelSuccess;
        gameManager.OnLevelFail -= LevelFail;
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
        hud.UpdateCoinTrigger(gameManager.dataManager.PlayerTotalCoin);
        hud.UpdateLevelTrigger(gameManager.levelManager.Level);

        touchToStart.gameObject.SetActive(false);
    }

    private void GameEnded() => hud.gameObject.SetActive(false);
    private void LevelSuccess() => Utils.DoActionAfterDelay(this, winScreenDelay, () => levelSuccess.gameObject.SetActive(true));
    private void LevelFail() => Utils.DoActionAfterDelay(this, failScreenDelay, () => levelFail.gameObject.SetActive(true));

    // Functions for dependant classes
    public void StartGame() => gameManager.StartGameTrigger();
    public void ChangeScene() => gameManager.ChangeSceneTrigger();
}
