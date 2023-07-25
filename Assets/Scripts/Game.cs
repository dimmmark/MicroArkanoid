using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public enum State
{
    Start,
    Menu,
    Playing,
    Win,
    Lose

}
public class Game : MonoBehaviour
{
    public static Game Instance;
    [SerializeField] private Platform _platform;
    [SerializeField] private Vector3 _offset;
    [SerializeField] private Ball _ball;
    private Ball _startBall;
    [HideInInspector] public List<Ball> BallsList = new List<Ball>();
    [SerializeField] private UI _ui;
    [SerializeField] private PixelsManager _pixelsManager;
    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(gameObject);

    }
    void Start()
    {
        SetBall();
        OnPlayerOnStart();
        Debug.Log(LevelIndex);
       // Debug.Log(CurrentState);
        
    }

    public void SetBall()
    {
         Ball newBall = Pooler.Instance.SpawnFromPool("Ball", _platform.transform.position + _offset, Quaternion.identity).GetComponent<Ball>();
       // Ball newBall = LeanPool.Spawn(_ball, _platform.transform.position + _offset, Quaternion.identity);
         BallsList.Add(newBall);
        if (CurrentState == State.Start)
            _startBall = newBall;
    }
    public void MultiplyBall()
    {
        List<Ball> newBalls = new List<Ball>();

        foreach (Ball ball in BallsList)
        {
            for (int j = 0; j < 2; j++)
            {
                Ball newBall = Pooler.Instance.SpawnFromPool("Ball", ball.transform.position, Quaternion.identity).GetComponent<Ball>();
                newBalls.Add(newBall);
            }
        }

        BallsList.AddRange(newBalls);
    }
    public void RemoveOneBall(Ball ball)
    {
        BallsList.Remove(ball);
        CheckLose();
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0) && CurrentState == State.Start)
        {
            OnPlayerInGame();
        }

        if (CurrentState == State.Playing)
        {
           // Debug.Log(BallsList.Count);
        }
    }
    public State CurrentState { get; private set; }

    public void OnPlayerOnStart()
    {
        Time.timeScale = 1;
        CurrentState = State.Start;
        _pixelsManager.MakeImage();
        Debug.Log(CurrentState.ToString());
    }
    public void OnPlayerInMenu()
    {
        CurrentState = State.Menu;
        Debug.Log(CurrentState.ToString());
    }

    public void OnPlayerInGame()
    {
        CurrentState = State.Playing;
        Debug.Log(CurrentState.ToString());
        if (_startBall)
            _startBall.LaunchBallInStart();

    }

    public void OnPlayerWin()
    {
        CurrentState = State.Win;
        LevelIndex++;
        _ui.ShowWinScrene();
        BallsList.Clear();
        Time.timeScale = 0;
        Debug.Log(CurrentState.ToString());
    }
    public void OnPlayerLose()
    {
        CurrentState = State.Lose;
        _ui.ShowLoseScrene();
        Time.timeScale = 0;
        Debug.Log(CurrentState.ToString());
    }
    public int LevelIndex
    {
        get
        {
            return PlayerPrefs.GetInt("LevelIndex", 0);
        }
        private set
        {
            PlayerPrefs.SetInt("LevelIndex", value);
            PlayerPrefs.Save();
        }
    }
    private void CheckLose()
    {
        if (BallsList.Count <= 0)
            OnPlayerLose();
    }
    public void RestartLevel()
    {
        SceneManager.LoadScene(0);
    }
    private void OnEnable()
    {
        PixelsManager.OnEndPixels += OnPlayerWin;
    }
    private void OnDisable()
    {
        PixelsManager.OnEndPixels -= OnPlayerWin;
    }
}
