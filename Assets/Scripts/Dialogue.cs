using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]

//A class that stores information contained in a dialogue.
public class Dialogue
{
    //names of the object thats speaking.
    public string[] names;
    
    //sprites of the object thats speaking.

    public Sprite[] avatars;
    //sprites of the current textbox, in case some objects have exclusive textboxes.
    public Sprite[] textboxs;

    //strings of the dialogue.
    [TextArea(5,10)]
    public string[] sentences;
}
