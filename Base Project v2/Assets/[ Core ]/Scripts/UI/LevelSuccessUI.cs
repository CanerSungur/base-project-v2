using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class LevelSuccessUI : MonoBehaviour
{
    private UIManager uiManager;
    public UIManager UIManager { get { return uiManager == null ? uiManager = FindObjectOfType<UIManager>() : uiManager; } }

    private TextMeshProUGUI levelText;
    private Button nextButton;

    private void OnEnable()
    {
        levelText = transform.GetChild(transform.childCount - 1).GetComponentInChildren<TextMeshProUGUI>();
        levelText.text = "Level " + (UIManager.GameManager.levelManager.Level - 1); // -1 because level is increased immediately on level success.
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
