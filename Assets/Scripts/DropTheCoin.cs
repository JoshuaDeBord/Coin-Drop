using UnityEngine;

public class DropTheCoin : MovingLeftAndRight
{
    // Start is called before the first frame update

    private Rigidbody rbCoin;

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
