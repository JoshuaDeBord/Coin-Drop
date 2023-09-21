using UnityEngine;

public class DropTheCoin  : MovingLeftAndRight
{
    // Start is called before the first frame update

    private Rigidbody rbCoin;
    private bool isntDropped = true;
    private void Start()
    {
        rbCoin = GetComponent<Rigidbody>();
    }
    public void DropCoin()
    {

        rbCoin.useGravity = true;

        rbCoin.constraints = RigidbodyConstraints.None;
        rbCoin.constraints = RigidbodyConstraints.FreezeRotationZ | RigidbodyConstraints.FreezeRotationX;
        

    }
}
