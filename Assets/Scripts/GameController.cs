using UnityEngine;
using System.Collections;
using System;

public class GameController : MonoBehaviour
{
    public Action onGameStart;
    public Action onGameEnd;
    public Action<int> onPointUpdate;

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
        _playerController.Init(this);
    }

    // Update is called once per frame
    void Update()
    {
        currentTime += Time.deltaTime;
        if (currentTime >= _gameTime)
        {
            EndGame();
        }
    }

    public void StartGame()
    {
        _isGameRunning = true;
        currentTime = 0f;

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
