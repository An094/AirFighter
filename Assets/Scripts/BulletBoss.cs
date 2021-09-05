using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletBoss : MonoBehaviour
{
    Vector3 m_ScreenBounds;
    private Vector2 moveDirection;
    private float moveSpeed;

    private void OnEnable()
    {
        Invoke("Destroy", 3f);
    }
    // Start is called before the first frame update
    void Start()
    {
        m_ScreenBounds = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, Camera.main.transform.position.z));
        moveSpeed = 5f;
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(moveDirection * moveSpeed * Time.deltaTime);
        Vector2 currentPos = transform.position;
        if(currentPos.x < -1f * m_ScreenBounds.x
        || currentPos.x > m_ScreenBounds.x
        || currentPos.y < -1f * m_ScreenBounds.y
        || currentPos.y > m_ScreenBounds.y)
        {
            gameObject.SetActive(false);
        }
    }

    public void SetMoveDirection(Vector2 dir)
    {
        moveDirection = dir;
    }
    private void Destroy()
    {
        gameObject.SetActive(false);
    }

    private void OnDisable()
    {
        CancelInvoke();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.transform.tag.Equals("player"))
        {
            this.gameObject.SetActive(false);
            //collision.gameObject.SetActive(false);
            GameObject explosion = ObjectPooler.SharedInstance.GetPooledObject("explosion");
            explosion.transform.position = transform.position;
            explosion.SetActive(true);
            GameManager.ShareInstance.DecreaseHeart();
        }
    }
}
