using UnityEngine;
using UnityEngine.Animations.Rigging;

public class PlayerCharacterAiming : MonoBehaviour
{
    [SerializeField] private Transform camFollowPosition;
    [SerializeField] private float mouseSensitivity = 1f;

    private float xRotation;
    private float yRotation;

    private Rigidbody playerRigidBody;

    [SerializeField] private float turnSpeed = 15;

    private Camera mainCamera;

    [SerializeField] private Rig rigAimLayer;
    [SerializeField] private float aimDuration;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

        playerRigidBody = GetComponent<Rigidbody>();

        mainCamera = Camera.main;
    }
    

    public void UpdateAimingRotations(Vector2 inputVec)
    {
        // Get input from new Input System
        Vector2 lookDelta = inputVec;

        // Apply sensitivity
        xRotation += lookDelta.x * mouseSensitivity;
        yRotation -= lookDelta.y * mouseSensitivity;

        // Clamp vertical rotation
        yRotation = Mathf.Clamp(yRotation, -30f, 40f);

        // Apply rotations
        camFollowPosition.localEulerAngles = new Vector3(yRotation, camFollowPosition.localEulerAngles.y, camFollowPosition.localEulerAngles.z);
        playerRigidBody.MoveRotation(Quaternion.Slerp(transform.rotation, Quaternion.Euler(0, xRotation, 0), turnSpeed * Time.deltaTime));
    }

    

    public void AimWeapon(bool b_aim)
    {
        if (b_aim) 
        {
            rigAimLayer.weight += Time.deltaTime / aimDuration;
        }
        else
        {
             rigAimLayer.weight -= Time.deltaTime / aimDuration;
        }
    }
}
