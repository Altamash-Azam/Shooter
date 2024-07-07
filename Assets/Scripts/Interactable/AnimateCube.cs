using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimateCube : Interactable
{
    Animator animator;
    private string startPrompt;

    private void Awake() {
        animator = GetComponent<Animator>();
    }

    private void Start() {
        startPrompt = promptMessage;
    }

    private void Update() {
        if(animator.GetCurrentAnimatorStateInfo(0).IsName("Idle")){
            promptMessage = startPrompt;
        }
        else{
            promptMessage = "Animating...";
        }
    }

    protected override void Interact()
    {
        animator.SetTrigger("Spin");
    }
}
