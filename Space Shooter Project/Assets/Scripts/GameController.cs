using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine;

public class GameController : MonoBehaviour
{
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

    void Start()
    {
        restart = false;
        gameOver = false;
        restartText.text = "";
        gameOverText.text = "";
        score = 0;
        UpdateScore();
        StartCoroutine(SpawnWaves());
        musicSource.clip = musicClipThree;
        musicSource.Play();
        musicSource.loop = true;
        background = GameObject.Find("Background");
        oneTime = 0;
    }

    void Update()
    {
        if (restart)
        {
            if (Input.GetKeyDown(KeyCode.Y))
            {
                SceneManager.LoadScene("SampleScene");
            }
        }
    }

    IEnumerator SpawnWaves()
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
    }
    public void AddScore(int newScoreValue)
    {
        score += newScoreValue;
        UpdateScore();
    }

    void UpdateScore()
    {
        scoreText.text = "Points:" + score;
        if (score >= 300)
        {
            gameOverText.text = "You win! Game Created By William Beatty";
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
    }

    IEnumerator BGSpeedUp() 
    { 
        while (background.GetComponent<BGScroller>().scrollSpeed > -75f)
        {
            yield return new WaitForSeconds(BGAccel);
            background.GetComponent<BGScroller>().scrollSpeed *= 1.01f;
        }
    }


    public void GameOver()
    {
            gameOverText.text = "Game Over!";
            gameOver = true;
            musicSource.clip = musicClipTwo;
            musicSource.Play();
            musicSource.loop = false;
    }
}
