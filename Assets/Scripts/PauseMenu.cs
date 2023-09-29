using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseMenu : MonoBehaviour
{
    public Rigidbody CoinMain;
    public static DropTheCoin DropTheCoin;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    ///
    public void GravityYesOrNeh()
    {
        

        Time.timeScale = 0;
    }


}
