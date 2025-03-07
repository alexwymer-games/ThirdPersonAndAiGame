using UnityEngine;

public class PlayerCharacterController : MonoBehaviour
{
    //Game Controls 
    private GameControls gameControls;
    private GameControls.PlayerActions playerActions;

    //Components 
    private PlayerCharacterLocomotion playerCharacterLocomotion;
    private PlayerCharacterAiming playerCharacterAiming;

    private PlayerCharacterActiveWeapon playerCharacterActiveWeapon;
   


    //Player Variables 

    private bool b_isAiming = false;

    #region LIFECYCLE
    private void Awake()
    {
        //Setup Controls 
        gameControls = new GameControls();
        playerActions = gameControls.Player;

        //Get attached Components
        playerCharacterLocomotion = GetComponent<PlayerCharacterLocomotion>();
        playerCharacterAiming = GetComponent<PlayerCharacterAiming>();

        playerCharacterActiveWeapon = GetComponentInChildren<PlayerCharacterActiveWeapon>();
        
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        playerCharacterLocomotion.MovePlayer(playerActions.Movement.ReadValue<Vector2>());

        playerCharacterAiming.AimWeapon(b_isAiming);
    }

    private void FixedUpdate()
    {
        
    }

    private void LateUpdate()
    {
        playerCharacterAiming.UpdateAimingRotations(playerActions.Look.ReadValue<Vector2>());
    }

    private void OnEnable()
    {
        playerActions.Enable();
        playerActions.Aim.performed += ctx => PlayerAimWeapon();
        playerActions.Aim.canceled += ctx => PlayerReturnToIdle();

        playerActions.Shoot.performed += ctx => PlayerBeginShootingWeapon();
        playerActions.Shoot.canceled += ctx => PlayerStopShootingWeapon();

        // Lock cursor
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    private void OnDisable()
    {
        playerActions.Disable();
        playerActions.Aim.performed -= ctx => PlayerAimWeapon();
        playerActions.Aim.canceled -= ctx => PlayerReturnToIdle();

        playerActions.Shoot.performed -= ctx => PlayerBeginShootingWeapon();
        playerActions.Shoot.canceled -= ctx => PlayerStopShootingWeapon();

        // Lock cursor
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }

    //Aiming Functions 
    private void PlayerAimWeapon()
    {
        b_isAiming = true;
    }

    private void PlayerReturnToIdle()
    {
        b_isAiming = false;
    }

    //Shooting Functions 
    private void PlayerBeginShootingWeapon()
    {
        if (b_isAiming)
        {
            playerCharacterActiveWeapon.BeginFiringWeapon();
        }
    }

    private void PlayerStopShootingWeapon()
    {
        if (b_isAiming)
        {
            playerCharacterActiveWeapon.EndFiringWeapon();
        }
    }

    #endregion
}
