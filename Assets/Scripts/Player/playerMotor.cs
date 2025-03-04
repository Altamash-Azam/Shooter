using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerMotor : MonoBehaviour
{
    private CharacterController controller;
    private Vector2 playerVelocity;
    private bool isGrounded;
    public float speed = 5f;
    public float gravity = -9.8f;
    public float jumpHeight = 1.5f;
    public float crouchTimer = 1f;
    bool sprinting = false;
    bool crouching = false;
    bool lerpCrouch = false;

    private void Start() {
        controller = GetComponent<CharacterController>();
    }

    private void Update() {
        isGrounded = controller.isGrounded;

        if(lerpCrouch){
            crouchTimer += Time.deltaTime;
            float p = crouchTimer/1;
            p *= p;
            if(crouching)
                controller.height = Mathf.Lerp(controller.height, 1, p);
            else
                controller.height = Mathf.Lerp(controller.height, 2, p);

            if(p>1){
                lerpCrouch = false;
                crouchTimer = 0f;
            }
        }
    }

    public void ProcessMove(Vector2 input){
        Vector3 moveDirection = Vector3.zero;
        moveDirection.x = input.x;
        moveDirection.z = input.y;
        controller.Move(transform.TransformDirection(moveDirection) * speed * Time.deltaTime);
        playerVelocity.y += gravity * Time.deltaTime;
        if(isGrounded && playerVelocity.y<0f){
            playerVelocity.y = -2f;
        }
        controller.Move(playerVelocity * Time.deltaTime);

    }

    public void Jump(){
        if(isGrounded){
            playerVelocity.y = Mathf.Sqrt(jumpHeight * -3.0f * gravity);
            controller.Move(playerVelocity * Time.deltaTime);
        }
    }

    public void Sprint(){
        sprinting = !sprinting;
        if(sprinting){
            speed = 8;
        }
        else{
            speed = 5;
        }
    }

    public void Crouch(){
        crouching = !crouching;
        crouchTimer = 0;
        lerpCrouch = true;
    }
}
