using UnityEngine;

public class PlayerReference : MonoBehaviour
{
    [SerializeField] private Transform left;
    [SerializeField] private Transform right;
    public static GameObject Player { get; private set; }
    public static Transform Left { get; private set; }
    public static Transform Right { get; private set; }

    
    private void Awake()
    {
        Player = gameObject;
        Left = left;
        Right = right;
    }
}