using UnityEngine;
using UnityEngine.Animations.Rigging;
using UnityEditor.Animations;
using System.Net.Http.Headers;

public class PlayerCharacterActiveWeapon : MonoBehaviour
{
    //Components
    [SerializeField] private WeaponController activeWeaponController;


    public Transform crosshairTarget;


    public Transform weaponParent;
    public Rig playerWeaponsHandIK;

    public Transform weaponLeftGrip;
    public Transform weaponRightGrip;

    Animator playerAnimator;
    AnimatorOverrideController playerAnimatorOverride;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        //Get and Setup Animators
        playerAnimator = GetComponent<Animator>();
        playerAnimatorOverride = playerAnimator.runtimeAnimatorController as AnimatorOverrideController;


        //Get Exisiting Weapon
        WeaponController existingWeapon = GetComponentInChildren<WeaponController>();

        //If Existing Weapon exisitng, equip it
        if (existingWeapon)
        {
            EquipWeapon(existingWeapon);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (activeWeaponController)
        {
            playerWeaponsHandIK.weight = 1.0f;
            playerAnimator.SetLayerWeight(1, 1.0f);
        }
        else
        {
            playerWeaponsHandIK.weight = 0.0f;
            playerAnimator.SetLayerWeight(1, 0.0f);
        }
    }




    public void BeginFiringWeapon()
    {
        if (activeWeaponController != null) 
        {
            activeWeaponController.StartFiring();
        }
    }

    public void EndFiringWeapon() 
    {
        if (activeWeaponController != null)
        {
            activeWeaponController.StopFiring();
        }
    }

    public void EquipWeapon(WeaponController weaponController)
    {
        if (activeWeaponController)
        {
            Destroy(activeWeaponController.gameObject);
        }

        activeWeaponController = weaponController;

        activeWeaponController.raycastDestination = crosshairTarget;

        activeWeaponController.transform.parent = weaponParent;
        activeWeaponController.transform.localPosition = Vector3.zero;
        activeWeaponController.transform.localRotation = Quaternion.identity;

        playerWeaponsHandIK.weight = 1.0f;
        playerAnimator.SetLayerWeight(1, 1.0f);

        Invoke(nameof(SetAnimationDelay), 0.01f);
    }

    private void SetAnimationDelay()
    {
        playerAnimatorOverride["WeaponAnim_Empty"] = activeWeaponController.weaponAnimation;
    }


    [ContextMenu("Save Weapon Pose")]
    public void SaveWeaponPose()
    {
        GameObjectRecorder recorder = new GameObjectRecorder(gameObject);

        recorder.BindComponentsOfType<Transform>(activeWeaponController.gameObject, false);
        recorder.BindComponentsOfType<Transform>(weaponLeftGrip.gameObject, false);
        recorder.BindComponentsOfType<Transform>(weaponRightGrip.gameObject, false);
        recorder.TakeSnapshot(0.0f);
        recorder.SaveToClip(activeWeaponController.weaponAnimation);

        UnityEditor.AssetDatabase.SaveAssets();
    }
}
