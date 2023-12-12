using UnityEngine;

public class PauseMenu : MonoBehaviour
{
    private bool isPause = false;

    public void GravityYesOrNeh()
    {

        if (isPause == true)
        {
            Time.timeScale = 0;
            isPause = false;
        }
        else if (isPause == false)
        {
            Time.timeScale = 1;
            isPause = true;
        }
    }


}
