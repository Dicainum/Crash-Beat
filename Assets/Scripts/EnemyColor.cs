using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyColor : MonoBehaviour
{
    private ColorManager colorManager;
    private Color MainColor;
    private Color SecondColor;
    [SerializeField] private SpriteRenderer[] _mainColorSprites;
    [SerializeField] private SpriteRenderer[] _secondColorSprite;
    private EnemyColor _enemyColor;

    private void Start()
    {
        colorManager = ColorManagerReference.ColorManager.GetComponent<ColorManager>();
        MainColor = colorManager.mainClr;
        SecondColor = colorManager.secondaryClr;
        UpdateColors();
        _enemyColor = gameObject.GetComponent<EnemyColor>();
        colorManager.enemies.Add(_enemyColor);
    }
    private void OnDestroy()
    {
        if (colorManager != null && colorManager.enemies.Contains(_enemyColor))
        {
            colorManager.enemies.Remove(_enemyColor);
        }
    }

    public void UpdateColors()
    {
        MainColor = colorManager.mainClr;
        SecondColor = colorManager.secondaryClr;
        foreach (var sprite in _mainColorSprites)
        {
            sprite.color = MainColor;
        }

        foreach (var sprite in _secondColorSprite)
        {
            sprite.color = SecondColor;
        }
    }
}
