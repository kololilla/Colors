using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterInputHandler : MonoBehaviour
{
    [field: SerializeField] private CharacterControllerBase controller;

    private float horizontalMove = 0f;
    private float verticalMove = 0f;
    private bool crouch;
    private bool jump;


    void Update()
    {
        horizontalMove = Input.GetAxisRaw("Horizontal");
        verticalMove = Input.GetAxisRaw("Vertical");
        if (Input.GetButtonDown("Jump"))
        {
            Debug.Log("Ugranék!!");
            jump = true;
        }
        if (Input.GetButtonDown("Crouch"))
        {
            crouch = true;
        }
        else if (Input.GetButtonUp("Crouch"))
        {
            crouch = false;
        }
    }

    private void FixedUpdate()
    {
        HandleUserInput();
        jump = false;
    }

    private void HandleUserInput()
    {
        controller.Movement(horizontalMove, verticalMove, jump, crouch);
    }
}
