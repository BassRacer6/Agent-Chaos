using UnityEngine;

public class GunRecoil : MonoBehaviour
{
    [Header("External References")]
    public GameObject weaponHolder;
    private PlayerWeapon playerWeapon;

    [Header("Recoil XYZ")]
    public float recoilX;
    [SerializeField] private float recoilY;
    [SerializeField] private float recoilZ;

    [Header("Recoil Settings")]
    [SerializeField] private float snappiness;
    [SerializeField] private float returnSpeed;
    public bool isRecoiling;
    [SerializeField] private float recoilTolerance = 0.5f;
    [SerializeField] private float recoilDampner;

    private Quaternion currentRotation;
    [SerializeField] private Quaternion targetRotation;

    private void Start()
    {
        playerWeapon = GetComponent<PlayerWeapon>();
        targetRotation = Quaternion.Euler(playerWeapon.gunRotation);
    }
    private void FixedUpdate()
    {
        RecoilCompositePositionRotation();
    }
    private void RecoilCompositePositionRotation()
    {
        if (Quaternion.Angle(targetRotation, Quaternion.Euler(playerWeapon.gunRotation)) > recoilTolerance)
        {
            isRecoiling = true;
            targetRotation = Quaternion.Lerp(targetRotation, Quaternion.Euler(playerWeapon.gunRotation), returnSpeed * Time.deltaTime); //Lerps back to original position
            currentRotation = Quaternion.Slerp(currentRotation, targetRotation, snappiness * Time.deltaTime); //Lerps from original to recoiled
            weaponHolder.transform.localRotation = currentRotation; //Applies angles to the GameObject
        }
        else
        {
            if (isRecoiling)
            {
                isRecoiling = false;
            }
        }
    }
    public void FireRecoil()
    {
        Quaternion recoilXRotation;
        Quaternion recoilYRotation;
        Quaternion recoilZRotation;
        if (playerWeapon.isAiming)
        {
            recoilXRotation = Quaternion.Euler(recoilX/recoilDampner, 0, 0);
            recoilYRotation = Quaternion.Euler(0, Random.Range(-recoilY, recoilY) / recoilDampner, 0);
            recoilZRotation = Quaternion.Euler(0, 0, recoilZ / recoilDampner);
        }
        else
        {
            recoilXRotation = Quaternion.Euler(recoilX, 0, 0);
            recoilYRotation = Quaternion.Euler(0, Random.Range(-recoilY, recoilY), 0);
            recoilZRotation = Quaternion.Euler(0, 0, recoilZ);
        }
        targetRotation *= recoilXRotation * recoilYRotation * recoilZRotation;
    }
}