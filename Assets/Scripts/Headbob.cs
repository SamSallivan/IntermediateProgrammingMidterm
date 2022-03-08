using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Slightly shakes player view when moving.
public class Headbob : MonoBehaviour
{
    //how fast the camera shakes.
    public float frequency = 1f;
    
    //how much the camera shakes.
    public float amplitute = 0.5f;

    //how far a point in distance the camera stablizes looking at.
    public float lookAtDistance = 15f;

    //speed required to start shaking,
    public float thresholdSpeed = 2f;

    //the default position to return when done shaking.
    public Vector3 returnPosition;

    //instead of camera itself, shakes camera's empty transform parent
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

                //increases motion if player is running.
                bool run = Input.GetKey(KeyCode.LeftShift);
                float scaler = run ? 1.75f : 1f;

                //calculates the motion to be applied.
                Vector3 motion = Vector3.zero;
                motion.x += Mathf.Cos(Time.time * frequency * scaler / 2) * amplitute * scaler * 2;
                motion.y += Mathf.Sin(Time.time * frequency * scaler) * amplitute * scaler;
                playerCamera.localPosition += motion;
            }
        }
        
        //always attempting to send camera back to default position.
        if (playerCamera.transform.localPosition != returnPosition){
            playerCamera.transform.localPosition = Vector3.Lerp(playerCamera.transform.localPosition, returnPosition, Time.deltaTime * 2);
        }

        //stablizes the camera by making it looking at a fixed point in the given distance.
        playerCamera.LookAt(playerHead.transform.position + playerHead.forward * lookAtDistance);

    }
}
