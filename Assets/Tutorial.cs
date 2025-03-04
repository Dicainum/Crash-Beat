using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tutorial : MonoBehaviour
{
    private Saves saves;
    [SerializeField] private AudioSource audio;
    private void Awake()
    {
        saves = Saves.saves;
        if (saves._tutorialEnded)
        {
            gameObject.SetActive(false);
        }
        else
        {
            Time.timeScale = 0;
            audio.Pause();
        }
    }

    public void Resume()
    {
        Time.timeScale = 1;
        audio.UnPause();
        gameObject.SetActive(false);
        saves._tutorialEnded = true;
    }
}
