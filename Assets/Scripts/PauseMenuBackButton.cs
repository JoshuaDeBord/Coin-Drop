using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseMenuBackButton : MonoBehaviour
{
    private DropTheCoin DropTheCoin;
    public Rigidbody CoinMain;
    

    public void IfGravityIsTrue()
    {
        Time.timeScale = 1;
    }
}