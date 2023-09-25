using UnityEngine;

public class DropTheCoin : MovingLeftAndRight
{
    
    
    public Rigidbody rbCoin;
    

    public bool dropButtonPressed = false;
    private void Start()
    {
        rbCoin = GetComponent<Rigidbody>();
    }
    public void DropCoin()
    {
        
        dropButtonPressed = true;
        rbCoin.useGravity = true;

        rbCoin.constraints = RigidbodyConstraints.None;
        rbCoin.constraints = RigidbodyConstraints.FreezeRotationZ | RigidbodyConstraints.FreezeRotationX;


    }
}
