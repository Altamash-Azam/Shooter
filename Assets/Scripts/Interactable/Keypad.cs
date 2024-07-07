using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Keypad : Interactable
{
    private bool doorOpen = false;
    public GameObject doors;
    // we will design our interaction here
    protected override void Interact()
    {
        doorOpen = !doorOpen;
        doors.GetComponent<Animator>().SetBool("isOpen",doorOpen);
    }
}
