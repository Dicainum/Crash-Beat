using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Score : MonoBehaviour
{
    [SerializeField] private TMP_Text scoreText;
    [SerializeField] private TMP_Text _finalScoreText;

    private void Awake()
    {
        _finalScoreText.text = "SCORE" + scoreText.text;
    }
}
