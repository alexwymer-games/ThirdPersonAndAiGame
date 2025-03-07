using UnityEngine;
using UnityEngine.Animations.Rigging;

public class PlayerCharacterActiveWeapon : MonoBehaviour
{
    //Components
    [SerializeField] private WeaponController activeWeaponController;


    public Transform crosshairTarget;

    public Rig playerWeaponsHandIK;


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

    // Update is called once per frame
    void Update()
    {
        if (activeWeaponController)
        {
            playerWeaponsHandIK.weight = 1.0f;
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
        activeWeaponController = weaponController;

        activeWeaponController.raycastDestination = crosshairTarget;
    }
}
