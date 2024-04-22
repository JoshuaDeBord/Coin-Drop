using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class LoadingScreen : MonoBehaviour
{
    private GameObject loadingScreenPanel;

    public TextMeshProUGUI loadingSliderNumber;
    public Slider loadingSlider;

    private Animator loadingAnimator;

    public Button startbutton;

    private GameManager gameManager;

    private void Start()
    {
        loadingScreenPanel = this.gameObject;
        loadingAnimator = this.GetComponent<Animator>();
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
    }


    public IEnumerator StartLoadingScreen()
    {
        Time.timeScale = 1;
        startbutton.gameObject.SetActive(false);
        loadingSliderNumber.fontSize = 65;

        loadingAnimator.SetBool("isUp", false);
        loadingAnimator.SetBool("isDown", true);
        //loadingAnimator.Play("LoadingScreenGoingDown");

        Debug.Log("RAN!!");
        yield return new WaitForSeconds(0.5f);
        bool done = false;
        while (done == false)
        {

            yield return new WaitForSeconds(0.008f);
            loadingSlider.value += 1;
            loadingSliderNumber.text = loadingSlider.value.ToString();
            if (loadingSlider.value >= 100)
            {
                loadingSliderNumber.fontSize = 46;
                loadingSliderNumber.text = "Ready!";
                startbutton.gameObject.SetActive(true);
                StopCoroutine(StartLoadingScreen());

                done = true;
            }
        }
    }
    public void StartClosing()
    {
        loadingAnimator.SetBool("isDown", false);
        loadingAnimator.SetBool("isUp", true);
        //loadingAnimator.Play("LoadingScreenGoingUp");

        StartCoroutine(ResetLoadingScreen());


    }

    public IEnumerator ResetLoadingScreen()
    {
        loadingSliderNumber.text = "";
        yield return new WaitForSeconds(1);
        startbutton.gameObject.SetActive(false);
        loadingSliderNumber.fontSize = 65;
        loadingSlider.value = 0;
        loadingSliderNumber.text = "0";
    }
    public void StartLoading()
    {
        StartCoroutine(StartLoadingScreen());
    }


}
