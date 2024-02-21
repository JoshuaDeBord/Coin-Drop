using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class EventSystemHelper : MonoBehaviour
{
    public Button settingsButton1, settingsButton2, settingsButton3, quitButton, resetButton, cheatBoxButtonObject;
    public TMP_InputField cheatBox;
    public GameObject SelectionBox1, SelectionBox2, SelectionBox3, quitSelectionBox, resetSelectionBox, CheatBoxSelection;
    public GameObject CheatBox, settingsBackButton, xboxBackIcon;
    public TextMeshProUGUI goBackText;
    public RectTransform settingsBackButtonRect;
    public Vector3 CheatBoxOGSpawnLocation;
    public bool isOnReset = false;

    public PlayerInput PI;
    public float resetSpeed;


    public GameManager gameManager;

#if UNITY_WSA



    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (EventSystem.current.currentSelectedGameObject == settingsButton1.gameObject)
        {
            Debug.Log("SettingsButton1 Found");
            SelectionBox1.SetActive(true);
            SelectionBox2.SetActive(false);
            SelectionBox3.SetActive(false);
            quitSelectionBox.SetActive(false);
            isOnReset = false;
            resetSelectionBox.SetActive(false);
            CheatBoxSelection.SetActive(false);
        }
        else if (EventSystem.current.currentSelectedGameObject == settingsButton2.gameObject)
        {
            Debug.Log("SettingsButton2 Found");
            SelectionBox1.SetActive(false);
            SelectionBox2.SetActive(true);
            SelectionBox3.SetActive(false);
            quitSelectionBox.SetActive(false);
            isOnReset = false;
            resetSelectionBox.SetActive(false);
            CheatBoxSelection.SetActive(false);
        }
        else if (EventSystem.current.currentSelectedGameObject == settingsButton3.gameObject)
        {
            Debug.Log("SettingsButton3 Found");
            SelectionBox1.SetActive(false);
            SelectionBox2.SetActive(false);
            SelectionBox3.SetActive(true);
            quitSelectionBox.SetActive(false);
            isOnReset = false;
            resetSelectionBox.SetActive(false);
            CheatBoxSelection.SetActive(false);
        }
        else if (EventSystem.current.currentSelectedGameObject == quitButton.gameObject)
        {
            SelectionBox1.SetActive(false);
            SelectionBox2.SetActive(false);
            SelectionBox3.SetActive(false);
            quitSelectionBox.SetActive(true);
            isOnReset = false;
            resetSelectionBox.SetActive(false);
            CheatBoxSelection.SetActive(false);
        }
        else if (EventSystem.current.currentSelectedGameObject == resetButton.gameObject && isOnReset == false)
        {
            SelectionBox1.SetActive(false);
            SelectionBox2.SetActive(false);
            SelectionBox3.SetActive(false);
            quitSelectionBox.SetActive(false);
            isOnReset = true;
            CheatBoxSelection.SetActive(false);
            StartCoroutine(ResetColorLoop());
            
        }
        else if (EventSystem.current.currentSelectedGameObject == cheatBoxButtonObject.gameObject)
        {
            SelectionBox1.SetActive(false);
            SelectionBox2.SetActive(false);
            SelectionBox3.SetActive(false);
            quitSelectionBox.SetActive(false);
            CheatBoxSelection.SetActive(true);
            resetSelectionBox.SetActive(false);
            isOnReset = false;

        }
        

        try
        {

            goBackText.lineSpacing = -27.8f;
            xboxBackIcon.SetActive(true);
        }
        catch { }






    }

#elif UNITY_ANDROID || UNITY_IOS
    public void Start()
    {
        EventSystem.current.firstSelectedGameObject = null;
        gameManager = GetComponent<GameManager>();
        EventSystem.current.SetSelectedGameObject(null);
        EventSystem.current.SetSelectedGameObject(null);
        CheatBoxOGSpawnLocation = cheatBox.transform.localPosition;

    }
    private void Update()
    {
#if UNITY_ANDROID || UNITY_IOS


        if (settingsBackButton.gameObject.activeInHierarchy == true)
        {
            xboxBackIcon.SetActive(false);
            goBackText.lineSpacing = -67.7f;
            
        }
        if (EventSystem.current.currentSelectedGameObject == CheatBox)
        {

            CheatBox.transform.localPosition = new Vector3(-1044.1f, 796.46f, -40);

        }
        else
        {
            EventSystem.current.SetSelectedGameObject(null);
            CheatBox.transform.localPosition = CheatBoxOGSpawnLocation;


        }


#endif
    }
#endif
        public IEnumerator ResetColorLoop()
    {

        while (isOnReset == true)
        {
            resetSelectionBox.SetActive(true);
            Debug.Log("Reset box off");
            yield return new WaitForSeconds(resetSpeed);
            resetSelectionBox.SetActive(false);
            Debug.Log("Reset box on");
            yield return new WaitForSeconds(resetSpeed);
        }
    }

    









}
