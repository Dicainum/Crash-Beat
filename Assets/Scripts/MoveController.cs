using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveController : MonoBehaviour
{
    [SerializeField] private GameObject _player;
    [SerializeField] private GameObject _camera;
    [SerializeField] private GameObject[] _enemy;
    [SerializeField] private GameObject _background;
    [SerializeField] private float _speed;
    [SerializeField] private float _time = 1f;
    [SerializeField] private float _delayToMoveEnemy;

    public void MovePlayerLeft()
    {
        var targetPosition = new Vector3(_player.transform.position.x - _speed, _player.transform.position.y, _player.transform.position.z);
        _player.transform.position = Vector3.Lerp(_player.transform.position, targetPosition, _time);
        
        /*
        foreach (var enemy in _enemy)
        {
            var enemyPos = new Vector3(enemy.transform.position.x - _speed, enemy.transform.position.y, enemy.transform.position.z);
            enemy.transform.position = enemyPos;
        }*/
        //StartCoroutine(MoveEnemies(-_speed));
    }
    
    public void MovePlayerRight()
    {
        Vector3 targetPosition = new Vector3(_player.transform.position.x + _speed, _player.transform.position.y, _player.transform.position.z);
        _player.transform.position = Vector3.Lerp(_player.transform.position, targetPosition, _time);
       
        /*foreach (var enemy in _enemy)
        {
            var enemyPos = new Vector3(enemy.transform.position.x + _speed, enemy.transform.position.y, enemy.transform.position.z);
            enemy.transform.position = enemyPos;
        }*/
    }

    private IEnumerator MoveEnemies(float speed)
    {
        yield return new WaitForSeconds(_delayToMoveEnemy);
        foreach (var enemy in _enemy)
        {
            enemy.transform.position = new Vector3(enemy.transform.position.x + speed, enemy.transform.position.y, enemy.transform.position.z);
        }
    }
    
}
