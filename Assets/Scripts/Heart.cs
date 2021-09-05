using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Heart : MonoBehaviour
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
        if(transform.position.y >= m_ScreenBounds.y * -1f)
        {
            transform.position = new Vector2(transform.position.x, transform.position.y - 2f * Time.deltaTime);
        }
        else
        {
            gameObject.SetActive(false);
        }
    }
}
