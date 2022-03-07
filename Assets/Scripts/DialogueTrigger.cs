using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class DialogueTrigger : MonoBehaviour
{
    public Dialogue dialogue;

    public Queue<string> sentences;
    private Queue<string> names;
    private Queue<Sprite> avatars;
    private Queue<Sprite> textboxes;

    [TextArea(5,10)]
    public string interactableName;
    public string sentence;
    string name;
    Sprite avatar;
    Sprite textbox;
    public TMP_Text nameText;
    public TMP_Text dialogueText;
    public Image avatarSprite;
    public Image textboxSprite;
    public TMP_Text Interactable;

    public bool triggered = false;
    public bool talked = false;
    public float distance = 2.5f;

    void Start()
    {
        names = new Queue<string>();
        sentences = new Queue<string>();
        avatars = new Queue<Sprite>();
        textboxes = new Queue<Sprite>();
        avatarSprite.enabled = false;
        textboxSprite.enabled = false;
    }
    void Update()
    {
        RaycastHit hit;

        if (Physics.Raycast(GameObject.Find("Player Camera").transform.position, GameObject.Find("Player Camera").transform.forward, out hit, distance))
        {
            if (hit.transform.gameObject == this.gameObject){
                if (!triggered){
                    triggered = true;
                    TriggerDialogue();
                    Interactable.text = interactableName;
                    Interactable.enabled = true;
                }
            }
            else{
                if (triggered)
                    EndDialogue();
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
            if (triggered)
                EndDialogue();
            triggered = false;
            Interactable.enabled = false;
        }

        // if (!talked && Mathf.Abs(transform.position.x - GameObject.Find("Madeline").transform.position.x) <= 2.5f)
        // {
        //     Interactable.GetComponent<MeshRenderer>().enabled = true;
        //     Interactable.transform.position = new Vector3(transform.position.x, transform.position.y + 3, transform.position.z);
        // }
        // else if(Mathf.Abs(transform.position.x - GameObject.Find("Madeline").transform.position.x) <= 10)
        // {
        //     Interactable.GetComponent<MeshRenderer>().enabled = false;
        // }

        if (triggered)
        {
            
            // if (!dialogueText.enabled){
            //     Interactable.enabled = true;
            // }
            // else{
            //     Interactable.enabled = false;
            // }

            if (Input.GetKeyDown(KeyCode.E) || Input.GetKeyDown(KeyCode.Mouse0))
            {
                talked = true;
                
                if (dialogueText.text == sentence){
                    
                    if (sentences.Count == 0)
                    {
                        EndDialogue();
                        return;
                    }

                    name = names.Dequeue();
                    sentence = sentences.Dequeue();
                    avatar = avatars.Dequeue();
                    textbox = textboxes.Dequeue();

                    nameText.enabled = true;
                    dialogueText.enabled = true;    //show text
                    avatarSprite.enabled = true;
                    textboxSprite.enabled = true;   //show image

                    nameText.text = name;
                    StartCoroutine(TypeText(sentence));
                    avatarSprite.GetComponent<Image>().sprite = avatar;
                    textboxSprite.GetComponent<Image>().sprite = textbox;
                }
                else{
                    StopAllCoroutines();
                    dialogueText.text = sentence;
                }
            }

        }
        else{
            //EndDialogue();
            //Interactable.enabled = false;
        }

    }

    void TriggerDialogue()
    {
        names.Clear();
        sentences.Clear();
        avatars.Clear();
        textboxes.Clear();

        foreach (string sentence in dialogue.sentences)
        {
            sentences.Enqueue(sentence);
        }
        foreach (string name in dialogue.names)
        {
            names.Enqueue(name);
        }
        foreach (Sprite avatar in dialogue.avatars)
        {
            avatars.Enqueue(avatar);
        }
        foreach (Sprite textbox in dialogue.textboxs)
        {
            textboxes.Enqueue(textbox);
        }

    }

    IEnumerator TypeText(string sentence){
        for (int i = 0; i <= sentence.Length; i++){
            dialogueText.text = sentence.Substring(0, i);
            yield return new WaitForSeconds(0.05f);
        }
    }

    void EndDialogue()
    {   
        triggered = false;
        avatarSprite.enabled = false;
        textboxSprite.enabled = false;
        nameText.enabled = false;
        dialogueText.enabled = false;
        avatarSprite.GetComponent<Image>().sprite = null;
        textboxSprite.GetComponent<Image>().sprite = null;
        nameText.text = "";
        dialogueText.text = "";

        names.Clear();  
        sentences.Clear();
        avatars.Clear();
        textboxes.Clear();

        name = "";
        sentence = "";
        avatar = null;
        textbox = null;
    }

}
