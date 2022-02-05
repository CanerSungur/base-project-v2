using UnityEngine;
using TMPro;
using System;

public class HUDUI : MonoBehaviour
{
    private UIManager uiManager;
    public UIManager UIManager { get { return uiManager == null ? uiManager = FindObjectOfType<UIManager>() : uiManager; } }

    [Header("-- TEXT REFERENCES --")]
    [SerializeField] private TextMeshProUGUI coinText;
    [SerializeField] private TextMeshProUGUI levelText;

    public event Action<int> OnUpdateCoin, OnUpdateLevel;

    private void OnEnable()
    {
        OnUpdateCoin += UpdateCoinText;
        OnUpdateLevel += UpdateLevelText;
    }

    private void OnDisable()
    {
        OnUpdateCoin -= UpdateCoinText;
        OnUpdateLevel -= UpdateLevelText;
    }

    public void UpdateCoinTrigger(int coin) => OnUpdateCoin?.Invoke(coin);
    public void UpdateLevelTrigger(int level) => OnUpdateLevel?.Invoke(level);
    public void UpdateLevelText(int level)
    {
        Debug.Log("Updated Coin Text");
        levelText.text = $"Level {level}";
    }
    public void UpdateCoinText(int coin)
    {
        Debug.Log("Updated Level Text");
        coinText.text = coin.ToString();
    }
}
