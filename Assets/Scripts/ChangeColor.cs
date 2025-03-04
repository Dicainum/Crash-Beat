using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeColor : MonoBehaviour
{
    [SerializeField] private PlayerController player;

    public void ChangeGameColor()
    {
        player.ChangeColors();
    }
}
