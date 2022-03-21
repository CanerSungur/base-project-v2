using UnityEngine;
using UnityEngine.UI;

public class SettingsBasicUI : MonoBehaviour
{
    private Animator _animator;
    public Animator Animator => _animator == null ? _animator = GetComponent<Animator>() : _animator;

    [Header("-- REFERENCES --")]
    [SerializeField] private Image soundImage;
    [SerializeField] private Image vibrationImage;

    [Header("-- IMAGES SETUP --")]
    [SerializeField] private Sprite soundOnSprite;
    [SerializeField] private Sprite soundOffSprite;
    [SerializeField] private Sprite vibrationOnSprite;
    [SerializeField] private Sprite vibrationOffSprite;


    private Color _enabledColor = Color.white;
    private Color _disabledColor = Color.gray;
    private bool menuIsOpen = false;

    private readonly int openID = Animator.StringToHash("Open");
    private readonly int closeID = Animator.StringToHash("Close");

    private void Awake()
    {
        _enabledColor.a = 1f;
        _disabledColor.a = .5f;

        soundImage.sprite = soundOnSprite;
        vibrationImage.sprite = vibrationOnSprite;
        soundImage.color = vibrationImage.color = _enabledColor;
    }

    #region Menu

    public void ToggleMenu()
    {
        if (menuIsOpen)
            CloseMenu();
        else
            OpenMenu();
    }

    private void OpenMenu()
    {
        Animator.SetTrigger(openID);
        menuIsOpen = true;
    }

    private void CloseMenu()
    {
        Animator.SetTrigger(closeID);
        menuIsOpen = false;
    }

    #endregion

    #region MyRegion

    public void ToggleSound()
    {
        if (GameManager.IsSoundOn)
            CloseSound();
        else
            OpenSound();
    }

    private void OpenSound()
    {
        GameManager.IsSoundOn = true;
        soundImage.color = _enabledColor;
        soundImage.sprite = soundOnSprite;
    }

    private void CloseSound()
    {
        GameManager.IsSoundOn = false;
        soundImage.color = _disabledColor;
        soundImage.sprite = soundOffSprite;
    }

    #endregion

    #region Vibration

    public void ToggleVibration()
    {
        if (GameManager.IsVibrationOn)
            CloseVibration();
        else
            OpenVibration();
    }

    private void OpenVibration()
    {
        GameManager.IsVibrationOn = true;
        vibrationImage.color = _enabledColor;
        vibrationImage.sprite = vibrationOnSprite;
    }

    private void CloseVibration()
    {
        GameManager.IsVibrationOn = false;
        vibrationImage.color = _disabledColor;
        vibrationImage.sprite = vibrationOffSprite;
    }

    #endregion
}
