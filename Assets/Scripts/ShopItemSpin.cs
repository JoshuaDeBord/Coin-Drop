using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShopItemSpin : MonoBehaviour
{

    public int spinspeed;
    void Start()
    {
        
    }

    void Update()
    {
        transform.Rotate(0,0, spinspeed * Time.deltaTime);
    }

}
