using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PointsManager : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI pointsText;
    [SerializeField] private TextMeshProUGUI comboText;
    [SerializeField] private int _maxCombo;
    private int _points;
    private int _combo;

    private void Awake()
    {
        pointsText.text = "";
        comboText.text = "";
    }

    public void GetPoints(int points)
    {
        if (_combo > 0)
        {
            _points += points * _combo;
        }
        else
        {
            _points += points;
        }
        pointsText.text = _points.ToString() + " pts";
    }

    public void GetCombo()
    {
        if (_combo < _maxCombo)
        {
            _combo++;
            comboText.text = "x" + _combo.ToString(); 
        }
    }

    public void LoseCombo()
    {
        if(_combo > 0)
        {
            if (_combo == _maxCombo)
            {
                _combo = _maxCombo/2;
            }
            else
            {
                _combo = 1;
            }
            comboText.text = "x" + _combo.ToString(); 
        }
    }
}
