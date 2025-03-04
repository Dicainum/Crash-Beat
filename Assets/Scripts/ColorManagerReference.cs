using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColorManagerReference : MonoBehaviour
{
    public static GameObject ColorManager { get; private set; }
    
    private void Awake()
    {
        ColorManager = gameObject;
    }
}
