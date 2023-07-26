using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UI : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _levelText;
    [SerializeField] private TextMeshProUGUI _ballsText;
    [SerializeField] private GameObject _winScrene;
    [SerializeField] private GameObject _loseScrene;
    [SerializeField] private GameObject _menuScrene;
    [SerializeField] private Button _soundButton;
    [SerializeField] private Sprite _soundImageOn;
    [SerializeField] private Sprite _soundImageOff;
    [SerializeField] private GameObject _rewardButton;
    private static bool _isMute;
    void Start()
    {
        _soundButton.onClick.AddListener(Mute);
        UpdateInfo(Game.Instance.BallsList.Count);
        InvokeRepeating(nameof(ShowRewardButton), 10, 10);
    }

    private void ShowRewardButton()
    {
        if (Game.Instance.BallsList.Count < 50)
            _rewardButton.SetActive(true);
    }
    private void Mute()
    {
        if (_isMute)
        {
            AudioListener.volume = 1;
            _isMute = false;
            _soundButton.image.sprite = _soundImageOn;
        }
        else
        {
            AudioListener.volume = 0;
            _isMute = true;
            _soundButton.image.sprite = _soundImageOff;
        }
    }

    public void ShowWinScrene()
    {
        _winScrene.SetActive(true);
    }
    public void ShowLoseScrene()
    {
        _loseScrene.SetActive(true);
    }
    public void ShowMenuScrene()
    {
        _menuScrene.SetActive(true);
        _rewardButton.SetActive(false);
    }
    public void UpdateInfo(int count)
    {
        _levelText.text = (Game.Instance.LevelIndex+1).ToString();
        _ballsText.text = count.ToString();
    }
    private void OnEnable()
    {
        Game.OnBallsChanged += UpdateInfo;
    }
    private void OnDisable()
    {
        Game.OnBallsChanged -= UpdateInfo;
    }
}
