using UnityEngine;

public class BonusManager : MonoBehaviour
{
    [SerializeField] private Bonus[] _bonusArray;
    [SerializeField] private Platform _platform;
    [SerializeField] private GameObject _safeLine;
    [SerializeField] private float _timer;
    private bool _isOn;
    void Start()
    {
        InvokeRepeating(nameof(SpawnBall), 5, 5);
    }
    private void Update()
    {
        if (_timer > 0 && _isOn)
        {
        _timer -= Time.deltaTime;
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
        Instantiate(_bonusArray[2], new Vector3(Random.Range(-24, 24), 46.5f, 0), Quaternion.identity);

    }
    private void SetSafeLine()
    {
       // _timer = 0;
        _timer += 7.5f;
        _isOn = true;
        //_safeLine.SetActive(true);
        //Invoke(nameof(OffSafeLine), 7.5f);
    }
    //private void OffSafeLine()
    //{
    //    _safeLine.SetActive(false);
    //}
    private void OnEnable()
    {
        BonusSafeLine.OnBonusSafeLine += SetSafeLine;
    }
    private void OnDisable()
    {
        BonusSafeLine.OnBonusSafeLine -= SetSafeLine;
    }
}
