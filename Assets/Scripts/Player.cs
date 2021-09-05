using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    Vector3 m_ScreenBounds;
    float shotTime = 0.25f;
    float currentTime = 0.0f;

    float horizontal;
    float vertical;

    // Start is called before the first frame update
    void Start()
    {
        m_ScreenBounds = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, Camera.main.transform.position.z));
        currentTime = 0.0f;
    }

    // Update is called once per frame
    void Update()
    {
        Vector2 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        //Move by keyboard

        horizontal = Input.GetAxis("Horizontal");
        vertical = Input.GetAxis("Vertical");

        //Move by mouse
        //if (
        //    mousePos.x > m_ScreenBounds.x * -1.0f && mousePos.x < m_ScreenBounds.x
        //    && mousePos.y > m_ScreenBounds.y * -1.0f && mousePos.y < m_ScreenBounds.y)
        //{
        //    transform.position = mousePos;

        //}

        if (currentTime + Time.deltaTime >= shotTime)
        {
            GameObject bullet = ObjectPooler.SharedInstance.GetPooledObject("bullet");
            if (bullet == null)
            {
                Debug.Log("bullet is null");
                return;
            }
            bullet.transform.position = transform.position;
            bullet.SetActive(true);
            currentTime = 0.0f;
        }
        else
        {
            currentTime += Time.deltaTime;
        }
    }

    private void FixedUpdate()
    {
        Vector2 position = transform.position;
        position.x = position.x + 5.0f * horizontal * Time.deltaTime;
        position.y = position.y + 5.0f * vertical * Time.deltaTime;
        
        if(position.x > m_ScreenBounds.x * -1f
          && position.x < m_ScreenBounds.x
          && position.y > m_ScreenBounds.y * -1f
          && position.y < m_ScreenBounds.y)
        {
            GetComponent<Rigidbody2D>().MovePosition(position);

        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.transform.tag.Equals("heartitem"))
        {
            GameManager.ShareInstance.IncreaseHeart();
            collision.gameObject.SetActive(false);
        }
        else if (collision.transform.tag.Equals("bombitem"))
        {
            GameController.DestroyAllEnemy();
            collision.gameObject.SetActive(false);
        }
        else if (collision.transform.tag.Equals("meteorite"))
        {
            GameObject expl = ObjectPooler.SharedInstance.GetPooledObject("explosion");
            if (expl == null)
            {
                Debug.Log("explosion is null");
                return;
            }
            expl.transform.position = transform.position;
            expl.SetActive(true);
            //Debug.Log("GAME OVER");
            GameManager.ShareInstance.DecreaseHeart();
        }
    }
}
