using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlashlightController : MonoBehaviour
{
    public GameObject Flashlight;

    void Update()
    {
        //when key pressed, set the flashlight bool to its opposite.
        if (Input.GetKeyDown(KeyCode.F)){
            Flashlight.SetActive(!Flashlight.activeInHierarchy);
        }
    }
}
