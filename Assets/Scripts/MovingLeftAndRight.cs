using UnityEngine;
using UnityEngine.UI;

public class MovingLeftAndRight : MonoBehaviour
{
    public Transform coinPosition;
    public float speed = 5f;
    private bool moveRight = false;
    private bool moveLeft = false;
    public Rigidbody rb;
    public DropTheCoin coinDrop;
    public bool rightLeftPressed = false;
    public Button dropButtonAfterPress;
    public void MovingCoinLeft()
    {
        moveLeft = true;
        rightLeftPressed = true;
        dropButtonAfterPress.interactable = true;
    }

    public void MovingCoinRight()
    {

        moveRight = true;
        rightLeftPressed = true;
        dropButtonAfterPress.interactable = true;
    }

    public void MovingCoinLeftStop()
    {
        moveLeft = false;
        if (!coinDrop.dropButtonPressed)
        {

            rb.constraints = RigidbodyConstraints.FreezePosition;
        }
    }

    public void MovingCoinRightStop()
    {
        moveRight = false;
        if (!coinDrop.dropButtonPressed)
        {

            rb.constraints = RigidbodyConstraints.FreezePosition;
        }
    }

    


    private void Update()
    {
        if (moveLeft && transform.position.x > -548.45 && !coinDrop.dropButtonPressed)
        {
            transform.position = new Vector3(transform.position.x - (speed * Time.deltaTime), transform.position.y);
        }

        else if (moveRight && transform.position.x < 698 && !coinDrop.dropButtonPressed)
        {
            transform.position = new Vector3(transform.position.x + (speed * Time.deltaTime), transform.position.y);
        }
    }

}

