using System.Collections;
using System.Collections.Generic;

using UnityEngine;

public class Settings : MonoBehaviour
{
    public Rigidbody CoinMain;
    public bool GravityHigh = true;
    public PhysicMaterial BouncyCoin;
    public DropTheCoin DropTheCoin;


    public void HighGravitySetting()
    {
        if (GravityHigh == true && DropTheCoin.dropButtonPressed == false)
        {
            
            
            
            GravityHigh = false;
        }
        else if (GravityHigh == false && DropTheCoin.dropButtonPressed == false)
        {
            
            GravityHigh = true;
        }

    }
}
