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

    private float currentTime;
    float currentTimeSquad;
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
        currentTime = 0.0f;
        currentTimeSquad = 0.0f;
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

        if(currentTime + Time.deltaTime >= 7.0f)
        {
            SpawnMeteorite();
            currentTime -= 7.0f;
        }
        else
        {
            currentTime += Time.deltaTime;
        }

        if(currentTimeSquad + Time.deltaTime > 10.0f)
        {
            SpawnEnemySquad();
            currentTimeSquad -= 10.0f;
        }
        else
        {
            currentTimeSquad += Time.deltaTime;
        }

        //Spawn boss
        if(GameManager.ShareInstance.IsSpawnBoss())
        {
            SpawnBoss();
            GameManager.ShareInstance.StopSpawnBoss();
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

    void SpawnMeteorite()
    {
        float posEnemyX = Random.Range(0.0f, m_ScreenBounds.x * 2) - m_ScreenBounds.x;
        GameObject meteorite = ObjectPooler.SharedInstance.GetPooledObject("meteorite");
        if (meteorite == null)
        {
            Debug.Log("meteorite is null");
            return;
        }
        meteorite.transform.position = new Vector2(posEnemyX, m_ScreenBounds.y);
        meteorite.SetActive(true);
    }

    void SpawnEnemySquad()
    {
        GameObject squad;
        int val = Random.Range(0,2);
        float posSquad = Random.Range(0.5f * m_ScreenBounds.y, m_ScreenBounds.y);
        if(val == 1)
        {
            squad = ObjectPooler.SharedInstance.GetPooledObject("squad");
            squad.transform.position = new Vector2(m_ScreenBounds.x, posSquad);
        }
        else
        {
            squad = ObjectPooler.SharedInstance.GetPooledObject("squad2");
            squad.transform.position = new Vector2(-1.0f * m_ScreenBounds.x, posSquad);
        }
        squad.SetActive(true);
    }

    void SpawnBoss()
    {
        GameObject boss = ObjectPooler.SharedInstance.GetPooledObject("boss");
        boss.transform.position = new Vector2(0.0f, m_ScreenBounds.y + 1);
        boss.SetActive(true);
    }
}
