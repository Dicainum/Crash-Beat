using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BPMReference : MonoBehaviour
{
    public static BPMReference Bpm { get; private set; }
    public float bpm = 120f;
    
    private void Awake()
    {
        Bpm = gameObject.GetComponent<BPMReference>();
    }
}
