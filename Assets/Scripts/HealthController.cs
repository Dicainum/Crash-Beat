using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UIElements;

public class HealthController : MonoBehaviour
{
    [SerializeField] private Scrollbar _scrollbar;
    [SerializeField] private GameObject _scrollbarHP;
    private int _maxHP = 100;
    [SerializeField] private int damage = 20;
    [SerializeField] private int _currentHP;
    [SerializeField] private GameObject _deathScreen;
    [SerializeField] private AudioSource _bgm;
    [SerializeField] private GameObject enemies;
    [SerializeField] private GameObject[] spawners;
    [SerializeField] private PlayerController _player;
    private void Awake()
    {
        _currentHP = _maxHP;
        UpdateUI();
    }

    private void UpdateUI()
    {
        if (_currentHP == _maxHP)
        {
            _scrollbarHP.SetActive(false);
        }
        else
        {
            _scrollbarHP.SetActive(true);
        }
        var curHealth = (float)_currentHP;
        var mxHP = (float)_maxHP;
        _scrollbar.size = 1 - (curHealth/mxHP);
        Debug.Log(curHealth/mxHP);
    }

    public void TakeDamage()
    {
        _currentHP -= damage;
        if (_currentHP <= 0)
        {
            _currentHP = 0;
            Death();
            _scrollbar.size = 1;
        }
        else
        {
            UpdateUI();
        }
    }

    public void GetHeal()
    {
        _currentHP += damage;
        if (_currentHP >= _maxHP)
        {
            _currentHP = _maxHP;
            UpdateUI();
        }
        else
        {
            UpdateUI();
        }
    }
    
    private void Death()
    {
        _bgm.Stop();
        _deathScreen.SetActive(true);
        Destroy(enemies);
        _player.dead = true;
        foreach (var spawner in spawners)
        {
            Destroy(spawner);
        }
        
    }
}
