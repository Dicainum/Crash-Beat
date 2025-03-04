using System;
using System.Collections;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    [Header("Movement Settings")]
    [SerializeField] private float bpm = 120;
    [SerializeField] private float moveSpeed = 0.5f;
    [SerializeField] private bool facePlayer = true;
    [SerializeField] private GameObject _firstGO;
    [SerializeField] private GameObject _firstGO_1;
    [SerializeField] private GameObject _secondGO;
    [SerializeField] private GameObject _secondGO_1;
    [SerializeField] private Animator _animator;
    [SerializeField] private bool _isBes = false;
    [SerializeField] private bool _isBoss = false;
    private int hit = 0;
    private Transform player;
    private Transform leftTransform;
    private Transform rightTransform;
    private float moveInterval;
    private float timeSinceLastMove;
    private Vector2 moveDirection;
    private Transform _left;
    private Transform _right;

    void Start()
    {
        bpm = BPMReference.Bpm.bpm;
        player = PlayerReference.Player.transform;
        _left = PlayerReference.Left;
        _right = PlayerReference.Right;
        //leftTransform = rightTransform;
        CalculateMoveInterval();
        timeSinceLastMove = 0f;
    }

    void FixedUpdate()
    {
        if(player == null) return;

        timeSinceLastMove += Time.fixedDeltaTime;

        if(timeSinceLastMove >= moveInterval)
        {
            UpdateMovement();
            timeSinceLastMove = 0f;
        }
    }

    void UpdateMovement()
    {
        // Рассчитываем направление к игроку
        moveDirection = (player.position - transform.position).normalized;
        
        // Двигаем только по оси X
        Vector2 move = new Vector2(moveDirection.x * moveSpeed, 0f);
        transform.Translate(move, Space.World);
        
        // Поворачиваем спрайт
        if(facePlayer) UpdateFacing();
        SwapTiles();
    }

    private void SwapTiles()
    {
        if (_firstGO.activeSelf)
        {            
            _firstGO.SetActive(false);
            _firstGO_1.SetActive(false);
            _secondGO.SetActive(true);
            _secondGO.SetActive(true);
        }
        else
        {
            _secondGO.SetActive(false);
            _secondGO_1.SetActive(false);
            _firstGO.SetActive(true);
            _firstGO_1.SetActive(true);
        }
    }

    public void Death()
    {
        if (_isBes)
        {
            hit++;
            if (hit > 1)
            {
                _animator.SetTrigger("Death");
                //StartCoroutine(DeathTimer());
            }
            else
            {
                TeleportBehind();
                UpdateMovement();
                UpdateFacing();
            }
        }

        if (_isBoss)
        {
            hit++;
            if (hit > 1)
            {
                _animator.SetTrigger("Death");
                //StartCoroutine(DeathTimer());
            }
            else
            {
                MoveBehind();
            }
        }
        if(!_isBoss && !_isBes)
        {
            _animator.SetTrigger("Death");
            //StartCoroutine(DeathTimer());
        }
    }

    private void MoveBehind()
    {
        float distanceToLeft = Vector2.Distance(transform.position, _left.position);
        float distanceToRight = Vector2.Distance(transform.position, _right.position);

        if (distanceToLeft > distanceToRight)
        {
            transform.position = new Vector3(transform.position.x + moveSpeed * 2,transform.position.y,transform.position.z);
        }
        else
        {
            transform.position = new Vector3(transform.position.x - moveSpeed * 2,transform.position.y,transform.position.z);
        }
    }

    private void TeleportBehind()
    {
        float distanceToLeft = Vector2.Distance(transform.position, _left.position);
        float distanceToRight = Vector2.Distance(transform.position, _right.position);

        if (distanceToLeft > distanceToRight)
        {
            transform.position = new Vector3(_left.position.x - moveSpeed,transform.position.y,transform.position.z);
        }
        else
        {
            transform.position = new Vector3(_right.position.x + moveSpeed,transform.position.y,transform.position.z);
        }
    }

    public void Destroy()
    {
        Destroy(gameObject);

    }

    private IEnumerator DeathTimer()
    {
        yield return new WaitForSeconds(2f);
        Destroy(gameObject);
    }
    void CalculateMoveInterval()
    {
        moveInterval = 60f / bpm;
    }

    void UpdateFacing()
    {
        if (moveDirection.x > 0)
        {
            transform.localScale = new Vector3(1, 1, 1);
        }
        else if (moveDirection.x < 0)
        {
            transform.localScale = new Vector3(-1, 1, 1);
        }
    }

    public void SetBPM(float newBPM)
    {
        bpm = newBPM;
        CalculateMoveInterval();
        timeSinceLastMove = 0f; // Сброс таймера при изменении BPM
    }

    void OnDrawGizmos()
    {
        if (player != null)
        {
            Gizmos.color = Color.red;
            Gizmos.DrawLine(transform.position, player.position);
        }
    }
}