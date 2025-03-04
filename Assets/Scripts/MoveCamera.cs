using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveCamera : MonoBehaviour
{
    [SerializeField] private bool _isLeft = false;
    [SerializeField] private GameObject _camera;
    [SerializeField] private GameObject _player;
    [SerializeField] private float _time = 3f;
    private Vector3 _targetPosition;
    private bool _canMove = false;
    private float timePassed = 0f;
    private void Update()
    {
        if (_canMove)
        {
            _camera.transform.position = Vector3.Lerp(_camera.transform.position, _targetPosition, _time*Time.deltaTime);
            timePassed += Time.deltaTime;
            if (timePassed >= _time)
            {
                _canMove = false;
                _targetPosition = Vector3.zero;
                timePassed = 0f;
            }
        }

    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
             _targetPosition = new Vector3(_player.transform.position.x, _camera.transform.position.y, _camera.transform.position.z);
             _canMove = true;
        }
    }
}
