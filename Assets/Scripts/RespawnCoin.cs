using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class RespawnCoin : MonoBehaviour
{
    public Vector3 respawnPos;
    public Transform CoinMain;
    public DropTheCoin DropTheCoin;
    public GameManager gameManager;
    private MovingLeftAndRight MovingLAR;


    public Button left, right, drop;

    private void Start()
    {
        respawnPos = transform.position;
        MovingLAR = GetComponent<MovingLeftAndRight>();
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();


    }

    public void RespawnTheCoinCall(InputAction.CallbackContext context)
    {
        RespawnTheCoin();
        MovingLAR.rightLeftPressed = false;
    }

    public void RespawnTheCoin()
    {
        if (gameManager.dropButtonPressed == true)
        {
            Debug.Log("Coin Reseted");
            RestartGame();
        }


    }

    public void RestartGame()
    {




        gameManager.dropButtonPressed = false;
        CoinMain.GetComponent<Rigidbody>().velocity = Vector3.zero;
        try
        {
            CoinMain.GetComponent<Rigidbody>().useGravity = false;
            transform.position = respawnPos;
            gameManager.modelSelect[0].transform.localPosition = new Vector3(0, 0, 0);
            gameManager.rbCoin[0].constraints = RigidbodyConstraints.FreezePosition;
            gameManager.rbCoin[0].constraints = RigidbodyConstraints.FreezeRotation;
        }
        finally
        {
            try
            {
                gameManager.rbCoin[1].useGravity = false;
                gameManager.rbCoin[1].velocity = Vector3.zero;
                gameManager.modelSelect[1].transform.position = respawnPos;
                gameManager.rbCoin[1].constraints = RigidbodyConstraints.FreezePosition;
                gameManager.rbCoin[1].constraints = RigidbodyConstraints.FreezeRotation;
                gameManager.rbCoin[1].transform.localRotation = Quaternion.Euler(0, 0, 0);

            }
            finally
            {
                left.interactable = true;
                right.interactable = true;
                MovingLAR.dropButtonAfterPress.interactable = false;
                AddPoint.coinEnter = false;
                AddPoint.coinEntered = false;
            }
        }
    }
}
