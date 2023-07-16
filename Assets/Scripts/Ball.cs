using UnityEngine;
using static TMPro.SpriteAssetUtilities.TexturePacker_JsonArray;

public class Ball : MonoBehaviour
{
    [SerializeField] private float _speed;
    [SerializeField] private float _xBound;
    [SerializeField] private float _topBound;
    [SerializeField] private float _bottomBound;
    private Vector2 _direction;
    private Rigidbody2D _rigidbody;
    public static event System.Action<Pixel> OnCollidedPixel;
    private Vector2 _vectorAtract;
    private bool _isPlaying;
    private int _frames;

    private void Start()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0) && !_isPlaying)
        {
            LaunchBall();
        }
    }

    private void FixedUpdate()
    {
        _frames++;
        _rigidbody.MovePosition(transform.position + new Vector3(_direction.x, _direction.y, 0) * _speed * Time.deltaTime);
    }

    private void LaunchBall()
    {
        float randomAngle = Random.Range(-15f, 15f);
        _direction = Quaternion.Euler(0f, 0f, randomAngle) * Vector2.up;
        _isPlaying = true;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {

        if (collision.gameObject.GetComponent<Platform>())
        {
            float hitFactor = CalculateHitFactor(transform.position, collision.transform.position, collision.collider.bounds.size.x);
            _direction = new Vector2(hitFactor, 1f).normalized;
        }
        else if (collision.gameObject.TryGetComponent<Pixel>(out Pixel pixel))
        {
            _vectorAtract = collision.GetContact(0).normal.normalized;
            _vectorAtract.x = Mathf.Round(_vectorAtract.x);
            _vectorAtract.y = Mathf.Round(_vectorAtract.y);
            if (_vectorAtract.x == 0)
            {
                _direction.y = -_direction.y;
            }
            if (_vectorAtract.y == 0)
            {
                _direction.x = -_direction.x;
            }
            if (_frames>2)
            {
                OnCollidedPixel.Invoke(pixel);
                pixel.gameObject.SetActive(false);
                _frames = 0;
            }
        }
        else if (collision.gameObject.GetComponent<Bounds>())
        {
            _direction.x = -_direction.x;
        }
        else if (collision.gameObject.GetComponent<BoundTop>())
        {
            _direction.y = -_direction.y;
        }
    }

    private float CalculateHitFactor(Vector2 ballPosition, Vector2 paddlePosition, float paddleWidth)
    {
        return (ballPosition.x - paddlePosition.x) / paddleWidth;
    }
}
