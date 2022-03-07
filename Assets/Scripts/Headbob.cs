using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Headbob : MonoBehaviour
{
    public float frequency = 1f;
    public float amplitute = 0.5f;
    public float lookAtDistance = 15f;
    public float thresholdSpeed = 2f;
    public Vector3 returnPosition;
    public Transform playerHead;    
    public Transform playerCamera;
    public CharacterController characterController;

    void Start()
    {
        characterController = GetComponent<CharacterController>();
        returnPosition = playerCamera.localPosition;
    }

    // Update is called once per frame
    void Update()
    {
        if (Mathf.Abs(characterController.velocity.x) > thresholdSpeed || Mathf.Abs(characterController.velocity.z) > thresholdSpeed){
            if (characterController.isGrounded){
                Vector3 motion = Vector3.zero;
                bool run = Input.GetKey(KeyCode.LeftShift);
                float scaler = run ? 1.75f : 1f;

                motion.x += Mathf.Cos(Time.time * frequency * scaler / 2) * amplitute * scaler * 2;
                motion.y += Mathf.Sin(Time.time * frequency * scaler) * amplitute * scaler;
                playerCamera.localPosition += motion;
            }
        }
        
        if (playerCamera.transform.localPosition != returnPosition){
            playerCamera.transform.localPosition = Vector3.Lerp(playerCamera.transform.localPosition, returnPosition, Time.deltaTime * 2);
        }

        playerCamera.LookAt(playerHead.transform.position + playerHead.forward * lookAtDistance);

    }
}
