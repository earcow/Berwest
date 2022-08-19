using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class EventTimer : MonoBehaviour
{
    public UnityEvent TimeIsOver = new UnityEvent();

    private float _delayTime;
    private float _elapsedTime;

    private bool _isRunTimer;

    private UnityAction _listener;

    private void Awake()
    {
        this._isRunTimer = false;
        this._elapsedTime = 0.0f;
    }

    public void SetAndRun(float seconds_delayTime, UnityAction listener)
    {
        this._delayTime = seconds_delayTime;
        this._isRunTimer = true;
        this._listener = listener;

        TimeIsOver.AddListener(_listener);
    }

    private void Update()
    {
        if (!_isRunTimer)
            return;
        
        _elapsedTime += Time.deltaTime;

        if (_elapsedTime > _delayTime)
        {
            TimeIsOver?.Invoke();
            TimeIsOver.RemoveListener(_listener);
            _isRunTimer = false;
            _elapsedTime = 0.0f;
        }
    }


}
