using System.Collections;
using System.Collections.Generic;
using UnityEditor.SearchService;
using UnityEngine;

public class Settings : MonoBehaviour
{
    public Rigidbody CoinMain;
    public bool GravityHigh = true;
    
    public void HighGravitySetting()
    {
        if (GravityHigh == true)
        {
            CoinMain.mass = 1000;
            CoinMain.drag = 1.96f;
            
            GravityHigh = false;
        }
        else if (GravityHigh == false)
        {
            CoinMain.mass = 3000;
            CoinMain.drag = 0;
            
            GravityHigh = true;
        }

    }
}
