using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShopItemSpinSphere : MonoBehaviour
{

    public float spinSpeed;
    void Start()
    {

    }

    void Update()
    {
        transform.Rotate(0, spinSpeed, 0 * Time.deltaTime);
    }

}
