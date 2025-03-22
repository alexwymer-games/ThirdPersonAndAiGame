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

    private WeaponRecoil activeWeaponRecoil;

    bool isHolstered = false;

    public Transform crosshairTarget;

    public Transform[] weaponSlots;
    public Rig playerWeaponsHandIK;

    public Transform weaponLeftGrip;
    public Transform weaponRightGrip;

    public Animator playerRigAnimator;

    public bool isChangingWeapons = false;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
  
    }

    private void Update()
    {
        activeWeaponController = GetEquippedWeapon(activeWeaponIndex);

        bool notSpriting = playerRigAnimator.GetCurrentAnimatorStateInfo(2).shortNameHash == Animator.StringToHash("NotSprinting");

        //Dont update weapon if !notSprinting
    }

    //Firing Functions

    public void BeginFiringWeapon()
    {
        if (activeWeaponController != null && !isHolstered) 
        {
            //Play Audio
            AudioManager.audioManagerInstance.PlaySFX(SoundType.RIFLE_SHOT);

            activeWeaponController.StartFiring();
            activeWeaponRecoil.GenerateRecoil(activeWeaponController.weaponName);

            //Trigger Event - Updates PlayerAiming Recoil Variables
            EventManager.TriggerEvent(EventType.UPDATE_WEAPON_RECOIL, activeWeaponRecoil);
        }
    }

    public void EndFiringWeapon() 
    {
        if (activeWeaponController != null && !isHolstered)
        {
            activeWeaponController.StopFiring();
        }
    }

    public bool IsFiring()
    {
        WeaponController currentWeapon = GetActiveWeapon();
        if (!currentWeapon)
        {
            return false;
        }
        return currentWeapon.isFiring;
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

        SetActiveWeapon(weaponController.weaponSlotType);

        //Get Weapon Recoil
        activeWeaponRecoil = activeWeaponController.GetComponent<WeaponRecoil>();
        activeWeaponRecoil.playerRigAnimator = playerRigAnimator;
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
        playerRigAnimator.SetInteger("WeaponIndex", activateIndex);

        yield return StartCoroutine(HolsterWeapon(holsterIndex));
        yield return StartCoroutine(ActivateWeapon(activateIndex));
        activeWeaponIndex = activateIndex;
    }

    IEnumerator HolsterWeapon(int index)
    {
        isHolstered = true;

        isChangingWeapons = true;

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

        isChangingWeapons = false;

       
    }

    IEnumerator ActivateWeapon(int index)
    {
        isChangingWeapons = true;

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

        isChangingWeapons = false;
    }



    public void EquipPrimaryWeapon()
    {
        SetActiveWeapon(WeaponSlot.PRIMARY);
    }

    public void EquipSecondaryWeapon() 
    {
        SetActiveWeapon(WeaponSlot.SECONDARY);
    }

        
    //Getters
    public WeaponController GetActiveWeapon()
    {
        return GetEquippedWeapon(activeWeaponIndex);
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
