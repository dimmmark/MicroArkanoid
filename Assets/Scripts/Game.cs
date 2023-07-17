using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
    private Ball _startBall;
    [SerializeField] private List<Ball> _ballsList = new List<Ball>();
    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(gameObject);
        OnPlayerOnStart();
    }
    void Start()
    {
        Invoke(nameof(SetBall), .1f);
    }

    public void SetBall()
    {
       Ball newBall = Pooler.Instance.SpawnFromPool("Ball", _platform.transform.position + _offset, Quaternion.identity).GetComponent<Ball>();
        _ballsList.Add(newBall);
        if(CurrentState == State.Start)
            _startBall = newBall;
    }
    public void RemoveOneBall(Ball ball)
    {
        _ballsList.Remove(ball);
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0)&& CurrentState == State.Start)
        {
            OnPlayerInGame();
        }

        if (CurrentState == State.Playing)
        {
          
        }
    }
    public State CurrentState { get; private set; }

    public void OnPlayerOnStart()
    {
        CurrentState = State.Start;
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
       if(_startBall)
            _startBall.LaunchBall();

    }

    public void OnPlayerWin()
    {
            Debug.Log(CurrentState.ToString());
    }
    public void OnPlayerLose()
    {
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
}
