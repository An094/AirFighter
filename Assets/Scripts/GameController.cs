using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    Vector3 m_ScreenBounds;
    //public GameObject backgroud;
    private GameObject currentBackgroud;
    private GameObject newBackgroud;
    private bool wasAdded;

    //public GameObject enemyPrefab;

    // Start is called before the first frame update
    void Start()
    {
        m_ScreenBounds = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, Camera.main.transform.position.z));

        //Create background
        currentBackgroud = ObjectPooler.SharedInstance.GetPooledObject("background");
        currentBackgroud.SetActive(true);
        wasAdded = false;

        //Spawn enemies
        StartCoroutine(enemyWave());
    }

    // Update is called once per frame
    void Update()
    {
        Vector2 currentPgPosition = currentBackgroud.transform.position;
        if (currentPgPosition.y <= 0.0f && !wasAdded)
        {
            //newBackgroud = (GameObject)Instantiate<GameObject>(backgroud);
            newBackgroud = ObjectPooler.SharedInstance.GetPooledObject("background");
            newBackgroud.SetActive(true);
            newBackgroud.transform.position = new Vector2(0.0f, 33.5f);
            wasAdded = true;
        }
        else if(currentPgPosition.y <= -22.0f)
        {
            currentBackgroud.SetActive(false);
            currentBackgroud = newBackgroud;
            wasAdded = false;
            currentPgPosition = currentBackgroud.transform.position;
        }
        currentPgPosition.y -= 2.0f * Time.deltaTime;
        currentBackgroud.transform.position = currentPgPosition;
        if (wasAdded)
        {
            newBackgroud.transform.position = new Vector2(0.0f, newBackgroud.transform.position.y - 2.0f*Time.deltaTime);
        }
    }

    IEnumerator enemyWave()
    {
        while(true)
        {
            float timeSpawn = Random.Range(0.5f, 1.5f);
            yield return new WaitForSeconds(timeSpawn);
            SpawnEnemy();
        }
       
    }

    void SpawnEnemy()
    {
        float posEnemyX = Random.Range(0.0f, m_ScreenBounds.x * 2) - m_ScreenBounds.x;

        GameObject enemy = ObjectPooler.SharedInstance.GetPooledObject("enemy");
        if(enemy == null)
        {
            Debug.Log("enemy is null");
            return;
        }
        enemy.transform.position = new Vector2(posEnemyX, m_ScreenBounds.y);
        enemy.SetActive(true);
    }
}
