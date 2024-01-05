using UnityEngine;
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

    

    public void RespawnTheCoin()
    {
        if (AddPoint.coinEnter == true)
        {
            RestartGame();
        }


    }

    public void RestartGame()
    {
        

        
        CoinMain.GetComponent<Rigidbody>().useGravity = false;
        gameManager.dropButtonPressed = false;
        CoinMain.GetComponent<Rigidbody>().velocity = Vector3.zero;
        try
        {
            transform.position = respawnPos;
            gameManager.modelSelect[0].transform.localPosition = new Vector3(0,0,0);
            gameManager.rbCoin[0].constraints = RigidbodyConstraints.FreezePosition;
            gameManager.rbCoin[0].constraints = RigidbodyConstraints.FreezeRotation;
        }
        finally
        {
            try
            {
                gameManager.modelSelect[1].transform.position = respawnPos;
                gameManager.rbCoin[1].constraints = RigidbodyConstraints.FreezePosition;
                gameManager.rbCoin[1].constraints = RigidbodyConstraints.FreezeRotation;
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
