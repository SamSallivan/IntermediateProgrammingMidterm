using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

//A class that detects whether dialogue is triggered, and sends the information to canvas and UI.
public class DialogueTrigger : MonoBehaviour
{
    //Dialogue class that contains dialogue information.
    public Dialogue dialogue;

    //Queues to store information exported from the dialogue class.
    public Queue<string> sentences;
    private Queue<string> names;
    private Queue<Sprite> avatars;
    private Queue<Sprite> textboxes;

    //Current dialogue information to be displayed.
    [TextArea(5,10)]
    public string sentence;
    string name;
    Sprite avatar;
    Sprite textbox;

    //Target UIs on the canvas.
    public TMP_Text nameText;
    public TMP_Text dialogueText;
    public Image avatarSprite;
    public Image textboxSprite;

    //A indication that pops up when player is in range of an available dialogue.
    public TMP_Text Interactable;
    //Text displayed by Interactable.
    [TextArea(5,10)]
    public string interactableName;

    //bool keeping track of whether is triggered.
    public bool triggered = false;
    public bool talked = false;

    //Maximum distance from player that can be triggered.
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

        //casts a ray from player camera, and checks if the hit object contains a dialogue trigger.
        //if so, import dialogue information into the queues.
        //and turn on the indication.
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
            //Ends dialogue and clears queues when player leaves trigger range during dialogue.
            else{
                if (triggered)
                    EndDialogue();
                triggered = false;
            }

            //turns off indication when dialogue actually starts.
            if (hit.transform.gameObject.tag == "DialogueTrigger" && !dialogueText.enabled){
                Interactable.enabled = true;
            }
            else{
                Interactable.enabled = false;
            }
        }
        //Ends dialogue and clears queues when player leaves trigger range during dialogue.
        else{
            if (triggered)
                EndDialogue();
            triggered = false;
            Interactable.enabled = false;
        }

        if (triggered)
        {
            if (Input.GetKeyDown(KeyCode.E) || Input.GetKeyDown(KeyCode.Mouse0))
            {
                talked = true;
                
                //When key pressed, check if current sentence has finished typing out.
                if (dialogueText.text == sentence){
                    
                    //if so, check if queue has been emptied.
                    if (sentences.Count == 0)
                    {
                        EndDialogue();
                        return;
                    }

                    //if not, moves to the next line in queues.
                    name = names.Dequeue();
                    sentence = sentences.Dequeue();
                    avatar = avatars.Dequeue();
                    textbox = textboxes.Dequeue();

                    //makes sure the UIs are turned on.
                    nameText.enabled = true;
                    dialogueText.enabled = true;    //show text
                    avatarSprite.enabled = true;
                    textboxSprite.enabled = true;   //show image
                    
                    //sets the UIs to the current dialogue information.
                    nameText.text = name;
                    StartCoroutine(TypeText(sentence));
                    avatarSprite.GetComponent<Image>().sprite = avatar;
                    textboxSprite.GetComponent<Image>().sprite = textbox;
                }
                //if current sentence is not finished typing out, skip the typing and show the full sentence.
                else{
                    StopAllCoroutines();
                    dialogueText.text = sentence;
                }
            }

        }

    }

    //when player enters range, clears previous queues, and import content from dialogue.
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

    //types out the current sentence by letter.
    IEnumerator TypeText(string sentence){
        for (int i = 0; i <= sentence.Length; i++){
            dialogueText.text = sentence.Substring(0, i);
            yield return new WaitForSeconds(0.025f);
        }
    }

    //exits any ongoing dialogue, clears queue.
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
