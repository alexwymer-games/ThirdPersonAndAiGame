using UnityEngine;

public class PlayerCharacterLocomotion : MonoBehaviour
{

    public Animator rigAnimator;

    public float jumpHeight;
    public float gravity;
    public float stepDown;
    public float airControl;
    public float jumpDamp;
    public float groundSpeed;

    public float pushPower = 2.0F;

    PlayerCharacterController playerCharacterController;

    Animator playerAnimator;
    CharacterController characterController;
    PlayerCharacterActiveWeapon activeWeapon;
    PlayerCharacterWeaponReload reloadWeapon;

    Vector2 inputVector;

    Vector3 rootMotion;
    Vector3 velocity;

    [SerializeField] bool isJumping = false;
    [SerializeField] bool isSprinting = false;
    [SerializeField] bool isGrounded;

    int isSprintingParam = Animator.StringToHash("IsSprinting");

    Vector3 spherePos;
    [SerializeField] private float groundYOffset;
    [SerializeField] private LayerMask groundLayerMask;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        playerCharacterController = GetComponent<PlayerCharacterController>();

        playerAnimator = GetComponent<Animator>();
        characterController = GetComponent<CharacterController>();
        activeWeapon = GetComponent<PlayerCharacterActiveWeapon>();
        reloadWeapon = GetComponent<PlayerCharacterWeaponReload>();
    }

    // Update is called once per frame
    public void UpdatePlayer(Vector2 inputVec)
    {
        //Modify inputVec 
        #region MODIFY VEC

        if (inputVec.x > 0)
        {
            inputVec.x = 1;
        }

        if (inputVec.x < 0)
        {
            inputVec.x = -1;
        }

        if (inputVec.y > 0)
        {
            inputVector.y = 1;
        }

        if (inputVec.y < 0)
        {
            inputVector.y = -1;
        }

        #endregion

        inputVector = inputVec;

        isGrounded = characterController.isGrounded;

        //Set Animator Values
        playerAnimator.SetFloat("InputX", inputVec.x);
        playerAnimator.SetFloat("InputY", inputVec.y);

        UpdateIsSprinting();
    }

    public void FixedUpdatePlayer()
    {
        if (isJumping)
        {
            UpdateInAir();
        }
        else
        {
            UpdateOnGround();
        }
    }


    private void UpdateInAir()
    {
        velocity.y -= gravity * Time.fixedDeltaTime;

        Vector3 displacement = velocity * Time.deltaTime;
        displacement += CalculateAirControl();
        characterController.Move(displacement);
        isJumping = !characterController.isGrounded;
        rootMotion = Vector3.zero;

        playerAnimator.SetBool("IsJumping", isJumping);
    }

    private void UpdateOnGround()
    {
        Vector3 stepForwardAmount = rootMotion * groundSpeed;
        Vector3 stepDownAmount = Vector3.down * stepDown;


        characterController.Move(stepForwardAmount + stepDownAmount);
        rootMotion = Vector3.zero;

        if (!characterController.isGrounded)
        {
            SetInAir(0);
        }
    }

    private void OnAnimatorMove()
    {
        rootMotion += playerAnimator.deltaPosition;
    }


    public void Jump()
    {
        if (!isJumping) 
        {
            float jumpVelocity = Mathf.Sqrt(2 * gravity * jumpHeight);
            SetInAir(jumpVelocity);
        }
    }

    public void Sprint()
    {
        isSprinting = !isSprinting;
        //playerAnimator.SetBool(isSprintingParam, true);
        //rigAnimator.SetBool(isSprintingParam, true);
    }


    private void UpdateIsSprinting()
    {
        bool playerIsSprinting = GetIsSprinting();

        playerAnimator.SetBool(isSprintingParam, playerIsSprinting);
        rigAnimator.SetBool(isSprintingParam, playerIsSprinting);
    }

    private bool GetIsSprinting()
    {
        bool isFiring = activeWeapon.IsFiring();
        bool isreloading = reloadWeapon.isReloading;
        bool isChangingWeapon = activeWeapon.isChangingWeapons;
        bool isAiming = playerCharacterController.b_isAiming;

        return isSprinting && !isFiring && !isreloading && !isChangingWeapon && !isAiming;
    }

    private void SetInAir(float jumpVelocity)
    {
        isJumping = true;
        velocity = playerAnimator.velocity * jumpDamp * groundSpeed;
        velocity.y = jumpVelocity;
        playerAnimator.SetBool("IsJumping", true);
    }

    Vector3 CalculateAirControl()
    {
        return ((transform.forward * inputVector.y) + (transform.right * inputVector.x)) * (airControl / 100);
    }

    void OnControllerColliderHit(ControllerColliderHit hit)
    {
        Rigidbody body = hit.collider.attachedRigidbody;

        // no rigidbody
        if (body == null || body.isKinematic)
            return;

        // We dont want to push objects below us
        if (hit.moveDirection.y < -0.3f)
            return;

        // Calculate push direction from move direction,
        // we only push objects to the sides never up and down
        Vector3 pushDir = new Vector3(hit.moveDirection.x, 0, hit.moveDirection.z);

        // If you know how fast your character is trying to move,
        // then you can also multiply the push velocity by that.

        // Apply the push
        body.linearVelocity = pushDir * pushPower;
    }

}
