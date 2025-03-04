using System;
using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;
using System.Collections.Generic;
using Random = UnityEngine.Random;


public class PlayerController : MonoBehaviour
{
    [SerializeField] private GameObject leftSwing;
    [SerializeField] private GameObject rightSwing;
    [SerializeField] private GameObject _badPf;
    [SerializeField] private Transform _textTransform;
    private InputAction swingLeftAction;
    private InputAction swingRightAction;
    private InputAction soulFire;
    [SerializeField] private float swingDuration = 0.1f;
    [SerializeField] private float swingCooldown = 0.1f;
    [SerializeField] private float _cooldownPrecentage = 0.5f;
    [SerializeField] private SpriteRenderer[] idleSprites; 
    [SerializeField] private SpriteRenderer[] attackSprites;
    [SerializeField] private Animator _slashLeft;
    [SerializeField] private Animator _slashRight;
    [SerializeField] private PointsManager _pointsManager;
    [SerializeField] private HealthController _health;
    [SerializeField] private Animator _soulFireAnimator;
    [SerializeField] private Color[] _mainColors;
    [SerializeField] private Color[] _secondaryColors;
    [SerializeField] private AudioSource _flameAudio;
    [SerializeField] private AudioSource[] _attackAudio;
    private ColorManager _colorManager;
    private bool _cooldown = false;
    public bool dead = false;

    private void Awake()
    {
        ActivateIdle();
        swingLeftAction = new InputAction("SwingLeft", binding: "<Keyboard>/a");
        swingRightAction = new InputAction("SwingRight", binding: "<Keyboard>/d");
        soulFire = new InputAction("SoulFire", binding: "<Keyboard>/s");
    }

    private void Start()
    {
        _colorManager = ColorManagerReference.ColorManager.GetComponent<ColorManager>();
        var bpm = BPMReference.Bpm.bpm;
        swingCooldown = (60f / bpm)*_cooldownPrecentage;
    }

    private void OnEnable()
    {
        swingLeftAction.Enable();
        swingRightAction.Enable();
        soulFire.Enable();
        swingLeftAction.performed += OnSwingLeft;
        swingRightAction.performed += OnSwingRight;
        soulFire.performed += OnSoulFire;
    }

    private void OnDisable()
    {
        swingLeftAction.performed -= OnSwingLeft;
        swingRightAction.performed -= OnSwingRight;
        soulFire.performed -= OnSoulFire;
        swingLeftAction.Disable();
        swingRightAction.Disable();
        soulFire.Disable();
    }

    private void ActivateIdle()
    {
        
        foreach (var sprite in attackSprites)
        {
            sprite.enabled = false;
        }
        
        foreach (var sprite in idleSprites)
        {
            sprite.enabled = true;
        }

    }

    private void ActivateAttacking()
    {
        var random = Random.Range(0, _attackAudio.Length);
        _attackAudio[random].Play();
        foreach (var sprite in idleSprites)
        {
            sprite.enabled = false;
        }
        
        foreach (var sprite in attackSprites)
        {
            sprite.enabled = true;
        }

    }

    private void OnSoulFire(InputAction.CallbackContext context)
    {
        if (!dead)
        {
            if (_cooldown)
            {
                //_soulFireAnimator.SetTrigger("Flame");
                var text = Instantiate(_badPf, _textTransform.position, Quaternion.identity);
                if (text != null)
                {
                    TextMeshPro tmpComponent = text.GetComponent<TextMeshPro>();
                    tmpComponent.color = _colorManager.secondaryClr;
                }
                _pointsManager.LoseCombo();
                _health.TakeDamage();
            }
            else
            {
                _soulFireAnimator.SetTrigger("Flame");
                _flameAudio.Play();
                _pointsManager.GetCombo();
                _health.GetHeal();
                StartCoroutine(Cooldown());
            } 
        }
    }

    public void ChangeColors()
    {
        var random = Random.Range(0, _mainColors.Length);
        _colorManager._mainColor = _mainColors[random];
        _colorManager._secondaryColor = _secondaryColors[random];
    }

    private void OnSwingLeft(InputAction.CallbackContext context)
    {
        if (!dead)
        {
            if (_cooldown)
            {
                var text = Instantiate(_badPf, _textTransform.position, Quaternion.identity);
                if (text != null)
                {
                    TextMeshPro tmpComponent = text.GetComponent<TextMeshPro>();
                    tmpComponent.color = _colorManager.secondaryClr;
                }
                _pointsManager.LoseCombo();
                _health.TakeDamage();
            }
            else
            {
                _cooldown = true;
                StartCoroutine(Cooldown());
                foreach (var sprite in idleSprites)
                {
                    sprite.flipX = true;
                }
                foreach (var sprite in attackSprites)
                {
                    sprite.flipX = true;
                }
                ActivateAttacking();
                _slashLeft.SetTrigger("Slash");
                if (leftSwing != null)
                {
                    leftSwing.SetActive(true);
                    StartCoroutine(DisableAfterDelay(leftSwing, swingDuration));
                }
            }
        }
    }

    private IEnumerator Cooldown()
    {
        yield return new WaitForSeconds(swingCooldown);
        _cooldown = false;
    }

    private void OnSwingRight(InputAction.CallbackContext context)
    {
        if (!dead)
        {
            if (_cooldown)
            {
                var text = Instantiate(_badPf, _textTransform.position, Quaternion.identity);
                if (text != null)
                {
                    TextMeshPro tmpComponent = text.GetComponent<TextMeshPro>();
                    tmpComponent.color = _colorManager.secondaryClr;
                }
                _pointsManager.LoseCombo();
                _health.TakeDamage();
            }
            else
            {
                _cooldown = true;
                StartCoroutine(Cooldown());
                foreach (var sprite in idleSprites)
                {
                    sprite.flipX = false;
                }

                foreach (var sprite in attackSprites)
                {
                    sprite.flipX = false;
                }

                ActivateAttacking();
                _slashRight.SetTrigger("Slash");
                if (rightSwing != null)
                {
                    rightSwing.SetActive(true);
                    StartCoroutine(DisableAfterDelay(rightSwing, swingDuration));
                }
            } 
        }
    }

    private IEnumerator DisableAfterDelay(GameObject obj, float delay)
    {
        yield return new WaitForSeconds(delay);
        obj.SetActive(false);
        ActivateIdle();
    }
    private void OnTriggerEnter2D (Collider2D col)
    {
        if (col.CompareTag("Enemy"))
        {
            _health.TakeDamage();
            _pointsManager.LoseCombo();
            col.gameObject.SetActive(false);
        }
    }
}