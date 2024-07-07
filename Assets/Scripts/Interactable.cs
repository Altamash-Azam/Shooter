using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Interactable : MonoBehaviour
{
    // add or remove interaction event to this game component
    public bool useEvents;
    public string promptMessage;

    public virtual string OnLook(){
        return promptMessage;
    }

    public void BaseInteract(){
        if(useEvents)
            GetComponent<InteractionEvent>().OnInteract.Invoke();
        Interact();
    }

    protected virtual void Interact(){
        // No line of code to be written here.....will be written in inherited classes
    }
}
