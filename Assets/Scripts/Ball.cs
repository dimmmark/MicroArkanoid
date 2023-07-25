using UnityEngine;

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
    private int _frames;
    private float _randomInt;
    private void Start()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
        if (Game.Instance.CurrentState == State.Playing)
            LaunchBallInGame();
    }

    private void FixedUpdate()
    {
        _frames++;
        //  _rigidbody.MovePosition(transform.position + new Vector3(_direction.x, _direction.y, 0) * _speed * Time.deltaTime);
        _rigidbody.velocity = _direction * _speed;
    }

    public void LaunchBallInStart()
    {
        float randomAngle = Random.Range(-15f, 15f);
        _direction = Quaternion.Euler(0f, 0f, randomAngle) * Vector2.up;
    }
    public void LaunchBallInGame()
    {
        float randomAngle = Random.Range(-360f, 360f);
        _direction = Quaternion.Euler(0f, 0f, randomAngle) * Vector2.up;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
       
        _randomInt = Random.Range(-3f,3f);
        _direction = Quaternion.Euler(0f, 0f, _randomInt) * _direction;

        if (collision.gameObject.GetComponent<Platform>())
        {
            float hitFactor = CalculateHitFactor(transform.position, collision.transform.position, collision.collider.bounds.size.x);
            _direction = new Vector2(hitFactor, 1f).normalized;
        }

        _vectorAtract = collision.GetContact(0).normal;
        _vectorAtract.x = Mathf.Round(_vectorAtract.x);
        _vectorAtract.y = Mathf.Round(_vectorAtract.y);


        if (collision.gameObject.TryGetComponent<Pixel>(out Pixel pixel))
        {
            
           // Debug.Log(_vectorAtract);
            if (_vectorAtract.x ==0)
            {
                _direction.y = -_direction.y;
            }
            if (_vectorAtract.y ==0)
            {
                _direction.x = -_direction.x;
            }
            if (_frames > 2)
            {
                SoundManager.Instance.Play("Hit");
                OnCollidedPixel.Invoke(pixel);
                pixel.gameObject.SetActive(false);
                _frames = 0;
            }
        }
         if ( collision.gameObject.GetComponent<IronPixel>())
        {

            //_direction = Vector2.Reflect(_direction, _vectorAtract).normalized;
            if (_frames > 1)
            {
                if (_vectorAtract.x == 0)
            {
                _direction.y = -_direction.y;
            }
            if (_vectorAtract.y == 0)
            {
                _direction.x = -_direction.x;
            }
                _frames = 0;
            }
        }
        if (collision.gameObject.GetComponent<Bounds>() || collision.gameObject.GetComponent<BoundTop>())
        {
            if (_vectorAtract.x == 0)
            {
                _direction.y = -_direction.y;
            }
            if (_vectorAtract.y == 0)
            {
                _direction.x = -_direction.x;
            }
        }
    }

    private float CalculateHitFactor(Vector2 ballPosition, Vector2 paddlePosition, float paddleWidth)
    {
        return (ballPosition.x - paddlePosition.x) / paddleWidth*2.5f;
    }
}
