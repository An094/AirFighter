using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Meteorite : MonoBehaviour
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
        if(currentPos.y >= -1 * m_ScreenBounds.y)
        {
            currentPos.y -= 5.0f * Time.deltaTime;
        }
        else
        {
            gameObject.SetActive(false);
        }
        transform.position = currentPos;
    }
}
