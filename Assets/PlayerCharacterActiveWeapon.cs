using UnityEngine;
using UnityEngine.Animations.Rigging;
using UnityEditor.Animations;
using System.Net.Http.Headers;
using System.Collections;

public enum WeaponSlot
{ 
    PRIMARY = 0,
    SECONDARY = 1,
    TERTIARY = 2
}


public class PlayerCharacterActiveWeapon : MonoBehaviour
{
    //Components
    [SerializeField] private WeaponController[] equippedWeaponControllers = new WeaponController[2];

    public WeaponController activeWeaponController;
    public int activeWeaponIndex = 0;

    bool isHolstered = false;

    public Transform crosshairTarget;

    public Transform[] weaponSlots;
    public Rig playerWeaponsHandIK;

    public Transform weaponLeftGrip;
    public Transform weaponRightGrip;

    public Animator playerRigAnimator;
    


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        //Get Exisiting Weapon
        //WeaponController existingWeapon = GetComponentInChildren<WeaponController>();



        //If Existing Weapon exisitng, equip it
        //if (existingWeapon)
        //{
       //     EquipWeapon(existingWeapon);
        //}


    }

    private void Update()
    {
        activeWeaponController = GetEquippedWeapon(activeWeaponIndex);
    }

    public void BeginFiringWeapon()
    {
        if (activeWeaponController != null && !isHolstered) 
        {
            activeWeaponController.StartFiring();
        }
    }

    public void EndFiringWeapon() 
    {
        if (activeWeaponController != null && !isHolstered)
        {
            activeWeaponController.StopFiring();
        }
    }

    public void EquipWeapon(WeaponController weaponController)
    {
        int weaponSlotIndex = (int)weaponController.weaponSlotType;

        activeWeaponController = GetEquippedWeapon(weaponSlotIndex);


        if (activeWeaponController)
        {
            Destroy(activeWeaponController.gameObject);
        }

        activeWeaponController = weaponController;

        activeWeaponController.raycastDestination = crosshairTarget;

        activeWeaponController.transform.SetParent(weaponSlots[weaponSlotIndex], false);



        equippedWeaponControllers[weaponSlotIndex] = activeWeaponController;

        //Debug.Log(animationString);

        SetActiveWeapon(weaponController.weaponSlotType);
    }

    public void ToggleActiveWeapon()
    {
        bool isHolstered = playerRigAnimator.GetBool("HolsterWeapon");

        if (isHolstered) 
        { 
            StartCoroutine(ActivateWeapon(activeWeaponIndex));
        }
        else
        {
            StartCoroutine(HolsterWeapon(activeWeaponIndex));
        }
    }

    private void SetActiveWeapon(WeaponSlot weaponSlot)
    {
        int holsterIndex = activeWeaponIndex;
        int activateIndex = (int)weaponSlot;

        if (holsterIndex == activateIndex)
        {
            holsterIndex = -1;
        }

        StartCoroutine(SwitchWeapon(holsterIndex, activateIndex));
    }

    //Coroutines
    IEnumerator SwitchWeapon(int holsterIndex, int activateIndex)
    {
        yield return StartCoroutine(HolsterWeapon(holsterIndex));
        yield return StartCoroutine(ActivateWeapon(activateIndex));
        activeWeaponIndex = activateIndex;
    }

    IEnumerator HolsterWeapon(int index)
    {
        isHolstered = true;

        var weapon = GetEquippedWeapon(index);

        if (weapon)
        {
            
            playerRigAnimator.SetBool("HolsterWeapon", true);

            yield return new WaitForSeconds(0.1f);

            do
            {
                yield return new WaitForEndOfFrame();
            }
            while (playerRigAnimator.GetCurrentAnimatorStateInfo(0).normalizedTime < 1.0f);
        }
    }

    IEnumerator ActivateWeapon(int index)
    {
        var weapon = GetEquippedWeapon(index);

        if (weapon)
        {
            playerRigAnimator.SetBool("HolsterWeapon", false);

            string animationString = "PlayerEquip_" + weapon.weaponName;
            playerRigAnimator.Play(animationString);

            do
            {
                yield return new WaitForEndOfFrame();
            } while (playerRigAnimator.GetCurrentAnimatorStateInfo(0).normalizedTime < 1.0f);

            isHolstered = false;
        }
    }


    /*
    public void SetEquipBool(bool b_isHolstered)
    {
        bool weaponIsHolstered = b_isHolstered;

        playerRigAnimator.SetBool("HolsterWeapon", weaponIsHolstered);
    }*/

    public void EquipPrimaryWeapon()
    {
        SetActiveWeapon(WeaponSlot.PRIMARY);
    }

    public void EquipSecondaryWeapon() 
    {
        SetActiveWeapon(WeaponSlot.SECONDARY);
    }

        



    public WeaponController GetEquippedWeapon(int index)
    {
        if (index < 0 || index >= equippedWeaponControllers.Length)
        {
            return null;
        }
        return equippedWeaponControllers[index];
    }
}
