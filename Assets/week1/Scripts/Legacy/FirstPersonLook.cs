using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FirstPersonLook : MonoBehaviour
{
    public float sensitivity = 100f;
    public Transform player;
    float horizontalRotation = 0f;
    float verticalRotation = 0f;

    // Start is called before the first frame update
    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
    }

    // Update is called once per frame
    void Update()
    {
        float mouseX = Input.GetAxis("Mouse X") * sensitivity * Time.deltaTime;
        float mouseY = Input.GetAxis("Mouse Y") * sensitivity * Time.deltaTime;

        horizontalRotation += mouseX;
        //horizontalRotation = Mathf.Clamp(horizontalRotation, -90f, 90f);
        verticalRotation -= mouseY;
        verticalRotation = Mathf.Clamp(verticalRotation, -90f, 90f);

        //Debug.Log(horizontalRotation);
        //Debug.Log(verticalRotation);

        //player.Rotate(new Vector3(0, 1, 0) * mouseX);
        //transform.Rotate(new Vector3(-1, 0, 0) * mouseY);
        //transform.localEulerAngles = new Vector3(horizontalRotation, 0, 0);
        //player.localEulerAngles = new Vector3(0, verticalRotation, 0);
        player.localRotation = Quaternion.Euler(0, horizontalRotation, 0);
        transform.localRotation = Quaternion.Euler(verticalRotation, 0, 0);


        if (horizontalRotation >= -90 && horizontalRotation <= 90)
        {
            //player.Rotate(new Vector3(0, 1, 0) * mouseX);
        }
        if (verticalRotation >= -90 && verticalRotation <= 90)
        {
            //transform.Rotate(new Vector3(-1, 0, 0) * mouseY);
        }

    }
}
