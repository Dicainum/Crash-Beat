using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Saves : MonoBehaviour
{
    public bool _tutorialEnded = false;
    public float _sound = 1f;
    public static Saves saves { get; private set; }

    
    private void Awake()
    {
        DontDestroyOnLoad(gameObject);
        saves = gameObject.GetComponent<Saves>();
    }
}
