using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class PlayerInteract : MonoBehaviour
{
    private Camera cam;
    [SerializeField] private float distance = 3f;
    [SerializeField] LayerMask mask;
    private PlayerUI playerUI;
    private InputManager inputManager;
    [SerializeField] Image crossHair;

    private void Start() {
        cam = GetComponent<PlayerLook>().cam;
        playerUI = GetComponent<PlayerUI>();
        inputManager = GetComponent<InputManager>();
    }

    private void Update() {
        playerUI.UpdateText(String.Empty);
        crossHair.enabled = false;
        // create a ray at the centre of camera shooting outwards
        Ray ray = new Ray(cam.transform.position, cam.transform.forward);  
        Debug.DrawRay(ray.origin, ray.direction * distance);  
        RaycastHit hitInfo;

        if(Physics.Raycast(ray, out hitInfo, distance, mask)){
            if(hitInfo.collider.GetComponent<Interactable>() != null){
                Interactable interactable = hitInfo.collider.GetComponent<Interactable>();
                playerUI.UpdateText(interactable.promptMessage);
                crossHair.enabled = true;
                if(inputManager.onFoot.Interact.triggered){
                    interactable.BaseInteract();
                }
            }
        }
    }
}
