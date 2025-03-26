using UnityEngine;

public class Glock22 : MonoBehaviour
{
    [Header("External References")]
    private PlayerWeapon playerWeapon;
    private GunRecoil gunRecoil;
    private void Awake()
    {
        playerWeapon = GetComponentInParent<PlayerWeapon>();
        gunRecoil = GetComponentInParent<GunRecoil>();
    }
    private void OnEnable()
    {
        playerWeapon.damage = 25;
        playerWeapon.range = 100f;
        playerWeapon.fireRate = 4f;
        playerWeapon.reloadTime = 1.5f;
        playerWeapon.isMelee = false;

        gunRecoil.recoilX = -30;
    }
}
