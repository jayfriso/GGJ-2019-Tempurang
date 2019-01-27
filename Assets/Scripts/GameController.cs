using UnityEngine;
using System.Collections;
using System;

public class GameController : MonoBehaviour
{
    public Action onGameStart;
    public Action onGameEnd;
    public Action<int> onPointUpdate;
    public Action<float> onTimeUpdate;

    [Header("Game Config")]
    [SerializeField]
    private float _gameTime;

    [Header("Components")]
    [SerializeField]
    private UIController _uiController;
    [SerializeField]
    private PlayerController _playerController;

    private bool _isGameRunning = false;
    private int _points = 0;
    private float currentTime = 0f;

    // Use this for initialization
    void Start()
    {
        Init();
    }

    private void Init()
    {
        _uiController.Init(this);
        _playerController.Init(this, _uiController);
    }

    // Update is called once per frame
    void Update()
    {
        if (_isGameRunning)
        {
            currentTime += Time.deltaTime;
            onTimeUpdate(_gameTime - currentTime);
            if (currentTime >= _gameTime)
            {
                EndGame();
            }
        }
    }

    public void IncremementPoints() 
    { 
        _points++;
        if (onPointUpdate != null)
            onPointUpdate.Invoke(_points);
    }

    public void StartGame()
    {
        _isGameRunning = true;
        currentTime = 0f;
        _isGameRunning = true;
        _playerController.StartGame();

        if (onGameStart != null)
            onGameStart.Invoke();
    }
    public void EndGame()
    {
        _isGameRunning = false;

        if (onGameEnd != null)
            onGameEnd.Invoke();
    }
}
