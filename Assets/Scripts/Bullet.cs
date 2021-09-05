using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    //public Boss boss;
    Vector3 m_ScreenBounds;
    // Start is called before the first frame update
    void Start()
    {
        m_ScreenBounds = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, Camera.main.transform.position.z));

    }

    // Update is called once per frame
    void Update()
    {
        Vector2 currentPos = transform.position;
        currentPos.y += 4.0f * Time.deltaTime;
        gameObject.transform.position = currentPos;
        if (currentPos.y >= m_ScreenBounds.y)
        {
            gameObject.SetActive(false);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag.Equals("enemyinsquad"))
        {
            gameObject.SetActive(false);
            collision.gameObject.SetActive(false);
            GameObject explosion = ObjectPooler.SharedInstance.GetPooledObject("explosion");
            explosion.transform.position = transform.position;
            explosion.SetActive(true);
            GameManager.ShareInstance.IncreaseScore();
            SoundManager.PlaySound("explosion");
        }
    }
}
