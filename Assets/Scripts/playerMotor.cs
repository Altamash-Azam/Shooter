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

    private void Start() {
        controller = GetComponent<CharacterController>();
    }

    private void Update() {
        isGrounded = controller.isGrounded;
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
        Debug.Log(playerVelocity.y);

    }
}
