using UnityEngine;
using UnityEngine.UI;

public class Settings : MonoBehaviour
{
    public OnOffColorChangeHighGravity OnOffColorChange;
    public DropTheCoin DropTheCoin;
    public GameManager GameManager;
    public Rigidbody CoinMain;
    public bool GravityHigh = true;
    public PhysicMaterial BouncyCoin;
    public Button OnOffButton;
    public ColorBlock ButtonColor;

    private void Update()
    {
        if (GameManager.dropButtonPressed == false)
        {
            OnOffButton.interactable = true;
            
        }
        else
        {
            OnOffButton.interactable = false;
            if (GravityHigh == true)
            {
                OnOffColorChange.ButtonSettings.color = new Color32(15, 255, 0, 255);
                
            }
            else if (GravityHigh == false)
            {
                OnOffColorChange.ButtonSettings.color = new Color32(255, 0, 2, 255);
            }
        }
    }
    public void HighGravitySetting()
    {
        if (GravityHigh == true && GameManager.dropButtonPressed == false)
        {

            CoinMain.mass = 1000;
            CoinMain.drag = 2.10f;
            BouncyCoin.bounciness = 1f;

            GravityHigh = false;
            Debug.Log("Gravity set to low");


        }
        else if (GravityHigh == false && GameManager.dropButtonPressed == false)
        {
            CoinMain.mass = 3000;
            CoinMain.drag = 0;
            BouncyCoin.bounciness = 0.55f;

            GravityHigh = true;
            Debug.Log("Gravity set to high");

        }
    }




}
