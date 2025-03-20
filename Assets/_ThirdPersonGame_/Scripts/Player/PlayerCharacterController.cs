using UnityEngine;
using UnityEngine.Animations.Rigging;

public class PlayerCharacterController : MonoBehaviour
{
    //Game Controls 
    private GameControls gameControls;
    private GameControls.PlayerActions playerActions;

    //Player Components 
    private PlayerCharacterLocomotion playerCharacterLocomotion;
    private PlayerCharacterAiming playerCharacterAiming;
    private PlayerCharacterActions playerCharacterActions;

    //Player Weapon Components
    private PlayerCharacterActiveWeapon playerCharacterActiveWeapon;
    private PlayerCharacterWeaponReload playerCharacterWeaponReload;

    //Player State
    public PlayerState playerState = PlayerState.IDLE;


    //Player Variables 
    public bool b_isAiming = false;
    private bool b_weaponIsHolstered = true;

    #region LIFECYCLE
    private void Awake()
    {
        //Setup Controls 
        gameControls = new GameControls();
        playerActions = gameControls.Player;

        //Get attached Components
        playerCharacterLocomotion = GetComponent<PlayerCharacterLocomotion>();
        playerCharacterAiming = GetComponent<PlayerCharacterAiming>();
        playerCharacterActions = GetComponent<PlayerCharacterActions>();

        playerCharacterActiveWeapon = GetComponentInChildren<PlayerCharacterActiveWeapon>();
        playerCharacterWeaponReload = GetComponent<PlayerCharacterWeaponReload>();
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        playerCharacterLocomotion.UpdatePlayer(playerActions.Movement.ReadValue<Vector2>());
    }

    private void FixedUpdate()
    {
        playerCharacterLocomotion.FixedUpdatePlayer();
    }

    private void LateUpdate()
    {
        playerCharacterAiming.UpdateAimingRotations(playerActions.Look.ReadValue<Vector2>());
        playerCharacterAiming.UpdateAimWithRecoil();
    }

    private void OnEnable()
    {
        //Enable Controls and Set Callbacks
        playerActions.Enable();
        //Aim
        playerActions.Aim.performed += ctx => PlayerAimWeapon();
        playerActions.Aim.canceled += ctx => PlayerReturnToIdle();
        //Sprint and Run
        playerActions.Sprint.performed += ctx => PlayerSprint();
        playerActions.Jump.performed += ctx => PlayerJump();
        //Shoot
        playerActions.Shoot.performed += ctx => PlayerBeginShootingWeapon();
        playerActions.Shoot.canceled += ctx => PlayerStopShootingWeapon();
        //Holster
        playerActions.Holster.performed += ctx => ToggleHolsterWeapon();
        //Equip
        playerActions.EquipPrimary.performed += ctx => EquipPrimaryWeapon();
        playerActions.EquipSecondary.performed += ctx => EquipSecondaryWeapon();
        //Reload
        playerActions.Interact.performed += ctx => EvaluateInteractState();

        // Lock cursor
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    private void OnDisable()
    {
        playerActions.Disable();
        playerActions.Aim.performed -= ctx => PlayerAimWeapon();
        playerActions.Aim.canceled -= ctx => PlayerReturnToIdle();
        //Sprint and Run
        playerActions.Sprint.performed -= ctx => PlayerSprint();
        playerActions.Jump.performed -= ctx => PlayerJump();
        //SHoot
        playerActions.Shoot.performed -= ctx => PlayerBeginShootingWeapon();
        playerActions.Shoot.canceled -= ctx => PlayerStopShootingWeapon();

        playerActions.Holster.performed -= ctx => ToggleHolsterWeapon();

        playerActions.EquipPrimary.performed -= ctx => EquipPrimaryWeapon();
        playerActions.EquipSecondary.performed -= ctx => EquipSecondaryWeapon();

        playerActions.Interact.performed -= ctx => EvaluateInteractState();

        // Lock cursor
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }

    #endregion

    #region PLAYER ACTIONS

    private void PlayerSprint()
    {
        playerCharacterLocomotion.Sprint();
    }

    //Player Jump Functions 
    private void PlayerJump()
    {
        playerCharacterLocomotion.Jump();
    }


    //Aiming Functions 
    private void PlayerAimWeapon()
    {
        b_isAiming = true;
        playerCharacterAiming.UpdateAiming(b_isAiming);
    }

    private void PlayerReturnToIdle()
    {
        b_isAiming = false;
        playerCharacterAiming.UpdateAiming(b_isAiming);
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

    //Holster Weapon
    public void ToggleHolsterWeapon()
    {
        playerCharacterActiveWeapon.ToggleActiveWeapon();
    }

    //Equip Primary
    private void EquipPrimaryWeapon()
    {
        playerCharacterActiveWeapon.EquipPrimaryWeapon();
    }

    //Equip Secondary
    private void EquipSecondaryWeapon()
    {
        playerCharacterActiveWeapon.EquipSecondaryWeapon();
    }

    //Interact
    private void EvaluateInteractState()
    {
        switch (playerState)
        {
            case PlayerState.IDLE:

                break;

            case PlayerState.ARMED: case PlayerState.AIMED:
                playerCharacterWeaponReload.WeaponReload();
                break;

            
            case PlayerState.INTERACT:
                playerCharacterActions.InteractWithDoor();
                break;
        }
    }


    #endregion
}


public enum PlayerState
{
    IDLE,
    ARMED,
    AIMED,
    INTERACT,

}
