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

    public Animator playerRigAnimator;
    


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        //Get Exisiting Weapon
        WeaponController existingWeapon = GetComponentInChildren<WeaponController>();

        //If Existing Weapon exisitng, equip it
        if (existingWeapon)
        {
            EquipWeapon(existingWeapon);
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

        string animationString = "PlayerEquip_" + activeWeaponController.weaponName;

        playerRigAnimator.Play(animationString);

        Debug.Log(animationString);
    }

    public void SetEquipBool(bool b_isHolstered)
    {
        bool weaponIsHolstered = b_isHolstered;

        playerRigAnimator.SetBool("HolsterWeapon", weaponIsHolstered);
    }

}
