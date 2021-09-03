using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Explosion : MonoBehaviour
{
    float currentTime;
    // Start is called before the first frame update
    void onStart()
    {
        currentTime = 0.0f;
    }


    // Update is called once per frame
    void Update()
    {
        if(currentTime + Time.deltaTime >= 0.583f)
        {
            gameObject.SetActive(false);
            currentTime -= 0.583f;
        }
        else
        {
            currentTime += Time.deltaTime;
        }
    }
}
