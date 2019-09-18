using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Controller : MonoBehaviour
{
    public float movementSpeed;
    public float rotSpeed = 15.0f;
    private Vector3 moveVector;

    private CharacterController ch_controller;
    private Animator ch_animator;

    private void Start()
    {

        ch_controller = GetComponent<CharacterController>();
        ch_animator = GetComponent<Animator>();

    }

    private void Update()
    {
        Movement();
    }

    private void Movement()
    {
        Debug.Log(moveVector);
        moveVector = Vector3.zero;
        moveVector.x = Input.GetAxis("Horizontal") * movementSpeed;
        moveVector.z = Input.GetAxis("Vertical") * movementSpeed;

        if (moveVector.x != 0 || moveVector.z != 0)
        {
            ch_animator.SetBool("Move", true);
        }
        else
        {
            ch_animator.SetBool("Move", false);
        }

        if (moveVector.x != 0 || moveVector.z != 0)
        {
            
            Vector3 direct = Vector3.RotateTowards(transform.forward, moveVector, movementSpeed, 0.0f); 
            
            transform.rotation =  Quaternion.LookRotation(direct);
            //Quaternion direction = Quaternion.LookRotation(moveVector);
            //transform.rotation = Quaternion.Lerp(transform.rotation, direction, rotSpeed * Time.deltaTime);
        }

        
        ch_controller.Move(moveVector * Time.deltaTime);
    }
}
