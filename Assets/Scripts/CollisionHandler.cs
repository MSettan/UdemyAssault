using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CollisionHandler : MonoBehaviour
{
    [SerializeField] float levelLoadDelay = 1f;
    [SerializeField] GameObject deathFX;
    
        // Start is called before the first frame update
    private void OnTriggerEnter(Collider other)
    {
        deathFX.SetActive(true);
        StartDeathSequnce();
    }

    private void StartDeathSequnce()
    {
        //ScoreBoard scoreBoard = new ScoreBoard();
        //scoreBoard.ScoreHit();

        print("hit");
        SendMessage("PlayerDeath");
        Invoke("LoaderScene", levelLoadDelay);
    }

    void LoaderScene()
    {
        SceneManager.LoadScene(1);
    }
}
