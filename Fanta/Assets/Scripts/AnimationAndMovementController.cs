using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class AnimationAndMovementController : MonoBehaviour
{
    PlayerInput playerInput;
    Animator animator;
    CharacterController characterController;
    Vector2 currentMovementInput;
    Vector3 currentMovement;
    bool isMovementPressed;
    float rotationFactorPerFrame = 1f;

    void Awake()
    {
        playerInput = new PlayerInput();
        characterController = GetComponent<CharacterController>();
        animator = GetComponent<Animator>();
        playerInput.CharacterControls.Move.started += OnMovementInput;
        playerInput.CharacterControls.Move.canceled += OnMovementInput;
        playerInput.CharacterControls.Move.performed += OnMovementInput;
    }
    void OnMovementInput(InputAction.CallbackContext context)
    {
        currentMovementInput = context.ReadValue<Vector2>();
        currentMovement.z = currentMovementInput.x;
        currentMovement.x = -currentMovementInput.y; 
        isMovementPressed = currentMovementInput.x != 0 || currentMovementInput.y !=0;
    }

    // Update is called once per frame
    void Update()
    {
        HandleRotation();
        HandleAnimation();
        characterController.Move(currentMovement*Time.deltaTime);
    }

    void HandleAnimation()
    {
        bool isWalking = animator.GetBool("isWalking");
        bool isRunning = animator.GetBool("isRunning");
        if(isMovementPressed && !isWalking)
        {
            animator.SetBool("isWalking",true);
        }
        else if(!isMovementPressed && isWalking)
        {
            animator.SetBool("isWalking",false);
        }
    }

    void HandleRotation()
    {
        Vector3 positionToLookAt;
        positionToLookAt.x = currentMovement.x;
        positionToLookAt.z = currentMovement.z;
        positionToLookAt.y = 0;
        Quaternion currentRotation = transform.rotation; 
        if(isMovementPressed)
        {
            Quaternion targetRotation = Quaternion.LookRotation(positionToLookAt); 
            transform.rotation = Quaternion.Slerp(currentRotation,targetRotation,rotationFactorPerFrame * Time.deltaTime);
        }
    }
    void OnEnable()
    {
        playerInput.CharacterControls.Enable();
    }

    void OnDisable()
    {
        playerInput.CharacterControls.Disable();
    }
}
