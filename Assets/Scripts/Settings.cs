using UnityEngine;
using UnityEngine.UI;

public class Settings : MonoBehaviour
{

    public DropTheCoin DropTheCoin;
    public Rigidbody CoinMain;
    public bool GravityHigh = true;
    public PhysicMaterial BouncyCoin;
    public Button OnOffButton;

    private void Update()
    {
        if (DropTheCoin.dropButtonPressed == false)
        {
            OnOffButton.interactable = true;
        }
        else
        {
            OnOffButton.interactable = false;
        }
    }
    public void HighGravitySetting()
    {
        if (GravityHigh == true && DropTheCoin.dropButtonPressed == false)
        {

            CoinMain.mass = 1000;
            CoinMain.drag = 2.10f;
            BouncyCoin.bounciness = 1f;

            GravityHigh = false;


        }
        else if (GravityHigh == false && DropTheCoin.dropButtonPressed == false)
        {
            CoinMain.mass = 3000;
            CoinMain.drag = 0;
            BouncyCoin.bounciness = 0.55f;

            GravityHigh = true;

        }
    }




}
