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

    [Header("Ground and Gravity")]
    [SerializeField] private bool b_isGrounded;
    [SerializeField] private float groundYOffset;
    [SerializeField] private LayerMask groundLayerMask;
    public float gravity = -9.81f;
    public float stepDown;
    Vector3 spherePos;

    Vector3 playerVelocity;

    bool b_isIdle;
    bool b_isWalking;
    bool b_isJumping = false;

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

    //UPDATE - Updates all player locomotion, called in PlayerCharacterController
    public void UpdatePlayerLocomotion(Vector2 inputVec)
    {
        HandlePlayerMovement(inputVec);
        //HandleJump();

        ApplyGravity();
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

        characterController.Move(playerDirection.normalized * currentSpeed * Time.deltaTime);
    }


    private void OnAnimatorMove()
    {
        
    }

    public void ApplyGravity()
    {

        if (GroundCheck() && playerVelocity.y < 0) //IS GROUNDED
        {
            //Debug.Log("Grounded");
            playerVelocity.y = -2f;        
        }
        else //IS IN AIR
        {
            //Debug.Log("Not Grounded");
            playerVelocity.y += gravity * Time.deltaTime;
        }

        characterController.Move(playerVelocity * Time.deltaTime);
    }

    private bool GroundCheck()
    {
        spherePos = new Vector3(transform.position.x, transform.position.y - groundYOffset, transform.position.z);

        if (Physics.CheckSphere(spherePos, characterController.radius - 0.05f, groundLayerMask))
        {
            return true;
        }
        return false;
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
            Debug.Log("Jump");

            b_isJumping = true;
            playerVelocity.y = Mathf.Sqrt(2 * gravity * jumpHeight);
        }
    }

    public void Sprint()
    {
        b_isSprinting = true;

        movementState = MovementState.SPRINT;
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawSphere(spherePos, 0.3f);
    }
}
