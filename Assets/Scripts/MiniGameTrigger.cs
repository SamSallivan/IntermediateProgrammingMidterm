using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class MiniGameTrigger : MonoBehaviour
{

    public bool triggered = false;
    public bool played = false;
    public float distance = 2.5f;
    public TMP_Text Interactable;
    public Transform playerCamera;
    public Transform targetCamera;
    public Transform returnCamera;
    public GameObject Player;
    public GameObject MiniPlayer;
    public float timer = 0;

    void Start()
    {
        returnCamera.transform.position = playerCamera.transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        if (timer < 0.5f){
            timer += Time.deltaTime;
        }

        if (played){
            playerCamera.transform.position = Vector3.Lerp(returnCamera.transform.position, targetCamera.transform.position, timer/0.5f);
            playerCamera.transform.rotation = Quaternion.Lerp(returnCamera.transform.rotation, targetCamera.transform.rotation, timer/0.5f);
        }
        else if (playerCamera.transform.position != returnCamera.transform.position){
            playerCamera.transform.position = Vector3.Lerp(targetCamera.transform.position, returnCamera.transform.position, timer/0.5f);
            playerCamera.transform.rotation = Quaternion.Lerp(targetCamera.transform.rotation, returnCamera.transform.rotation, timer/0.5f);
        }

        RaycastHit hit;

        if (Physics.Raycast(GameObject.Find("Player Camera").transform.position, GameObject.Find("Player Camera").transform.forward, out hit, distance))
        {
            if (hit.transform.gameObject == this.gameObject){
                if (!triggered){
                    triggered = true;
                }
            }
            else{
                triggered = false;
            }
        }
        else{
            triggered = false;
        }
        
        
        if (triggered)
        {
                Interactable.enabled = true;

            if (Input.GetKeyDown(KeyCode.E))
            {
                timer = 0;
                
                if (!played){
                    Interactable.text = "PRESS E TO EXIT";
                    Player.GetComponent<View>().isActive = false;
                    MiniPlayer.GetComponent<View>().isActive = true;
                    played = true;
                    returnCamera.transform.position = playerCamera.transform.position;
                    returnCamera.transform.rotation = playerCamera.transform.rotation;
                }
                else if (played){
                    Interactable.text = "PRESS E TO INTERACT";
                    Player.GetComponent<View>().isActive = true;
                    MiniPlayer.GetComponent<View>().isActive = false;
                    played = false;
                }

            }
        }
        else{
                Interactable.text = "PRESS E TO INTERACT";
                Interactable.enabled = false;
        }
                
    }
}
