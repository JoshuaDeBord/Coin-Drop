using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;

public class leaderboardController : MonoBehaviour
{
    public GameObject[] leaderboard;
    public GameObject LeaderboardPanel;
    public int leaderboardSelectedInt = 1;
    public TextMeshProUGUI leaderboardName;

    public void ChangeLeaderboardUp()
    {

        {
            leaderboardSelectedInt++;
            if (leaderboardSelectedInt == 3)
                leaderboardSelectedInt = 0;
            switch (leaderboardSelectedInt)
            {
                case 0:
                    leaderboard[0].SetActive(true);
                    leaderboard[1].SetActive(false);
                    leaderboard[2].SetActive(false);
                    leaderboardName.text = "CLASSIC";
                    break;

                case 1:
                    leaderboard[0].SetActive(false);
                    leaderboard[1].SetActive(true);
                    leaderboard[2].SetActive(false);
                    leaderboardName.text = "TIMED";
                    break;

                case 2:
                    leaderboard[0].SetActive(false);
                    leaderboard[1].SetActive(false);
                    leaderboard[2].SetActive(true);
                    leaderboardName.text = "BOMBS";
                    break;
            }
            Debug.Log("LEFT IS PRESSED DOWN");
        }
    }

    public void ChangeLeaderboardDown()
    {
        leaderboardSelectedInt--;
        if (leaderboardSelectedInt == -1)
            leaderboardSelectedInt = 2;
        switch (leaderboardSelectedInt)
        {
            case 0:
                leaderboard[0].SetActive(true);
                leaderboard[1].SetActive(false);
                leaderboard[2].SetActive(false);
                leaderboardName.text = "CLASSIC";
                break;

            case 1:
                leaderboard[0].SetActive(false);
                leaderboard[1].SetActive(true);
                leaderboard[2].SetActive(false);
                leaderboardName.text = "TIMED";
                break;

            case 2:
                leaderboard[0].SetActive(false);
                leaderboard[1].SetActive(false);
                leaderboard[2].SetActive(true);
                leaderboardName.text = "BOMBS";
                break;
        }

    }

    public void ConsoleChangeLeaderboardDown(InputAction.CallbackContext context)
    {
        if (LeaderboardPanel.activeInHierarchy == true)
        {
            if (context.performed)
                ChangeLeaderboardDown();
        }
    }

    public void ConsoleChangeLeaderboardUp(InputAction.CallbackContext context)
    {
        if (LeaderboardPanel.activeInHierarchy == true)
        {
            if (context.performed)
                ChangeLeaderboardUp();
        }
    }
}
