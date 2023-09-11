using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cover : MonoBehaviour
{

    public Collider ColliderFalse;
    public MeshRenderer RendererEnabler;
    // Start is called before the first frame update
    

    // Update is called once per frame
    void Update()
    {
        


    }

    private void OnTriggerExit(Collider other)
    {
        ColliderFalse.isTrigger = false;
        RendererEnabler.enabled = true;
    }
}
