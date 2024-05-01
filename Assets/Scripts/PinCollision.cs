using UnityEngine;

public class PinCollision : MonoBehaviour
{

    public AudioSource hittingPinSound;
    public RespawnCoin respawnCoin;
    private BombsGamemodeController gameModeController;
    private GamemodeFinishController finishController;
    private GameManager gameManager;
    private BombsGamemodeLeaderboard bombLeaderboard;

    private void Start()
    {
        respawnCoin = GameObject.Find("MovingLeftAndRightAndRespawnPoint").GetComponent<RespawnCoin>();
        gameModeController = GameObject.Find("GameModesController").GetComponent<BombsGamemodeController>();
        finishController = GameObject.Find("LeaderBoard").GetComponent<GamemodeFinishController>();
        bombLeaderboard = finishController.gameObject.GetComponent<BombsGamemodeLeaderboard>();
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
    }

    private void OnCollisionEnter(Collision collision)
    {
        bool done = false;
        if (collision.gameObject.CompareTag("Pin")) hittingPinSound.Play();

        if (collision.gameObject.CompareTag("Bomb") && done == false)
        {
            hittingPinSound.Play();
            GetComponent<Rigidbody>().velocity = Vector3.zero;
            GetComponent<MeshRenderer>().enabled = false;
            GetComponent<Light>().enabled = false;
            GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeAll;
            GetComponent<ParticleSystem>().Play();
            gameModeController.RemoveLife();

            if (gameModeController.livesLeft != 0)
                respawnCoin.ToggleCoinsOn(true, false);
            else finishController.OpenBombsFinishBoard();

            Destroy(gameObject, 5);
            if (gameManager.bombsSavedPoints > 0)
            StartCoroutine(bombLeaderboard.SubmitScoreRoutine(gameManager.bombsSavedPoints));

            done = true;
        }
    }

    

    private void OnDestroy()
    {
        
    }

}
