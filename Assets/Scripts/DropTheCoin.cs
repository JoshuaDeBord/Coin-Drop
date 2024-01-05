using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.ProBuilder.MeshOperations;

public class DropTheCoin : MovingLeftAndRight
{
    public GameObject[] modelSelect;
    public Rigidbody rbCoin;
    public bool useGravityIf = false;

    
    private void Start()
    {
        rbCoin = GetComponent<Rigidbody>();
    }
    
}
