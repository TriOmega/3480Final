using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine;

public class GameController : MonoBehaviour
{


    #region Components


    public GameObject[] hazards;

    public Vector3 spawnValues;

    public int hazardCount;

    public float spawnWait;
    public float startWait;
    public float waveWait;

    public Text scoreText;
    public Text restartText;
    public Text gameOverText;

    public AudioSource musicSource;

    public AudioClip musicClipOne;
    public AudioClip musicClipTwo;
    public AudioClip musicClipThree;

    public float BGAccel;

    private int score;
    private bool gameOver;
    private bool restart;
    private GameObject background;
    private int oneTime;


    #endregion Components


    #region Methods

    //----------------------------//
    void Start()
    //----------------------------//
    {
        restart = false;
        gameOver = false;
        restartText.text = "";
        gameOverText.text = "";
        score = 0;
        UpdateScore();
        StartCoroutine(ISpawnWaves());
        musicSource.clip = musicClipThree;
        musicSource.Play();
        musicSource.loop = true;
        background = GameObject.Find("Background");
        oneTime = 0;

    }//END Start

    //----------------------------//
    void Update()
    //----------------------------//
    {
        if (restart)
        {
            if (Input.GetKeyDown(KeyCode.Y))
            {
                SceneManager.LoadScene(0);
            }
        }
    }//END Update

    //----------------------------//
    IEnumerator ISpawnWaves()
    //----------------------------//
    {
        yield return new WaitForSeconds(startWait);
        while (true)
        {
            for (int i = 0; i < hazardCount; i++)
            {
                GameObject hazard = hazards[Random.Range(0, hazards.Length)];
                Vector3 spawnPosition = new Vector3(Random.Range(-spawnValues.x, spawnValues.x), spawnValues.y, spawnValues.z);
                Quaternion spawnRotation = Quaternion.identity;
                Instantiate(hazard, spawnPosition, spawnRotation);
                yield return new WaitForSeconds(spawnWait);
            }
            yield return new WaitForSeconds(waveWait);

            if (gameOver)
            {
                restartText.text = "Press 'Y' for Restart";
                restart = true;
                break;
            }
        }
    }//END ISpawnWaves

    //----------------------------//
    public void AddScore(int newScoreValue)
    //----------------------------//
    {
        score += newScoreValue;
        UpdateScore();
    }//END AddScore

    //----------------------------//
    void UpdateScore()
    //----------------------------//
    {
        scoreText.text = "Points:" + score;
        if (score >= 300)
        {
            gameOverText.text = "You win! Press Escape to Quit!";
            gameOver = true;
            restart = true;
            oneTime++;
            if (oneTime == 1)
            {
                musicSource.clip = musicClipOne;
                musicSource.Play();
                musicSource.loop = false;
                StartCoroutine(BGSpeedUp());
            }
        }

    }//END UpdateScore

    //----------------------------//
    IEnumerator BGSpeedUp()
    //----------------------------//
    {
        while (background.GetComponent<BGScroller>().scrollSpeed > -75f)
        {
            yield return new WaitForSeconds(BGAccel);
            background.GetComponent<BGScroller>().scrollSpeed *= 1.01f;
        }

    }//END BGSpeedUp

    //----------------------------//
    public void GameOver()
    //----------------------------//
    {
        gameOverText.text = "Game Over!";
            gameOver = true;
            musicSource.clip = musicClipTwo;
            musicSource.Play();
            musicSource.loop = false;
    }//END GameOver

    #endregion Methods


}//END CLASS GameController
