using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBullet : MonoBehaviour
{
    private Rigidbody2D rb2d;
    Vector3 screenBounds;
    Vector2 vForce;

    public GameObject playerPrefab;

    void OnEnable()
    {
        screenBounds = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, Camera.main.transform.position.z));
        rb2d = GetComponent<Rigidbody2D>();

        Vector3 direction = playerPrefab.transform.position - gameObject.transform.position;

        vForce = direction;
    }

    // Update is called once per frame
    void Update()
    {
        if (transform.position.x < screenBounds.x * -1 || transform.position.x > screenBounds.x
            || transform.position.y < -1.0f * screenBounds.y)
        {
            this.gameObject.SetActive(false);
        }
        Vector2 pos = transform.position;
        pos.x += vForce.x  * Time.deltaTime * 0.5f;
        pos.y += vForce.y  * Time.deltaTime * 1.0f;
        transform.position = pos;
    }

    private void FixedUpdate()
    {
        //rb2d.velocity = transform.forward * 2.0f;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.transform.tag.Equals("player"))
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
