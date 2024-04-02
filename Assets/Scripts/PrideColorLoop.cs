using Unity.VisualScripting;
using UnityEngine;


public class PrideColorLoop : MonoBehaviour
{
    public Light objectLight;

    public float rainbowColorNumber;

    private GameManager manager;

    public Material SphereMaterial, CoinMaterial;

    public Rigidbody coinMainRB;

    private void Start()
    {
        objectLight = GetComponent<Light>();
        manager = GameObject.Find("GameManager").GetComponent<GameManager>();
        CoinMaterial.color = Color.yellow;
        SphereMaterial.color = Color.red;
    }

    private void Update()
    {
        rainbowColorNumber = Random.Range(0f, 1f);


        if (manager.prideIsOn == true && manager.modelSelected == 1)
        {
            objectLight.color = Color.Lerp(objectLight.color, Color.HSVToRGB(rainbowColorNumber, 1f, 1f), .02f);
            CoinMaterial.SetColor("_Color", Color.HSVToRGB(rainbowColorNumber, 1f, 1f));
        }
        else if (manager.prideIsOn == true && manager.modelSelected == 2)
        {
            SphereMaterial.color = Color.Lerp(SphereMaterial.color, Color.HSVToRGB(rainbowColorNumber, 1f, 1f), 10f);
        }
        
        //transform.localRotation = Quaternion.Euler(new Vector3(0, transform.rotation.y, 0));

        
    }

    

}
