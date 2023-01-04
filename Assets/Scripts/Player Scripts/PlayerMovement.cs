using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    
    private CharacterController character_controller;
    private Vector3 move_Direction;
    public float speed = 5f;
    private float gravity = 20f;

    public float jump_Force = 10f;
    private float vertical_Velocity;

    private void Awake(){
        character_controller = GetComponent<CharacterController>();

    }
    
    private void Update()
    {
        MoveThePlayer();
    }

    private void MoveThePlayer(){
        move_Direction = new Vector3(Input.GetAxis(Axis.HORIZONTAL), 0f, Input.GetAxis(Axis.VERTICAL));

        move_Direction  = transform.TransformDirection(move_Direction);
        move_Direction *= speed * Time.deltaTime;
        ApplyGravity();
        character_controller.Move(move_Direction);
    }

    private void ApplyGravity(){
        if(character_controller.isGrounded){
            vertical_Velocity -= gravity * Time.deltaTime;
            //jump
            PlayerJump();
        }else{
            vertical_Velocity -= gravity * Time.deltaTime;
        }
        move_Direction.y = vertical_Velocity * Time.deltaTime;
    }

    private void PlayerJump(){
        if(character_controller.isGrounded && Input.GetKeyDown(KeyCode.Space)){
            vertical_Velocity = jump_Force;
        }
    }
}
