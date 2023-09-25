using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cover : MonoBehaviour
{

    public Collider ColliderFalse;
    public MeshRenderer RendererEnabler;
    
    

    private void OnTriggerExit(Collider other)
    {
        ColliderFalse.isTrigger = false;
        RendererEnabler.enabled = true;
    }
}
