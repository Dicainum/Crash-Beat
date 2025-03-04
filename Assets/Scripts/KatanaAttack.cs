using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class KatanaAttack : MonoBehaviour
{
    private bool hasAttacked = false;
    private Collider2D col;

    [SerializeField] private float _perfectDistance = 0.1f;
    [SerializeField] private float _goodDistance = 0.3f;
    [SerializeField] private float _okayDistance = 0.5f;
    [SerializeField] private Transform _textPlace;
    [SerializeField] private GameObject _perfectTextPrefab;
    [SerializeField] private GameObject _goodTextPrefab;
    [SerializeField] private PointsManager _pointsManager;
    [SerializeField] private int _perfectPoints = 5;
    [SerializeField] private int _goodPoints = 2;
    [SerializeField] private int _okayPoints = 1;
    [SerializeField] private HealthController _health;
    [SerializeField] private bool _isLeft = false;
    [SerializeField] private MoveController _moveController;
    [SerializeField] private ColorManager _colorManager;

    private void OnEnable()
    {
        hasAttacked = false;
        col = GetComponent<Collider2D>();
        Attack();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (!hasAttacked && other.CompareTag("Enemy"))
        {
            Attack();
        }
    }

    private void Attack()
    {
        if (PlayerReference.Player == null) return;

        List<Collider2D> results = new List<Collider2D>();
        ContactFilter2D filter = new ContactFilter2D
        {
            useTriggers = true,
            useLayerMask = false
        };
        col.OverlapCollider(filter, results);

        GameObject closestEnemy = null;
        float minDistance = float.MaxValue;

        foreach (Collider2D c in results)
        {
            if (c.CompareTag("Enemy"))
            {
                float dist = Vector2.Distance(PlayerReference.Player.transform.position, c.transform.position);
                if (dist < minDistance)
                {
                    minDistance = dist;
                    closestEnemy = c.gameObject;
                }
            }
        }

        if (closestEnemy != null)
        {
            CheckDistance(closestEnemy.transform.position);
            var enemyController = closestEnemy.GetComponent<EnemyController>();
            enemyController.Death();
            hasAttacked = true;
        }
    }

    private void CheckDistance(Vector2 enemyPosition)
    {
        if (enemyPosition != null)
        {
            float distance = Vector2.Distance(gameObject.transform.position, enemyPosition);
            Debug.Log(distance);
            if (distance <= _perfectDistance)
            {
                var text = Instantiate(_perfectTextPrefab, _textPlace.position, Quaternion.identity);
                var tmpComponent = text.GetComponent<TMP_Text>();
                
                if (tmpComponent != null)
                {
                    tmpComponent.color = _colorManager.secondaryClr;
                }
                _pointsManager.GetPoints(_perfectPoints);
                _pointsManager.GetCombo();
                if (_isLeft)
                {
                    _moveController.MovePlayerLeft();
                }
                else
                {
                    _moveController.MovePlayerRight();
                }
                return;
            }
            else if (distance <= _goodDistance)
            {
                var text = Instantiate(_goodTextPrefab, _textPlace.position, Quaternion.identity);
                if (text != null)
                {
                    var tmpComponent = text.GetComponent<TMP_Text>();
                    tmpComponent.color = _colorManager.secondaryClr;
                }
                if (_isLeft)
                {
                    _moveController.MovePlayerLeft();
                }
                else
                {
                    _moveController.MovePlayerRight();
                }
                _pointsManager.GetPoints(_goodPoints);
                Debug.Log("Good");
                _pointsManager.GetCombo();
                return;
            }
            else if (distance <= _okayDistance)
            {
                _pointsManager.GetPoints(_okayPoints);
                Debug.Log("Okay");
                _pointsManager.GetCombo();
                if (_isLeft)
                {
                    _moveController.MovePlayerLeft();
                }
                else
                {
                    _moveController.MovePlayerRight();
                }
                return;
            }
            else
            {
                _pointsManager.LoseCombo();
                _health.TakeDamage();
            } 
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(gameObject.transform.position, _perfectDistance);

        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(gameObject.transform.position, _goodDistance);

        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(gameObject.transform.position, _okayDistance);
    }
}
