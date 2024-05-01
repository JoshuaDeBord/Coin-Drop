using UnityEngine;

public class infoScreen : MonoBehaviour
{
    public GameObject infoPanel;
    
    public void CloseInfoScreen()
    {
        if (infoPanel.activeInHierarchy == true)
        infoPanel.SetActive(false);
    }
}
