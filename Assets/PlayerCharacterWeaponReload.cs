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

            case "Pull_Slide":

                break;

            case "Release_Slide":

                break;
        }
    }


    private void DetachMag()
    {
        WeaponController weapon = activeWeaponController.GetActiveWeapon();

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

        weapon.weaponMagazine.SetActive(true);

        Destroy(inHandMagazine);

        weapon.ammoCount = weapon.clipSize;

        playerRigAnimator.ResetTrigger("ReloadWeapon");
    }

    private void PullSlide()
    {

    }

    private void ReleaseSlide()
    {

    }
}
