using TMPro;
using UnityEngine;

public class BombsGamemodeController : MonoBehaviour
{
    public GameObject[] row1Pins, row2Pins, row3Pins, row4Pins, row5Pins, row6Pins, row7Pins;

    public int row1Bomb, row2Bomb, row3Bomb, row4Bomb, row5Bomb, row6Bomb, row7Bomb;

    public Material regularMaterial, bombMaterial;

    public int livesLeft = 3;

    public TextMeshProUGUI livesLeftText;

    public void RandomizeNumbers()
    {
        row1Bomb = Random.Range(0, 8);
        row2Bomb = Random.Range(0, 7);
        row3Bomb = Random.Range(0, 8);
        row4Bomb = Random.Range(0, 7);
        row5Bomb = Random.Range(0, 8);
        row6Bomb = Random.Range(0, 7);
        row7Bomb = Random.Range(0, 8);

        /*Debug for numbers*/
        {
            Debug.Log("The Pins chosen for each row are: " +
                $"Row 1: {row1Bomb} " +
                $"Row 2: {row2Bomb} " +
                $"Row 3: {row3Bomb} " +
                $"Row 4: {row4Bomb} " +
                $"Row 5: {row5Bomb} " +
                $"Row 6: {row6Bomb} " +
                $"Row 7: {row7Bomb} ");
        }
    }

    public void AssignBombColors()
    {
        row1Pins[row1Bomb].GetComponent<MeshRenderer>().material = bombMaterial;
        row1Pins[row1Bomb].tag = "Bomb";
        row2Pins[row2Bomb].GetComponent<MeshRenderer>().material = bombMaterial;
        row2Pins[row2Bomb].tag = "Bomb";
        row3Pins[row3Bomb].GetComponent<MeshRenderer>().material = bombMaterial;
        row3Pins[row3Bomb].tag = "Bomb";
        row4Pins[row4Bomb].GetComponent<MeshRenderer>().material = bombMaterial;
        row4Pins[row4Bomb].tag = "Bomb";
        row5Pins[row5Bomb].GetComponent<MeshRenderer>().material = bombMaterial;
        row5Pins[row5Bomb].tag = "Bomb";
        row6Pins[row6Bomb].GetComponent<MeshRenderer>().material = bombMaterial;
        row6Pins[row6Bomb].tag = "Bomb";
        row7Pins[row7Bomb].GetComponent<MeshRenderer>().material = bombMaterial;
        row7Pins[row7Bomb].tag = "Bomb";
    }
    public void StartGamemode()
    {
        ResetPinsColors();

        RandomizeNumbers();

        AssignBombColors();
    }

    public void RemoveLife()
    {
        livesLeft--;

        livesLeftText.text = $"{livesLeft}     left";
    }

    public void ResetPinsColors()
    {
        foreach (GameObject obj in row1Pins)
        {
            obj.GetComponent<MeshRenderer>().material = regularMaterial;
            obj.tag = "Pin";
        }
        foreach (GameObject obj in row2Pins)
        {
            obj.GetComponent<MeshRenderer>().material = regularMaterial;
            obj.tag = "Pin";
        }
        foreach (GameObject obj in row3Pins)
        {
            obj.GetComponent<MeshRenderer>().material = regularMaterial;
            obj.tag = "Pin";
        }
        foreach (GameObject obj in row4Pins)
        {
            obj.GetComponent<MeshRenderer>().material = regularMaterial;
            obj.tag = "Pin";
        }
        foreach (GameObject obj in row5Pins)
        {
            obj.GetComponent<MeshRenderer>().material = regularMaterial;
            obj.tag = "Pin";
        }
        foreach (GameObject obj in row6Pins)
        {
            obj.GetComponent<MeshRenderer>().material = regularMaterial;
            obj.tag = "Pin";
        }
        foreach (GameObject obj in row7Pins)
        {
            obj.GetComponent<MeshRenderer>().material = regularMaterial;
            obj.tag = "Pin";
        }
    }

    
}
