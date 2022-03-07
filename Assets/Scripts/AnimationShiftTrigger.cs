using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationShiftTrigger : MonoBehaviour
{
    
    public GameObject character;
    public Animator animator;
    public float blend = 0;
    public bool changed = false;

    // Start is called before the first frame update
    void Start()
    {
        animator = character.GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (blend < 1 && changed){
            blend += 0.01f;
        }
        if (blend > 0 && !changed){
            blend -= 0.01f;
        }

        animator.SetFloat("Blend", blend);
        animator.SetBool("Triggered", changed);
    }

    
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
