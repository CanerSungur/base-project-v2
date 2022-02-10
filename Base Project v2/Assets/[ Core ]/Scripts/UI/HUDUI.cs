using UnityEngine;
using TMPro;
using System;
using DG.Tweening;

public class HUDUI : MonoBehaviour
{
    private UIManager uiManager;
    public UIManager UIManager { get { return uiManager == null ? uiManager = FindObjectOfType<UIManager>() : uiManager; } }

    [Header("-- TEXT REFERENCES --")]
    [SerializeField] private TextMeshProUGUI coinText;
    [SerializeField] private TextMeshProUGUI levelText;

    [Header("-- COIN SETUP --")]
    [SerializeField] private Transform coinHUDTransform;
    public Transform CoinHUDTransform => coinHUDTransform;

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
    private void UpdateLevelText(int level)
    {
        //Debug.Log("Updated Coin Text");
        levelText.text = $"Level {level}";
    }
    private void UpdateCoinText(int coin)
    {
        //Debug.Log("Updated Level Text");
        //coinText.text = coin.ToString();
        coinText.text = UIManager.GameManager.dataManager.PlayerTotalCoin.ToString();

        ShakeCoinHUD();
    }

    private void ShakeCoinHUD()
    {
        CoinHUDTransform.DORewind();

        CoinHUDTransform.DOShakePosition(.5f, .5f);
        CoinHUDTransform.DOShakeRotation(.5f, .5f);
        CoinHUDTransform.DOShakeScale(.5f, .5f);
    }
}
