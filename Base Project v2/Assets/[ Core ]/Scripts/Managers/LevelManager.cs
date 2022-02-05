using UnityEngine;
using UnityEngine.SceneManagement;

[RequireComponent(typeof(GameManager))]
public class LevelManager : MonoBehaviour
{
    private GameManager gameManager;
    private SceneTransition sceneTransition;

    public int Level { get { return PlayerPrefs.GetInt("Level", 1); } }
    private int currentLevel, lastSceneBuildIndex;

    private void Awake()
    {
        gameManager = GetComponent<GameManager>();
        currentLevel = Level;

        sceneTransition = FindObjectOfType<SceneTransition>();
    }

    private void Start()
    {
        gameManager.OnChangeScene += ChangeScene;
    }

    private void OnDisable()
    {
        gameManager.OnChangeScene -= ChangeScene;
    }

    private void ChangeScene()
    {
        if (GameManager.GameEnd == GameEnd.Fail)
            sceneTransition.LoadLevel(currentLevel);
        else if (GameManager.GameEnd == GameEnd.Win)
            sceneTransition.LoadLevel(GetSceneBuildIndexToBeLoaded());

        //if (sceneTransition == null)
        //{
        //    if (gameManager.GameEnd == GameEnd.Fail)
        //        SceneManager.LoadScene(currentLevel);
        //    else if (gameManager.GameEnd == GameEnd.Win)
        //        SceneManager.LoadScene(GetSceneBuildIndexToBeLoaded());
        //}
        //else
        //{
        //    if (gameManager.GameEnd == GameEnd.Fail)
        //        sceneTransition.LoadLevel(currentLevel);
        //    else if (gameManager.GameEnd == GameEnd.Win)
        //        sceneTransition.LoadLevel(GetSceneBuildIndexToBeLoaded());
        //}
    }

    public int GetSceneBuildIndexToBeLoaded()
    {
        // Uncomment this and run game once to reset level.
        //DeleteLevelData();

        lastSceneBuildIndex = SceneManager.sceneCountInBuildSettings - 1;
        int index = Level % lastSceneBuildIndex;
        if (index == 0)
            currentLevel = lastSceneBuildIndex;
        else
            currentLevel = index;

        return currentLevel;
    }

    private void DeleteLevelData()
    {
        PlayerPrefs.SetInt("Level", 1);
        PlayerPrefs.Save();
    }
}
