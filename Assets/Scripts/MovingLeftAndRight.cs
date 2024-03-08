using UnityEngine;
using UnityEngine.UI;
using UnityEngine.InputSystem;


public class MovingLeftAndRight : MonoBehaviour
{
    public Transform coinPosition;
    public float speed = 20f;
    public bool moveRight = false;
    public bool moveLeft = false;
    public Rigidbody rb;
    public PrideColorLoop coinDrop;
    public GameManager manager;
    public GameObject leftBorder, rightBorder;

    public bool rightLeftPressed = false;
    public Button dropButtonAfterPress;


    private void Start()
    {


    }

    private void Update()
    {

        if (moveLeft && transform.position.x > leftBorder.transform.position.x + 3.83f && !manager.dropButtonPressed || moveLeft && transform.position.x > leftBorder.transform.position.x + 3.83f && manager.rapidSpawn == true)
        {

            transform.position = new Vector3(transform.position.x - (speed * Time.deltaTime), transform.position.y, transform.position.z);
        }

        else if (moveRight && transform.position.x < rightBorder.transform.position.x - 3.71f && !manager.dropButtonPressed || moveRight && transform.position.x < rightBorder.transform.position.x - 3.71f && manager.rapidSpawn == true)
        {
            transform.position = new Vector3(transform.position.x + (speed * Time.deltaTime), transform.position.y, transform.position.z);
        }



    }


    public void MoveCoin(InputAction.CallbackContext context)
    {
        if (context.performed == true && context.ReadValue<Vector2>().x > 0)
        {
            MovingCoinRight();
            MovingCoinLeftStop();
        }

        if (context.performed == true && context.ReadValue<Vector2>().x < 0)
        {
            MovingCoinLeft();
            MovingCoinRightStop();
        }




        if (context.canceled == true)
        {
            MovingCoinLeftStop();
            MovingCoinRightStop();
        }
    }
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






}

