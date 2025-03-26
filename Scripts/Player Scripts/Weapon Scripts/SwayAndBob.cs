using UnityEngine;

public class SwayAndBob : MonoBehaviour
{
    [Header("External References")]
    private InputManager inputManager;
    private PlayerInput playerInput;
    private GunRecoil gunRecoil;
    private PlayerWeapon playerWeapon;

    public GameObject weaponHolder;
    public PlayerInput.OnFootActions onFoot;
    private CharacterController characterController;

    [Header("Sway position settings")]
    public float step = 0.01f;
    public float maxStepDistance = 0.06f;
    private Vector3 swayPosition;

    [Header("Sway position settings")]
    public float rotationStep = 4f;
    public float maxRotationStep = 5f;
    private Vector3 swayEulerRotation;

    [Header("General settings")]
    public float smoothPosition = 10f;
    public float smoothRotation = 12f;

    //Inputs for internal storage
    private Vector2 walkInput;
    private Vector2 lookInput;

    [Header("Bobbing")]
    public float speedCurve;
    float curveSin { get => Mathf.Sin(speedCurve); }
    float curveCos { get => Mathf.Cos(speedCurve); }

    public Vector3 travelLimit = Vector3.one * 0.025f;
    public Vector3 bobLimit = Vector3.one * 0.01f;
    Vector3 bobPosition;

    public float bobExaggeration;

    [Header("Bob Rotation")]
    public Vector3 multiplier;
    public Vector3 bobEulerRotation;
    private void Awake()
    {
        inputManager = GetComponent<InputManager>();
        gunRecoil = GetComponent<GunRecoil>();
        playerWeapon = GetComponent<PlayerWeapon>();
        characterController = GetComponent<CharacterController>();

        playerInput = inputManager.playerInput;
        onFoot = playerInput.OnFoot;
    }
    private void Update()
    {
        if (!gunRecoil.isRecoiling)
        {
            GetInput();
            SwayPosition();
            SwayRotation();
            BobOffset();
            BobRotation();
            CompositePositionRotation();
        }
    }
    private void GetInput()
    {
        walkInput = onFoot.Movement.ReadValue<Vector2>();
        lookInput = onFoot.Look.ReadValue<Vector2>();
    }
    private void SwayPosition()
    {
        Vector3 invertLook = lookInput * -step;
        invertLook.x = Mathf.Clamp(invertLook.x, -maxStepDistance, maxStepDistance);
        invertLook.y = Mathf.Clamp(invertLook.y, -maxStepDistance, maxStepDistance);
        swayPosition = invertLook + playerWeapon.gunPosition;
    }
    private void SwayRotation()
    {
        Vector2 invertLook = lookInput * -rotationStep;
        invertLook.x = Mathf.Clamp(invertLook.x, -maxRotationStep, maxRotationStep);
        invertLook.y = Mathf.Clamp(invertLook.y, -maxRotationStep, maxRotationStep);
        swayEulerRotation = new Vector3(invertLook.y, invertLook.x, invertLook.x) + playerWeapon.gunRotation;
    }
    void BobOffset()
    {
        Vector3 formattedBobLimit;
        if (playerWeapon.isAiming)
        {
            formattedBobLimit = bobLimit / 3;
        }
        else
        {
            formattedBobLimit = bobLimit;
        }
        speedCurve += Time.deltaTime * (characterController.isGrounded ? (Input.GetAxis("Horizontal") + Input.GetAxis("Vertical")) * bobExaggeration : 1f) + 0.01f;
        if(speedCurve >= 270)
        {
            speedCurve = 0;
        }
        bobPosition.x = (curveCos * formattedBobLimit.x * (characterController.isGrounded ? 1 : 0)) - (walkInput.x * travelLimit.x);
        bobPosition.y = (curveSin * formattedBobLimit.y) - (Input.GetAxis("Vertical") * travelLimit.y);
        bobPosition.z = -(walkInput.y * travelLimit.z);
    }

    void BobRotation()
    {
        bobEulerRotation.x = (walkInput != Vector2.zero ? multiplier.x * (Mathf.Sin(2 * speedCurve)) : multiplier.x * (Mathf.Sin(2 * speedCurve) / 2));
        bobEulerRotation.y = (walkInput != Vector2.zero ? multiplier.y * curveCos : 0);
        bobEulerRotation.z = (walkInput != Vector2.zero ? multiplier.z * curveCos * walkInput.x : 0);
    }
    private void CompositePositionRotation()
    {
        weaponHolder.transform.localPosition = Vector3.Lerp(weaponHolder.transform.localPosition,
            swayPosition + bobPosition,
            Time.deltaTime * smoothPosition);
        if (!playerWeapon.isAiming)
        {
            weaponHolder.transform.localRotation = Quaternion.Slerp(weaponHolder.transform.localRotation,
            Quaternion.Euler(swayEulerRotation) * Quaternion.Euler(bobEulerRotation),
            Time.deltaTime * smoothRotation);
        }
        else
        {
            weaponHolder.transform.localRotation = Quaternion.Slerp(weaponHolder.transform.localRotation,
            Quaternion.Euler(swayEulerRotation) * Quaternion.Euler(bobEulerRotation/4),
            Time.deltaTime * smoothRotation);
        }
    }
}
