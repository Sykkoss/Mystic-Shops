using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerTime : MonoBehaviour
{
    public static PlayerTime Instance;
    public delegate void TimerFinishHandler();

    public bool IsPaused { get; private set; }
    public bool IsFinished { get; private set; }

    public event TimerFinishHandler OnTimerFinish;

    private float _currentTime;


    private void Awake()
    {
        Instance = this;
        IsPaused = true;
        IsFinished = false;
    }

    private void Start()
    {
        InitTime(60f);
        StartTimer();
    }

    private void Update()
    {
        DecreaseTime();
    }

    public void InitTime(float maxTime)
    {
        _currentTime = maxTime;
    }

    public void StartTimer()
    {
        IsPaused = false;
    }

    private void PauseTime()
    {
        IsPaused = !IsPaused;
    }

    private void DecreaseTime()
    {
        if (!IsPaused && !IsFinished)
            _currentTime -= Time.deltaTime;
        if (_currentTime <= 0f && !IsFinished)
        {
            _currentTime = 0f;
            IsFinished = true;
            OnTimerFinish?.Invoke();
        }
    }

    public float GetCurrentTime() => _currentTime;
}
