using UnityEngine;

public class PlayerCharacterWeaponReload : MonoBehaviour
{

    [SerializeField] private Animator playerRigAnimator;
    public WeaponAnimationEvents weaponAnimationEvents;

    public PlayerCharacterActiveWeapon activeWeaponController;

    public Transform leftHandTransform;

    public GameObject inHandMagazine;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        activeWeaponController = GetComponent<PlayerCharacterActiveWeapon>();

        weaponAnimationEvents.WeaponAnimationEvent.AddListener(OnAnimationEvent);
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void WeaponReload()
    {
        playerRigAnimator.SetTrigger("ReloadWeapon");
    }

    private void OnAnimationEvent(string eventName)
    {

        switch (eventName) 
        {
            case "Detach_Mag":
                DetachMag();
                break;

            case "Drop_Mag":
                DropMag();
                break;

            case "Pickup_Mag":
                Pickup_Mag();
                break;

            case "Attach_Mag":
                AttachMag();
                break;

            case "Release_Slide":
                ReleaseSlide();
                break;
        }
    }


    private void DetachMag()
    {
        //Get Active Weapon
        WeaponController weapon = activeWeaponController.GetActiveWeapon();

        //Play Audio
        AudioManager.audioManagerInstance.PlaySFX(weapon.gunUnloadSFX);

        inHandMagazine = Instantiate(weapon.weaponMagazine, leftHandTransform, true);

        weapon.weaponMagazine.SetActive(false);

    }

    private void DropMag()
    {
        

        GameObject droppedMag = Instantiate(inHandMagazine, leftHandTransform.transform.position, leftHandTransform.transform.rotation);
        droppedMag.AddComponent<Rigidbody>();
        droppedMag.AddComponent<BoxCollider>();

        inHandMagazine.SetActive(false);
    }

    private void Pickup_Mag()
    {
        inHandMagazine.SetActive(true);
    }

    private void AttachMag()
    {
        WeaponController weapon = activeWeaponController.GetActiveWeapon();

        //Play Audio
        AudioManager.audioManagerInstance.PlaySFX(weapon.gunLoadSFX);

        weapon.weaponMagazine.SetActive(true);

        Destroy(inHandMagazine);

        weapon.currentAmmoCount = weapon.clipSize;

        playerRigAnimator.ResetTrigger("ReloadWeapon");
    }

    private void ReleaseSlide()
    {
        //Get Active Weapon
        WeaponController weapon = activeWeaponController.GetActiveWeapon();

        //Play Audio
        AudioManager.audioManagerInstance.PlaySFX(weapon.gunReleaseSlideSFX);
    }
}
