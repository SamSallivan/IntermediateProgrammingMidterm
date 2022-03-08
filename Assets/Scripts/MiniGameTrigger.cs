using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class MiniGameTrigger : MonoBehaviour
{

    //bool keeping track of whether is triggered.
    public bool triggered = false;
    public bool played = false;

    //Maximum distance from player that can be triggered.
    public float distance = 2.5f;

    //A indication that pops up when player is in range of an available dialogue.
    public TMP_Text Interactable;

    //Text displayed by Interactable.
    [TextArea(5,10)]
    public string interactableName;
    public TMP_Text dialogueText;

    //transforms of player camera, target camera and return position.
    public Transform playerCamera;
    public Transform targetCamera;
    public Transform returnCamera;
    public GameObject Player;
    public GameObject MiniPlayer;
    public float timer = 0;

    void Start()
    {
        //sets the camera return posiiton.
        returnCamera.transform.position = playerCamera.transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        if (timer < 0.5f){
            timer += Time.deltaTime;
        }

        //lerps player camera between target and return positions. depending on whether is playing the minigame.
        if (played){
            playerCamera.transform.position = Vector3.Lerp(returnCamera.transform.position, targetCamera.transform.position, timer/0.5f);
            playerCamera.transform.rotation = Quaternion.Lerp(returnCamera.transform.rotation, targetCamera.transform.rotation, timer/0.5f);
        }
        else if (playerCamera.transform.position != returnCamera.transform.position){
            playerCamera.transform.position = Vector3.Lerp(targetCamera.transform.position, returnCamera.transform.position, timer/0.5f);
            playerCamera.transform.rotation = Quaternion.Lerp(targetCamera.transform.rotation, returnCamera.transform.rotation, timer/0.5f);
        }

        //casts a ray from player camera, and checks if the hit object contains a minigame trigger.
        //if so, turn on the indication.
        RaycastHit hit;

        if (Physics.Raycast(GameObject.Find("Player Camera").transform.position, GameObject.Find("Player Camera").transform.forward, out hit, distance))
        {
            if (hit.transform.gameObject == this.gameObject){
                if (!triggered){
                    triggered = true;
                    Interactable.text = interactableName;
                    Interactable.enabled = true;
                }
            }
            else{
                triggered = false;
            }

            if (hit.transform.gameObject.tag == "DialogueTrigger" && !dialogueText.enabled){
                Interactable.enabled = true;
            }
            else{
                Interactable.enabled = false;
            }

        }
        else{
            triggered = false;
            Interactable.enabled = false;
        }
        
        
        if (triggered)
        {

            //When key pressed, pauses main player control, and starts minigame player control.
            if (Input.GetKeyDown(KeyCode.E))
            {
                timer = 0;
                
                if (!played){
                    Interactable.text = "[E] EXIT";
                    Player.GetComponent<View>().isActive = false;
                    MiniPlayer.GetComponent<View>().isActive = true;
                    played = true;
                    returnCamera.transform.position = playerCamera.transform.position;
                    returnCamera.transform.rotation = playerCamera.transform.rotation;
                }
                //starts main player control, and pauses minigame player control.
                else if (played){
                    Interactable.text = interactableName;
                    Player.GetComponent<View>().isActive = true;
                    MiniPlayer.GetComponent<View>().isActive = false;
                    played = false;
                }

            }
        }
                
    }
}
