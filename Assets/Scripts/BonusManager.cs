using UnityEngine;

public class BonusManager : MonoBehaviour
{
    [SerializeField] private Bonus[] _bonusArray;
    [SerializeField] private Platform _platform;
    [SerializeField] private GameObject _safeLine;
    [SerializeField] private float _safeLineTimer;
    //[SerializeField] private float _expandPlatformTimer;
    private bool _isOn;
    void Start()
    {
        InvokeRepeating(nameof(SpawnBall), 5, 5);
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
    private void OnEnable()
    {
        BonusSafeLine.OnBonusSafeLine += SetSafeLine;
       
    }
    private void OnDisable()
    {
        BonusSafeLine.OnBonusSafeLine -= SetSafeLine;
    }
}
