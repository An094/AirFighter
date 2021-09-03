using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
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
        currentPos.y -= 2.0f * Time.deltaTime;
        gameObject.transform.position = currentPos;
        if(currentPos.y <= -1.0f * m_ScreenBounds.y)
        {
            gameObject.SetActive(false);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.transform.tag == "bullet")
        {
            gameObject.SetActive(false);
            collision.gameObject.SetActive(false);
            GameObject expl = ObjectPooler.SharedInstance.GetPooledObject("explosion");
            if(expl == null)
            {
                Debug.Log("explosion is null");
                return;
            }
            expl.transform.position = transform.position;
            expl.SetActive(true);
        }
        else if(collision.transform.tag == "player")
        {
            gameObject.SetActive(false);
            collision.gameObject.SetActive(false);
            GameObject expl = ObjectPooler.SharedInstance.GetPooledObject("explosion");
            if (expl == null)
            {
                Debug.Log("explosion is null");
                return;
            }
            expl.transform.position = transform.position;
            expl.SetActive(true);
            Debug.Log("GAME OVER");
        }
    }
}
