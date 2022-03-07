using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float time = 0f;
    public float maxTime = 5f;
    public Vector3 dir;


    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        time = Mathf.Clamp(time + Time.deltaTime, 0f, maxTime);
        //lerps the bullet towards the given direction.
        transform.position = Vector3.Lerp(transform.position, transform.position + dir, time*2/maxTime);

        //deactivates the bullet when time is up.
        if(time == maxTime)
        {
            gameObject.SetActive(false);
        }
    }
}
