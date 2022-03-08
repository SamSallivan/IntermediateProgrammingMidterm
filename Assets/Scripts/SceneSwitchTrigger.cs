using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class SceneSwitchTrigger : MonoBehaviour
{

    //bool keeping track of whether is triggered.
    public bool triggered = false;
    public bool played = false;

    //Maximum distance from player that can be triggered.
    public float distance = 2.5f;

    //Text displayed by Interactable.
    [TextArea(5,10)]
    public string interactableName;

    //A indication that pops up when player is in range of an available dialogue.
    public TMP_Text Interactable;
    public TMP_Text dialogueText;
    public GameObject Player;

    //takes a float as the time it takes before loading new scene.
    public float delay = 0;

    //takes the name of the new scene to be loaded, in a string.
	public string scene;

    void Update()
    {

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
        
        //When key pressed, starts coroutine that switches to a scene after the given delay.
        if (triggered)
        {
            if (Input.GetKeyDown(KeyCode.E))
		        StartCoroutine(LoadScene(delay));
                
        }
    }

	IEnumerator LoadScene(float delay)
	{
		yield return new WaitForSeconds(delay);
		UnityEngine.SceneManagement.SceneManager.LoadScene(scene);
	}
}
