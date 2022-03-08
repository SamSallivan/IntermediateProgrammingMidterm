using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationShiftTrigger : MonoBehaviour
{
    
    public GameObject character;
    public Animator animator;
    public float blend = 0; //A float between 0 and 1 for animations transition.
    public bool changed = false; //A boolean that switches 2 animations on and off.
    void Start()
    {
        animator = character.GetComponent<Animator>();
    }

    void Update()
    {
        //gradually shifts the value blend between 0 and 1, depending on whether is triggered.
        if (blend < 1 && changed){
            blend += 0.01f;
        }
        if (blend > 0 && !changed){
            blend -= 0.01f;
        }

        //sends the values to the animator, to be used as conditions for animation transition.
        animator.SetFloat("Blend", blend);
        animator.SetBool("Triggered", changed);
    }

    //set the bool equal to whether is triggered.
     void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player"){
            changed = true;
        }
    }
    void OnTriggerExit(Collider other)
    {
        if(other.tag == "Player"){
            changed = false;
        }

    }

}
