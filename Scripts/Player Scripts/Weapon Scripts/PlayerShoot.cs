using UnityEngine;
using UnityEngine.InputSystem.HID;

public class PlayerShoot : MonoBehaviour
{
    [Header("External References")]
    private PlayerWeapon playerWeapon;
    private GunRecoil gunRecoil;
    private PlayerMotor playerMotor;
    private LogicScript logicScript;
    public AudioSource gunAudioSource;
    public AudioClip glockAudioClip;

    [Header("Impact effects references")]
    public ParticleSystem muzzleFlash;
    public GameObject impactEffectWall;
    public ParticleSystem bloodSplat;
    public GameObject impactEffectEnemy;

    private float nextTimeToFire = 0f;
    //private LineRenderer lineRenderer;

    public void Awake()
    {
        playerWeapon = GetComponent<PlayerWeapon>();
        gunRecoil = GetComponent<GunRecoil>();
        playerMotor = GetComponent<PlayerMotor>();
        logicScript = GameObject.FindGameObjectWithTag("Logic").GetComponent<LogicScript>();
        //lineRenderer = GetComponent<LineRenderer>();
        //lineRenderer.positionCount = 2;
    }
    public void Update()
    {
        Vector3 startPosition = playerWeapon.muzzle.transform.position;
        Vector3 endPosition = startPosition + playerWeapon.muzzle.transform.forward * 100f;
        //lineRenderer.SetPosition(0, startPosition);
        //lineRenderer.SetPosition(1, endPosition);
    }

    public void Shoot()
    {
        if ((playerWeapon.magazineAmmo > 0 || playerWeapon.isMelee) && !playerWeapon.isReloading &&
            Time.time >= nextTimeToFire && !playerMotor.isActuallySprinting &&
            !logicScript.isPaused)
        {
            nextTimeToFire = Time.time + 1f / playerWeapon.fireRate;
            gunRecoil.FireRecoil();
            if (!playerWeapon.isMelee)
            {
                playerWeapon.SpendBullet();
                muzzleFlash.Play();
                gunAudioSource.PlayOneShot(glockAudioClip);
            }
            RaycastHit hit;
            if (Physics.Raycast(playerWeapon.muzzle.transform.position, playerWeapon.muzzle.transform.forward, out hit, playerWeapon.range))
                {
                EnemyScript target = hit.transform.GetComponentInParent<EnemyScript>();
                if (target != null)
                {
                    if (hit.collider.CompareTag("Head"))
                    {
                        target.TakeDamage((playerWeapon.damage * 3)/2);
                    }
                    else
                    {
                        target.TakeDamage(playerWeapon.damage);
                    }
                    GameObject impactGO = Instantiate(impactEffectEnemy,
                        hit.point + (hit.normal * 0.1f),
                        Quaternion.LookRotation(hit.normal));
                    bloodSplat.Play();
                    Destroy(impactGO, 0.2f);
                }
                else
                {
                    GameObject impactGO = Instantiate(impactEffectWall,
                    hit.point + (hit.normal * 0.001f),
                    Quaternion.identity);
                    impactGO.transform.LookAt(hit.point + hit.normal);
                    impactGO.transform.Rotate(new Vector3(90f, 0f, 0f));
                    Destroy(impactGO, 3f);
                }
            }
        }
    }
}
