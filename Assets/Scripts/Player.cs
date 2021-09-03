using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    Vector3 m_ScreenBounds;
    float shotTime = 0.3f;
    float currentTime = 0.0f;
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
        //Detect mouse click on 
        //if (Input.GetMouseButtonDown(0))
        //{
        //    Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        //    RaycastHit hit;
        //    if (Physics.Raycast(ray, out hit))
        //    {
        //        //Select stage    
        //        if (hit.transform.tag == "player")
        //        {
        //            Debug.Log("Click on player");
        //        }
        //    }
        //}


        if (Input.GetButton("Fire1")
            && mousePos.x > m_ScreenBounds.x * -1.0f && mousePos.x < m_ScreenBounds.x
            && mousePos.y > m_ScreenBounds.y * -1.0f && mousePos.y < m_ScreenBounds.y)
        {
            transform.position = mousePos;

        }
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
}
