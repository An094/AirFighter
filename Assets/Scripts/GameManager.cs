using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public static GameManager ShareInstance;
    public Text scoreText;
    private int score;

    public GameObject playerPrefab;

    private int numHeart;
    public GameObject heart1;
    public GameObject heart2;
    public GameObject heart3;

    bool isSpawnBoss;

    // Start is called before the first frame update

    private void Awake()
    {
        ShareInstance = this;
    }

    void Start()
    {
        score = 0;
        numHeart = 3;
        isSpawnBoss = false;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void IncreaseScore()
    {
        score += 1;
        scoreText.text = score.ToString();
        if(score == 30)
        {
            isSpawnBoss = true;
        }
    }

    public void IncreaseHeart()
    {
        numHeart += 1;
        if(numHeart == 2)
        {
            heart2.gameObject.SetActive(true);
        }
        if(numHeart == 3)
        {
            heart1.gameObject.SetActive(true);
        }
    }

    public void DecreaseHeart()
    {
        Camera.main.GetComponent<CameraShake>().Shake();
        numHeart -= 1;
        if(numHeart == 2)
        {
            heart1.gameObject.SetActive(false);
        }
        else if(numHeart == 1)
        {
            heart2.gameObject.SetActive(false);
        }
        else if(numHeart == 0)
        {
            heart3.gameObject.SetActive(false);
            Debug.Log("GAME OVER");
            playerPrefab.SetActive(false);
            GameObject explosion = ObjectPooler.SharedInstance.GetPooledObject("explosion");
            explosion.transform.position = playerPrefab.transform.position;
            explosion.SetActive(true);
        }
    }
    
    public bool IsSpawnBoss()
    {
        return isSpawnBoss;
    }

    public void StopSpawnBoss()
    {
        isSpawnBoss = false;
    }
}
