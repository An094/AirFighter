using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public static GameManager ShareInstance;
    public Text scoreText;
    private int score;

    private int numHeart;
    public GameObject heart1;
    public GameObject heart2;
    public GameObject heart3;

    // Start is called before the first frame update

    private void Awake()
    {
        ShareInstance = this;
    }

    void Start()
    {
        score = 0;
        numHeart = 3;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void IncreaseScore()
    {
        score += 1;
        scoreText.text = score.ToString();
    }

    public void DecreaseHeart()
    {
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
        }
    }
}
