using UnityEngine;

public class PlayerCharacterLocomotion : MonoBehaviour
{
    [Header("Movement Settings")]
    [SerializeField] private Vector2 inputVector;
    [SerializeField] private MovementState movementState;

    [SerializeField] private Vector3 playerDirection = new Vector3();

    public float currentSpeed;
    public float walkSpeed = 3f;
    public float runSpeed = 6f;
    public float acceleration = 10f;

    public bool b_isSprinting;

    [Header("Jump Settings")]
    public float jumpHeight;
    public float gravity = -9.81f;
    public float stepDown;





    Vector3 playerVelocity;

    bool b_isIdle;
    bool b_isWalking;
    bool b_isJumping;

    //Components 
    private Animator playerAnimator;
    private CharacterController characterController;


    #region LIFECYCLE

    private void Awake()
    {
        //Get attached Components
        playerAnimator = GetComponent<Animator>();
        characterController = GetComponent<CharacterController>();
    }

    #endregion

    //MOVEMENT FUNCTIONS


    public void UpdatePlayerLocomotion(Vector2 inputVec)
    {
        HandlePlayerMovement(inputVec);
        ApplyGravity();

        characterController.Move(playerDirection.normalized * currentSpeed * Time.deltaTime);
    }
    
    public void HandlePlayerMovement(Vector2 inputVec)
    {
        //Get Input
        inputVector = inputVec;

        playerDirection = transform.forward * inputVector.y + transform.right * inputVector.x;

        //Set Animator Values
        playerAnimator.SetFloat("InputX", inputVector.x);
        playerAnimator.SetFloat("InputY", inputVector.y);

        //Determine target Speed 
        float targetSpeed = b_isSprinting ? walkSpeed : runSpeed;

        //Accelerate and Decelerate to target speed
        currentSpeed = Mathf.MoveTowards(currentSpeed, targetSpeed, acceleration * Time.deltaTime);


        /*
        if (inputVec != Vector2.zero) 
        {
            //playerVelocity.x = playerDirection.x * currentSpeed;
            //playerVelocity.z = playerDirection.y * currentSpeed;
        }
        else
        {
            //Player is Idle
           // movementState = MovementState.IDLE;

            //playerVelocity.x = Mathf.MoveTowards(playerVelocity.x, 0, acceleration * Time.deltaTime);
            //playerVelocity.z = Mathf.MoveTowards(playerVelocity.z, 0, acceleration * Time.deltaTime);
        }*/
    }


    public void ApplyGravity()
    {

        if (characterController.isGrounded && playerVelocity.y < 0) 
        {
            playerVelocity.y = -2f;        
        }
        else
        {
            playerVelocity.y += gravity * Time.deltaTime;
        }
    }
    
    public void HandleJump()
    {
        if (b_isJumping)
        {
            playerVelocity.y -= gravity * Time.deltaTime;
           
            characterController.Move(playerVelocity * Time.fixedDeltaTime);
            b_isJumping = !characterController.isGrounded; 
        }
        else
        {

        }
    }


    public void Jump()
    {
        if (!b_isJumping)
        {
            b_isJumping = true;
            playerVelocity.y = Mathf.Sqrt(2 * gravity * jumpHeight);
        }
    }

    public void Sprint()
    {
        b_isSprinting = true;

        movementState = MovementState.SPRINT;
    }
}
