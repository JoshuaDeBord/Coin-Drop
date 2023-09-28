using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class Quit : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void QuitGame() {

#if UNITY_EDITOR
        EditorApplication.ExitPlaymode();
#else
        Application.Quit(); 
#endif
    }
}