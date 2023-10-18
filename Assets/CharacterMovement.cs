using System;
using System.Collections;
using System.Threading;
using System.Threading.Tasks;
using UnityEngine;

public class CharacterMovement : MonoBehaviour
{
    [SerializeField] private Input _input;
    [SerializeField] private CharacterAnimation _characterAnimation;
    [SerializeField] private Path _path;
    [SerializeField] private float _speed;
    private Vector3 _currentPosition;
    private Vector3 _lastPosition;
    private float _deltaTime;

    private void Awake()
    {
        _currentPosition = _path.Points[0].Position;
    }

    private async void Start()
    {
        await Task.Run(() => MoveAlongPath(MoveCompleted, _input.SpaceCancel.Token), destroyCancellationToken);
    }

    private async void MoveAlongPath(Action onCompleteCallback, CancellationToken token)
    {
        int currentPointIndex = 0;
        _characterAnimation.AnimateByMove(_path.Points[currentPointIndex].Position - _currentPosition);
        
        while (currentPointIndex != _path.Points.Count)
        {
            if (token.IsCancellationRequested)
            {
                _characterAnimation.Idle();
                return;
            }
            
            _currentPosition = Vector3.MoveTowards(_currentPosition, _path.Points[currentPointIndex].Position,
                _speed * _deltaTime);

            if (_currentPosition == _path.Points[currentPointIndex].Position)
            {
                currentPointIndex++;
                
                if (currentPointIndex >= _path.Points.Count)
                {
                    onCompleteCallback?.Invoke();
                    return;
                }
                    
                _characterAnimation.AnimateByMove(_path.Points[currentPointIndex].Position - _currentPosition);
            }

            await Task.Delay((int)(_deltaTime * 1000));
        }
    }

    private void MoveCompleted()
    {
        _characterAnimation.Idle();
    }

    private void Update()
    {
        if (_currentPosition != _lastPosition)
        {
            transform.position = _currentPosition;
            _lastPosition = _currentPosition;
        }

        _deltaTime = Time.deltaTime;
    }
}