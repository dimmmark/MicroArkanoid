using System.Collections;
using TMPro;
using UnityEngine;

public class BonusManager : MonoBehaviour
{
    [SerializeField] private Bonus[] _bonusArray;
    [SerializeField] private Platform _platform;
    [SerializeField] private GameObject _safeLine;
    [SerializeField] private float _safeLineTimer;
    [SerializeField] private TextMeshProUGUI _safeLineTimerText;
    private int _randomInt;
    private bool _isOn;
    void Start()
    {
        
    }
    private void Update()
    {
        if (_safeLineTimer > 0 && _isOn)
        {
            _safeLineTimer -= Time.deltaTime;
            _safeLine.SetActive(true);
            _safeLineTimerText.text = _safeLineTimer.ToString("F1");
        }
        else
        {
            _safeLine.SetActive(false);
            _isOn = false;
        }
    }
    private void SetSafeLine()
    {
        _safeLineTimer += 7.5f;
        _isOn = true;
    }
    private void MakeBonus(Pixel pixel)
    {
        if (Game.Instance.BallsList.Count <= 3)
        {
            _randomInt = Random.Range(1, 6);
            if (_randomInt == 2)
            {
                Instantiate(_bonusArray[Random.Range(0, _bonusArray.Length)], pixel.transform.position, Quaternion.identity);
            }
        }
        else if (Game.Instance.BallsList.Count > 3 && Game.Instance.BallsList.Count <= 21)
        {
            _randomInt = Random.Range(1, 21);
            if (_randomInt == 2)
            {
                Instantiate(_bonusArray[Random.Range(1, _bonusArray.Length)], pixel.transform.position, Quaternion.identity);
            }
        }
        else if (Game.Instance.BallsList.Count > 21 && Game.Instance.BallsList.Count <= 100)
        {
            _randomInt = Random.Range(1, 51);
            if (_randomInt == 2)
            {
                Instantiate(_bonusArray[Random.Range(4, _bonusArray.Length)], pixel.transform.position, Quaternion.identity);
            }
        }
    }
    public void Add100Balls()
    {
        StartCoroutine(Add100());
    }

    IEnumerator Add100()
    {

        for (int i = 0; i < 100; i++)
        {
            Game.Instance.SetBall();
            yield return null;
        }
    }
    public void AddSafeLine30Sec()
    {
        for (int i = 0; i < 4; i++)
        {
            SetSafeLine();
        }
    }
    public void ExtraExtendPlatform()
    {
        for (int i = 0; i < 5; i++)
        {
            _platform.ExpandPlatform();
        }
    }
    private void OnEnable()
    {
        BonusSafeLine.OnBonusSafeLine += SetSafeLine;
        Ball.OnCollidedPixel += MakeBonus;

    }
    private void OnDisable()
    {
        BonusSafeLine.OnBonusSafeLine -= SetSafeLine;
        Ball.OnCollidedPixel -= MakeBonus;
    }
}
