using UnityEngine;

public class WeaponPickup : MonoBehaviour
{

    [SerializeField] private WeaponController weaponPrefab;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider collison)
    {
        if (collison.CompareTag("Player"))
        {
            PlayerCharacterActiveWeapon activeWeapon = collison.gameObject.GetComponent<PlayerCharacterActiveWeapon>();

            WeaponController weaponController = Instantiate(weaponPrefab);

            activeWeapon.EquipWeapon(weaponController);

        }
    }
}
