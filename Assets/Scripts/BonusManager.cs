using UnityEngine;

public class BonusManager : MonoBehaviour
{
    [SerializeField] private Bonus[] _bonusArray;
    [SerializeField] private Platform _platform;
    [SerializeField] private GameObject _safeLine;
    [SerializeField] private float _safeLineTimer;
    private int _randomInt;
    //[SerializeField] private float _expandPlatformTimer;
    private bool _isOn;
    void Start()
    {
        // InvokeRepeating(nameof(SpawnBall), 5, 5);
    }
    private void Update()
    {
        if (_safeLineTimer > 0 && _isOn)
        {
            _safeLineTimer -= Time.deltaTime;
            _safeLine.SetActive(true);
        }
        else
        {
            _safeLine.SetActive(false);
            _isOn = false;
        }
    }
    private void SpawnBall()
    {
        Instantiate(_bonusArray[1], new Vector3(Random.Range(-24, 24), 46.5f, 0), Quaternion.identity);

    }
    private void SetSafeLine()
    {
        _safeLineTimer += 7.5f;
        _isOn = true;
    }
    //private void ExpandPlatform()
    //{
    //    _platform.transform.localScale.x 
    //}
    private void MakeBonus(Pixel pixel)
    {
        if (Game.Instance.BallsList.Count <= 3)
        {
            _randomInt = Random.Range(1, 6);
            Debug.Log(_randomInt);
            if (_randomInt == 2)
            {
                Instantiate(_bonusArray[Random.Range(0, _bonusArray.Length)], pixel.transform.position, Quaternion.identity);
            }
        }
        else if (Game.Instance.BallsList.Count > 3 && Game.Instance.BallsList.Count <= 21)
        {
            _randomInt = Random.Range(1, 21);
            Debug.Log(_randomInt);
            if (_randomInt == 2)
            {
                Instantiate(_bonusArray[Random.Range(0, _bonusArray.Length)], pixel.transform.position, Quaternion.identity);
            }
        }
        else if (Game.Instance.BallsList.Count > 21 && Game.Instance.BallsList.Count <= 100)
        {
            _randomInt = Random.Range(1, 51);
            Debug.Log(_randomInt);
            if (_randomInt == 2)
            {
                Instantiate(_bonusArray[Random.Range(0, _bonusArray.Length)], pixel.transform.position, Quaternion.identity);
            }
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
