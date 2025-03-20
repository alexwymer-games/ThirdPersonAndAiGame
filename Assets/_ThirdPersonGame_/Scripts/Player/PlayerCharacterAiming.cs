using Unity.Cinemachine;
using UnityEngine;
using UnityEngine.Animations.Rigging;

public class PlayerCharacterAiming : MonoBehaviour
{
    //Cinemachine 
    private CinemachineImpulseListener cinemachineImpulseListener;


    //Cinemachine 
    public InputAxis xAxis;
    public InputAxis yAxis;


    [SerializeField] private Transform camFollowPosition;
    [SerializeField] private float mouseSensitivity = 1f;

    private float xRotation;
    private float yRotation;

    //private Rigidbody playerRigidBody;
    private CharacterController characterController;

    [SerializeField] private float turnSpeed = 15;

    private Camera mainCamera;

    [SerializeField] private float aimDuration;


    private Animator playerAnimator;

    int isAimingParam = Animator.StringToHash("IsAiming");

    //Weapon Recoil Stuff
    private float recoilTime;
    private float verticalRecoil;
    private float horizontalRecoil;
    private float recoilDuration;

    private float recoilModifier = 1.0f;



    #region LIFECYCLE

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        //Get Components 
        //playerRigidBody = GetComponent<Rigidbody>();

        characterController = GetComponent<CharacterController>();
        cinemachineImpulseListener = GetComponentInChildren<CinemachineImpulseListener>();

        playerAnimator = GetComponent<Animator>();

        mainCamera = Camera.main;
    }
    

    public void UpdateAimingRotations(Vector2 inputVec)
    {
        // Get input from new Input System
        Vector2 lookDelta = inputVec;

        // Apply sensitivity
        xRotation += lookDelta.x * mouseSensitivity;
        yRotation -= lookDelta.y * mouseSensitivity;

        if (recoilTime > 0)
        {
            //Modify Y Rotation with Recoil Values
            yRotation -= (((verticalRecoil / 100) * Time.deltaTime) / recoilDuration) * recoilModifier;
            xRotation -= (((horizontalRecoil / 10) * Time.deltaTime) / recoilDuration) * recoilModifier;
            recoilTime -= Time.deltaTime;
        }

        // Clamp vertical rotation
        yRotation = Mathf.Clamp(yRotation, -30f, 40f);

        // Apply rotations
        camFollowPosition.localEulerAngles = new Vector3(yRotation, camFollowPosition.localEulerAngles.y, camFollowPosition.localEulerAngles.z);
        //playerRigidBody.MoveRotation(Quaternion.Slerp(transform.rotation, Quaternion.Euler(0, xRotation, 0), turnSpeed * Time.deltaTime));
        gameObject.transform.rotation = (Quaternion.Slerp(transform.rotation, Quaternion.Euler(0, xRotation, 0), turnSpeed * Time.deltaTime));
    }

    private void OnEnable()
    {
        //Subscribe to Events
        EventManager.StartListening<WeaponRecoil>(EventType.UPDATE_WEAPON_RECOIL, UpdateCurrentWeaponRecoil);
        EventManager.StartListening(EventType.TRIGGER_WEAPON_RECOIL, TriggerWeaponRecoil);


    }

    private void OnDisable()
    {
        //Unsubscribe to Events
        EventManager.StopListening<WeaponRecoil>(EventType.UPDATE_WEAPON_RECOIL, UpdateCurrentWeaponRecoil);
        EventManager.StopListening(EventType.TRIGGER_WEAPON_RECOIL, TriggerWeaponRecoil);

    }

    #endregion

    public void UpdateAimWithRecoil()
    {
        /*
        if (recoilTime > 0)
        {
            //Modify Y Rotation with Recoil Values
            yRotation -= ((verticalRecoil / 1000) * Time.deltaTime) / recoilDuration;
            xRotation -= ((horizontalRecoil / 10) * Time.deltaTime) / recoilDuration;
            recoilTime -= Time.deltaTime;

            // Apply rotations
            camFollowPosition.localEulerAngles = new Vector3(yRotation, camFollowPosition.localEulerAngles.y, camFollowPosition.localEulerAngles.z);
        }
        */
    }

    //Get Recoil Values for the equipped weapon
    private void UpdateCurrentWeaponRecoil(WeaponRecoil weaponRecoil)
    {
        recoilTime = weaponRecoil.recoilTime;
        recoilDuration = weaponRecoil.recoilDuration;
        verticalRecoil = weaponRecoil.verticalRecoil;
        horizontalRecoil = weaponRecoil.horizontalRecoil;
        recoilModifier = weaponRecoil.recoilModifier;
    }

    //Trigger Recoil - Reset the recoil timer
    private void TriggerWeaponRecoil()
    {
        recoilTime = recoilDuration;
    }
    

    public void UpdateAiming(bool isAiming)
    {
        playerAnimator.SetBool(isAimingParam, isAiming);

        recoilModifier = isAiming ? 0.3f : 1f;
    }


}
