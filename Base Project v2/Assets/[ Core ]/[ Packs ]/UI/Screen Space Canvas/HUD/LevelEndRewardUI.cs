using UnityEngine;
using TMPro;

/// <summary>
/// Dependant to HUD UI Script.
/// This script only triggers reward animation, shows rewarded value if reward is activated.
/// </summary>
public class LevelEndRewardUI : MonoBehaviour
{
    private HUDUI hudUI;
    public HUDUI HUDUI { get { return hudUI == null ? hudUI = GetComponentInParent<HUDUI>() : hudUI; } }

    [Header("-- SETUP --")]
    private TextMeshProUGUI rewardCoinText;
    private readonly int startID = Animator.StringToHash("Start");
    private readonly int amountID = Animator.StringToHash("Amount");

    private void OnEnable()
    {
        HUDUI.UIManager.GameManager.OnCalculateReward += TriggerReward;
    }

    private void OnDisable()
    {
        HUDUI.UIManager.GameManager.OnCalculateReward -= TriggerReward;
    }

    private void TriggerReward()
    {
        rewardCoinText = transform.GetChild(transform.childCount - 1).GetComponent<TextMeshProUGUI>();
        rewardCoinText.text = "+" + HUDUI.UIManager.GameManager.dataManager.RewardCoin;

        HUDUI.Animator.SetTrigger(startID);
    }

    // Animation event listener.
    public void AlertObservers(string message)
    {
        if (message.Equals("RewardAnimEnded")) // Level success screen should trigger here.
            HUDUI.UIManager.GameManager.LevelSuccessTrigger();
        else if (message.Equals("UpdateCoin"))
            HUDUI.UpdateCoinUITrigger(HUDUI.UIManager.GameManager.dataManager.TotalCoin);
    }
}