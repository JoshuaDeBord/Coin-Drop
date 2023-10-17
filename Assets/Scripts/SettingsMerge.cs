using UnityEngine;

public class SettingsMerge : MonoBehaviour
{
    public Settings Settings;
    public static DropTheCoin DropTheCoin;

    private void Update()
    {
        if (Settings.GravityHigh == true  )
        {

            Settings.CoinMain.mass = 1000;
            Settings.CoinMain.drag = 0;
            Settings.BouncyCoin.bounciness = .55f;

            
        }
        else if (Settings.GravityHigh == false )
        {
            Settings.CoinMain.mass = 3000;
            Settings.CoinMain.drag = 2.10f;
            Settings.BouncyCoin.bounciness = .55f;
            
        }
    }




}
