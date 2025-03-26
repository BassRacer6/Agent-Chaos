using UnityEngine;

public class WeaponSwitching : MonoBehaviour
{
    [Header("External References")]
    public GameObject weapons;
    private PlayerWeapon playerWeapon;

    [Header("General properties")]
    [SerializeField] private int selectedWeaponIndex = 0;
    [SerializeField] private int maxWeaponIndex;
    public float mouseScrollY;
    void Start()
    {
        playerWeapon = GetComponent<PlayerWeapon>();
        maxWeaponIndex = -1;
        foreach (Transform weapon in weapons.transform)
        {
            maxWeaponIndex++;
        }
    }
    public void SelectWeapon()
    {
        if (mouseScrollY > 0)
        {
            selectedWeaponIndex++;
            if (selectedWeaponIndex > maxWeaponIndex)
            {
                selectedWeaponIndex = 0;
            }
        }
        else if (mouseScrollY < 0)
        {
            selectedWeaponIndex--;
            if (selectedWeaponIndex < 0)
            {
                selectedWeaponIndex = maxWeaponIndex;
            }
        }
        int i = 0;
        foreach(Transform weapon in weapons.transform)
        {
            if (i == selectedWeaponIndex)
            {
                weapon.gameObject.SetActive(true);
                if (weapon.CompareTag("Pistol"))
                {
                    playerWeapon.animator.SetTrigger("equippedPistol");
                }
                else if (weapon.CompareTag("Melee"))
                {
                    playerWeapon.animator.SetTrigger("equippedMelee");
                }
            }
            else
            {
                weapon.gameObject.SetActive(false);
            }
            i++;
        }
    }
}
