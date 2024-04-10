using TMPro;
using UnityEngine;


public class MusicController : MonoBehaviour
{
    public int musicSelectedInt;

    public TextMeshProUGUI musicNameText;

    public AudioClip[] musicClips;

    private AudioSource musicSource;

   

    private string[] musicNames =
    {
        "Funky Thanksgiving",
        "New Beginning",
        "Electric boogaloo"
    };

    private void Start()
    {
        musicSource = GetComponent<AudioSource>();
    }
    public void ChangeSongUp()
    {
        musicSource.Stop();
        musicSelectedInt++;
        
        if (musicSelectedInt == 3)
        {
            musicSelectedInt = 0;
        }
        
        if (musicSelectedInt == 0)
        {
            musicSource.clip = musicClips[0];
            musicNameText.text = "Funky\nThanksgiving";
            musicSource.Play();
        }
        if (musicSelectedInt == 1)
        {
            musicSource.clip = musicClips[1];
            musicNameText.text = "New\nBeginning";
            musicSource.Play();
        }
        if (musicSelectedInt == 2)
        {
            musicSource.clip = musicClips[2];
            musicNameText.text = "Elctric\nBoogaloo";
            musicSource.Play();
        }

    }

    public void ChangeSongDown()
    {
        musicSource.Stop();
        musicSelectedInt--;

        if (musicSelectedInt == -1)
        {
            musicSelectedInt = 2;
        }

        if (musicSelectedInt == 0)
        {
            musicSource.clip = musicClips[0];
            musicNameText.text = "Funky\nThanksgiving";
            musicSource.Play();
        }
        if (musicSelectedInt == 1)
        {
            musicSource.clip = musicClips[1];
            musicNameText.text = "New\nBeginning";
            musicSource.Play();
        }
        if (musicSelectedInt == 2)
        {
            musicSource.clip = musicClips[2];
            musicNameText.text = "Elctric\nBoogaloo";
            musicSource.Play();
        }
    }
}
