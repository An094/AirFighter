using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss : MonoBehaviour
{
    bool isStart;
    public HealthBar healthBar;
    Vector3 m_ScreenBounds;
    // Start is called before the first frame update
    void Start()
    {
        m_ScreenBounds = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, Camera.main.transform.position.z));
        isStart = false;
    }

    // Update is called once per frame
    void Update()
    {
        if(transform.position.y >=m_ScreenBounds.y * 0.5f)
        {
            transform.position = new Vector2(0.0f, transform.position.y - 2.0f * Time.deltaTime);
        }
        else
        {
            isStart = true;
        }
    }

    public void HitBullet()
    {
        if(isStart)
        {
            healthBar.DecreaseHealth();

        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.transform.tag.Equals("bullet"))
        {
            collision.gameObject.SetActive(false);
            GameObject expl = ObjectPooler.SharedInstance.GetPooledObject("explosion");
            expl.transform.position = collision.transform.position;
            expl.SetActive(true);
            HitBullet();
            SoundManager.PlaySound("explosion");
        }
    }

    public void Destroy()
    {
        gameObject.SetActive(false);
    }
}
