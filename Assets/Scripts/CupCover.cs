using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CupCover : MonoBehaviour
{
    public bool used = true;
    public Collider ColliderFalse;
    public MeshRenderer RendererEnabler;
    
    

    // Update is called once per frame
    void Update()
    {
        


    }

    private void OnTriggerExit(Collider other)
    {
        if (used)
        {
        ColliderFalse.isTrigger = false;
        RendererEnabler.enabled = true;
        used = false;
        }
        
    }
}
