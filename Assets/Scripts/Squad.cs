using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Squad : MonoBehaviour
{
    Vector3 m_ScreenBounds;
    bool isLeft;
    // Start is called before the first frame update
    void OnEnable()
    {
        m_ScreenBounds = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, Camera.main.transform.position.z));
        if (transform.position.x > 0)
        {
            isLeft = false;
        }
        else
        {
            isLeft = true;
        }
        for(int i =0;i<5;i++)
        {
            GameObject child = gameObject.transform.GetChild(i).gameObject;
            child.SetActive(true);
        }
        
    }

    // Update is called once per frame
    void Update()
    {
        Vector2 currentPos = transform.position;
        if(!isLeft)
        {
            currentPos.x -= 1.0f * Time.deltaTime;
            currentPos.y -= 1.5f * Time.deltaTime;
            if(currentPos.x < -1.5f * m_ScreenBounds.x)
            {
                gameObject.SetActive(false);
            }
        }
        else
        {
            currentPos.x += 1.0f * Time.deltaTime;
            currentPos.y -= 1.5f * Time.deltaTime;
            if(currentPos.x > 1.5f * m_ScreenBounds.x)
            {
                gameObject.SetActive(false);
            }
        }
        transform.position = currentPos;
    }
}
