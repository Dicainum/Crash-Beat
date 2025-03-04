using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

[ExecuteAlways]
public class ColorManager : MonoBehaviour
{
    public Color _mainColor;
    public Color _secondaryColor;
    [SerializeField] private SpriteRenderer[] _mainColorSprites;
    [SerializeField] private SpriteRenderer[] _secondaryColorSprites;
    [SerializeField] private SpriteRenderer[] _outline;
    [SerializeField] private TextMeshProUGUI[] _secondaryText;
    [SerializeField] private Image[] _secondaryImage;
    [SerializeField] private Image[] _mainImage;
    [SerializeField] private Outline[] _ouline;
    public Color mainClr;
    public Color secondaryClr;
    public List<EnemyColor> enemies = new List<EnemyColor>();
    
    private void Awake()
    {
        mainClr = _mainColor;
        secondaryClr = _secondaryColor;
        ApplyColors();
    }

    private void LateUpdate()
    {
        if (mainClr != _mainColor || secondaryClr != _secondaryColor)
        {
            mainClr = _mainColor;
            secondaryClr = _secondaryColor;
            ApplyColors();
            ChangeEnemyColors();
        }
    }

    private void OnValidate()
    {
        mainClr = _mainColor;
        secondaryClr = _secondaryColor;
        ApplyColors();
        ChangeEnemyColors();
    }

    public void InverseColors()
    {
        mainClr = _secondaryColor;
        secondaryClr = _mainColor;
        ChangeEnemyColors();
    }

    private void ApplyColors()
    {
        if (_mainColorSprites != null)
        {
            foreach (var sprite in _mainColorSprites)
            {
                if (sprite != null)
                    sprite.color = mainClr;
            }
        }
        if (_secondaryColorSprites != null)
        {
            foreach (var sprite in _secondaryColorSprites)
            {
                if (sprite != null)
                    sprite.color = secondaryClr;
            }
        }

        foreach (var sprite in _outline)
        {
            SetOutlineColor(sprite, secondaryClr);
        }

        foreach (var text in _secondaryText)
        {
            text.color = secondaryClr;
        }

        foreach (var image in _secondaryImage)
        {
            image.color = secondaryClr;
        }
        foreach (var image in _mainImage)
        {
            image.color = mainClr;
        }

        foreach (var outline in _ouline)
        {
            outline.effectColor = secondaryClr;
        }

    }

    public void ChangeEnemyColors()
    {
        if (enemies != null)
        {
            foreach (var enemy in enemies)
            {
                enemy.UpdateColors();
            }
        }
    }
    private void SetOutlineColor(SpriteRenderer spriteRenderer, Color outlineColor)
    {
        if (spriteRenderer.sharedMaterial.HasProperty("_SolidOutline"))
        {
            spriteRenderer.sharedMaterial .SetColor("_SolidOutline", outlineColor);
        }
    }
}