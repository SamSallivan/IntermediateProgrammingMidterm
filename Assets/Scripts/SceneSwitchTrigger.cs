using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class SceneSwitchTrigger : MonoBehaviour
{

    public bool triggered = false;
    public bool played = false;
    public float distance = 2.5f;
    [TextArea(5,10)]
    public string interactableName;
    public TMP_Text Interactable;
    public TMP_Text dialogueText;
    public GameObject Player;
    public float delay = 0;
	public string scene;

    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {

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
