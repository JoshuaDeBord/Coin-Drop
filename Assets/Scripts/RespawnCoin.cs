using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RespawnCoin : MonoBehaviour
{
    public Vector3 respawnPos;
    public Transform CoinMain;
    public DropTheCoin DropTheCoin;
    public Cover Cover;
    public AddPoint AddPoint;
    public Button left, right;
    void Start()
    {
        respawnPos = transform.position;
        
    }

   public void RespawnTheCoin()
    {
        if (AddPoint.coinEnter = true)
        {
            transform.position = respawnPos;
            CoinMain.position = respawnPos;
            CoinMain.GetComponent<Rigidbody>().useGravity = false;
            DropTheCoin.dropButtonPressed = false;
            CoinMain.GetComponent<Rigidbody>().velocity = Vector3.zero;
            DropTheCoin.rbCoin.constraints = RigidbodyConstraints.FreezePosition;
            DropTheCoin.rbCoin.constraints = RigidbodyConstraints.FreezeRotation;
            Cover.ColliderFalse.isTrigger = true;
            Cover.RendererEnabler.enabled = false;
            left.interactable = true;
            right.interactable = true;
            AddPoint.coinEnter = false;
        }
        

    }
    void Update()
    {
        
    }
}
