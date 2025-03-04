using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameTimer : MonoBehaviour
{
    [SerializeField] private float TimeToEnd = 180f;
    [SerializeField] private GameObject EndScreen;
    [SerializeField] private AudioSource audioSource;
    private void FixedUpdate()
    {
        TimeToEnd -= Time.fixedDeltaTime;
        if (TimeToEnd <= 0f)
        {
            audioSource.Stop();
            Time.timeScale = 0f;
            EndScreen.SetActive(true);
        }
    }
}
