using UnityEngine;

public class MovingLeftAndRight : MonoBehaviour
{
    public Transform coinPosition;
    public float speed = 5f;
    private bool moveRight = false;
    private bool moveLeft = false;
    public Rigidbody rb;
    public DropTheCoin coinDrop;
    public void MovingCoinLeft()
    {
        moveLeft = true;
        

    }

    public void MovingCoinRight()
    {

        moveRight = true;

    }

    public void MovingCoinLeftStop()
    {
        moveLeft = false;
        rb.constraints = RigidbodyConstraints.FreezePosition;

    }

    public void MovingCoinRightStop()
    {
        moveRight = false;
        rb.constraints = RigidbodyConstraints.FreezePosition;
    }

    

    private void Update()
    {
        if (moveLeft && transform.position.x > -5.38 && !coinDrop.dropButtonPressed)
        {
            transform.position = new Vector3(transform.position.x - (speed * Time.deltaTime), transform.position.y);
        }

        else if (moveRight && transform.position.x < 25.82 && !coinDrop.dropButtonPressed)
        {
            transform.position = new Vector3(transform.position.x + (speed * Time.deltaTime), transform.position.y);
        }
    }

}

