using UnityEngine;
using UnityEngine.UI;

public class MovingLeftAndRight : MonoBehaviour
{
    public Transform coinPosition;
    public float speed = 5f;
    public bool moveRight = false;
    public bool moveLeft = false;
    public Rigidbody rb;
    public DropTheCoin coinDrop;
    public GameManager manager;
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
        if (!manager.dropButtonPressed)
        {

            rb.constraints = RigidbodyConstraints.FreezePosition | RigidbodyConstraints.FreezeRotation;
            
        }
    }

    public void MovingCoinRightStop()
    {
        moveRight = false;
        if (!manager.dropButtonPressed)
        {

            rb.constraints = RigidbodyConstraints.FreezePosition | RigidbodyConstraints.FreezeRotation;
        }
    }

    


    private void Update()
    {
        
        if (moveLeft && transform.position.x > -44 && !manager.dropButtonPressed)
        {
            
            transform.position = new Vector3(transform.position.x - (speed * Time.deltaTime), transform.position.y, transform.position.z);
        }

        else if (moveRight && transform.position.x < 79.94f && !manager.dropButtonPressed)
        {
            transform.position = new Vector3(transform.position.x + (speed * Time.deltaTime), transform.position.y, transform.position.z);
        }

        
    }

}

