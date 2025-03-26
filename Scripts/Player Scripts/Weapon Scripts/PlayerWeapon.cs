using System.Collections;
using TMPro;
using UnityEngine;

public class PlayerWeapon : MonoBehaviour
{
    [Header("External References")]
    private PlayerMotor playerMotor;
    public GameObject ammoCounter;
    public GameObject weaponHolder;
    public GameObject muzzle;
    public TextMeshProUGUI ammoCounterText;
    public Animator animator;

    [Header("Gun Properties")]
    public int magazineAmmo = 12;
    public int magazineSize = 12;
    public int currentAmmo = 36;
    public int maxAmmo = 96;
    public int damage = 25;
    public float range = 100f;
    public float fireRate = 2f;
    public float reloadTime;
    public bool isReloading;
    public Vector3 gunRotation;
    public bool isMelee;

    [Header("Aiming properties")]
    public bool isAiming;
    public Vector3 gunPosition;
    public Vector3 idlePosition;
    public Vector3 aimedPosition;
    private void Awake()
    {
        playerMotor = GetComponent<PlayerMotor>();
    }
    public void Start()
    {
        ammoCounterText = ammoCounter.GetComponent<TextMeshProUGUI>();
        UpdateCounter();
    }
    public void SpendBullet()
    {
        if (!isMelee)
        {
            magazineAmmo--;
            UpdateCounter();
        }
    }
    public void IncreaseAmmo(int ammoAmount)
    {
        if (currentAmmo + ammoAmount > maxAmmo)
        {
            currentAmmo = maxAmmo;
        }
        else
        {
            currentAmmo += ammoAmount;
        }
    }
    public void Reload()
    {
        if (!isReloading && currentAmmo > 0 && magazineAmmo != magazineSize
            && !isMelee && !playerMotor.isActuallySprinting)
        {
            isReloading = true;
            animator.SetBool("isReloading", true);
            StartCoroutine(ReloadCoroutine());
        }
    }
    public IEnumerator ReloadCoroutine()
    {
        yield return new WaitForSeconds(reloadTime);
        int missingBullets = (magazineSize - magazineAmmo);
        if (currentAmmo - missingBullets >= 0)
        {
            currentAmmo -= missingBullets; //Takes missing bullets from magazine from current ammo, that is in bag
            magazineAmmo += missingBullets; //Adds to the magazine the amount of missing bullets
        }
        else
        {
            magazineAmmo += currentAmmo; //Gives the magazine more ammo based on the missing bullets in it
            currentAmmo = 0;
        }
        UpdateCounter();
        isReloading = false;
        animator.SetBool("isReloading", false);
    }
    public void Aim()
    {
        if (!isMelee)
        {
            isAiming = !isAiming;
            if (isAiming)
            {
                gunPosition = aimedPosition;
            }
            else
            {
                gunPosition = idlePosition;
            }
        }
    }
    public void UpdateCounter()
    {
        ammoCounterText.text = (magazineAmmo + "/" + currentAmmo);
    }
}
