using UnityEngine;

public class Hammer : MonoBehaviour
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
        playerWeapon.damage = 34;
        playerWeapon.range = 2f;
        playerWeapon.fireRate = 2f;
        playerWeapon.isMelee = true;

        gunRecoil.recoilX = 60;
    }
}
