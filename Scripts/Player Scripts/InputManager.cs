using UnityEngine;
public class InputManager : MonoBehaviour
{
    [Header("External References")]
    public PlayerInput playerInput;
    public PlayerInput.OnFootActions onFoot;
    private PlayerMotor playerMotor;
    private PlayerLook playerLook;
    private PlayerShoot playerShoot;
    private PlayerWeapon playerWeapon;
    public GameObject logicManager;
    private LogicScript logicScript;
    private WeaponSwitching weaponSwitching;

    void Awake()
    {
        playerInput = new PlayerInput();
        onFoot = playerInput.OnFoot;

        playerMotor = GetComponent<PlayerMotor>();
        playerLook = GetComponent<PlayerLook>();
        playerShoot = GetComponent<PlayerShoot>();
        playerWeapon = GetComponent<PlayerWeapon>();
        logicScript = logicManager.GetComponent<LogicScript>();
        weaponSwitching = GetComponent<WeaponSwitching>();

        onFoot.Jump.performed += ctx => playerMotor.Jump();
        onFoot.Sprint.performed += ctx => playerMotor.Sprint();
        onFoot.Shoot.performed += ctx => playerShoot.Shoot();
        onFoot.Aim.performed += ctx => playerWeapon.Aim();
        onFoot.Reload.performed += ctx => playerWeapon.Reload();
        onFoot.Pause.performed += ctx => logicScript.TogglePause();
        onFoot.SwitchWeapon.performed += x => weaponSwitching.mouseScrollY = x.ReadValue<float>();
        onFoot.SwitchWeapon.performed += ctx => weaponSwitching.SelectWeapon();
    }
    void FixedUpdate()
    {
        playerMotor.ProcessMove(onFoot.Movement.ReadValue<Vector2>());
    }
    private void LateUpdate()
    {
        playerLook.ProcessLook(onFoot.Look.ReadValue<Vector2>());
    }

    private void OnEnable()
    {
        onFoot.Enable();
    }
    private void OnDisable()
    {
        onFoot.Disable();
    }
}
