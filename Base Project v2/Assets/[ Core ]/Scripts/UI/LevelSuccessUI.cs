using UnityEngine;
using UnityEngine.UI;

public class LevelSuccessUI : MonoBehaviour
{
    private UIManager uiManager;
    public UIManager UIManager { get { return uiManager == null ? uiManager = FindObjectOfType<UIManager>() : uiManager; } }

    private Button nextButton;

    private void OnEnable()
    {
        nextButton = GetComponentInChildren<Button>();
        nextButton.onClick.AddListener(NextButtonClicked);
    }

    private void OnDisable()
    {
        nextButton.onClick.RemoveListener(NextButtonClicked);
    }

    private void NextButtonClicked()
    {
        Debug.Log("Next Button Clicked!");

        UIManager.ChangeScene();
    }
}
