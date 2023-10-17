using UnityEngine;
using UnityEngine.UI;

public class RespawnCoin : MonoBehaviour
{
    public Vector3 respawnPos;
    public Transform CoinMain;
    public DropTheCoin DropTheCoin;
    private MovingLeftAndRight MovingLAR;
    public static PauseMenu PM;

    public Button left, right;

    private void Start()
    {
        respawnPos = transform.position;
        MovingLAR = GetComponent<MovingLeftAndRight>();
    }

    public void RespawnTheCoin()
    {
        if (AddPoint.coinEnter == true)
        {
            transform.position = respawnPos;
            CoinMain.position = respawnPos;
            CoinMain.GetComponent<Rigidbody>().useGravity = false;
            DropTheCoin.dropButtonPressed = false;
            CoinMain.GetComponent<Rigidbody>().velocity = Vector3.zero;
            DropTheCoin.rbCoin.constraints = RigidbodyConstraints.FreezePosition;
            DropTheCoin.rbCoin.constraints = RigidbodyConstraints.FreezeRotation;
            left.interactable = true;
            right.interactable = true;
            MovingLAR.dropButtonAfterPress.interactable = false;
            AddPoint.coinEnter = false;
            AddPoint.coinEntered = false;
        }


    }

}
