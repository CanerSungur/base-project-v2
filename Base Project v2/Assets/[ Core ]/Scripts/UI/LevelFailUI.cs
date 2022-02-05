using UnityEngine;
using UnityEngine.UI;

public class LevelFailUI : MonoBehaviour
{
    private UIManager uiManager;
    public UIManager UIManager { get { return uiManager == null ? uiManager = FindObjectOfType<UIManager>() : uiManager; } }

    private Button restartButton;

    private void OnEnable()
    {
        restartButton = GetComponentInChildren<Button>();
        restartButton.onClick.AddListener(RestartButtonClicked);
    }

    private void OnDisable()
    {
        restartButton.onClick.RemoveListener(RestartButtonClicked);
    }

    private void RestartButtonClicked()
    {
        Debug.Log("Restart Button Clicked!");

        UIManager.ChangeScene();
    }
}
