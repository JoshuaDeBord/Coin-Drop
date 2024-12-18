using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Timer : MonoBehaviour
{
    public int seconds = 0;
    public TextMeshProUGUI countdownTMP;
    public Button startbutton;
    public GameModesController controller;
    public GamemodeFinishController finishcontroller;
    public RespawnCoin respawnCoin;
    public GameManager manager;
    public MovingLeftAndRight movingLAR;
    public Button leftButton, rightButton;

    private void Start()
    {

    }
    public void StartTimer(int timeToSet)
    {
        if (controller.chosenGamemode == 1)
        {
            seconds = timeToSet;
            controller.timerStarted = true;
            StartCoroutine(CountdownTimer());
            startbutton.gameObject.SetActive(false);
            countdownTMP.text = "1:00";
            controller.dropButton.gameObject.SetActive(true);
            manager.DropCoin();
        }
    }

    public void resetTimerAndScore()
    {
        countdownTMP.text = "1:00";
    }

    public IEnumerator CountdownTimer()
    {
        while (true)
        {

            yield return new WaitForSeconds(1);
            seconds--;
            if (seconds > 9)
                countdownTMP.text = "0:" + seconds.ToString();

            else countdownTMP.text = "0:0" + seconds.ToString();

            if (seconds == 0)
            {
                controller.timerStarted = false;
                leftButton.gameObject.SetActive(false);
                rightButton.gameObject.SetActive(false);
                manager.PI.SwitchCurrentActionMap("UI");

                finishcontroller.OpenTimedFinishBoard();
                break;
            }
        }
    }
}
