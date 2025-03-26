using UnityEngine;

public class PlayerMotor : MonoBehaviour
{
    [Header("External references")]
    private CharacterController characterController;
    private PlayerStats playerStats;
    private PlayerWeapon playerWeapon;

    [Header("General settings")]
    private Vector3 playerVelocity;
    public float speed = 4f;
    public float walkingSpeed = 4f;
    public float runningSpeed = 8f;
    private bool isGrounded;
    public float gravity = -19.6f;
    public float jumpHeight = 1.5f;
    public bool isSprinting;
    public bool isActuallySprinting;

    void Awake()
    {
        playerWeapon = GetComponent<PlayerWeapon>();
        playerStats = GetComponent<PlayerStats>();
        characterController = GetComponent<CharacterController>();
    }
    void Update()
    {
        isGrounded = characterController.isGrounded;
    }
    public void ProcessMove(Vector2 input)
    {
        Vector3 moveDirection = Vector3.zero;
        moveDirection.x = input.x;
        moveDirection.z = input.y;
        characterController.Move(transform.TransformDirection(moveDirection) * speed * Time.deltaTime);

        if (isSprinting && moveDirection != Vector3.zero && playerStats.currentStamina > 0 && !playerWeapon.isAiming)
        {
            playerStats.LoseStamina();
            playerWeapon.animator.SetBool("isRunning", true);
            isActuallySprinting = true;
        }
        else if (!isSprinting && playerStats.currentStamina < playerStats.maxStamina
            || moveDirection == Vector3.zero && playerStats.currentStamina < playerStats.maxStamina)
        {
            playerStats.RegenStamina();
            playerWeapon.animator.SetBool("isRunning", false);
            isActuallySprinting = false;
        }

        playerVelocity.y += gravity * Time.deltaTime;
        if (isGrounded && playerVelocity.y < 0)
        {
            playerVelocity.y = -2;
        }
        characterController.Move(playerVelocity * Time.deltaTime);
    }
    public void Jump()
    {
        if (isGrounded)
        {
            playerVelocity.y = Mathf.Sqrt(jumpHeight * -3.0f * gravity);
        }
    }
    public void Sprint()
    {
        isSprinting = !isSprinting;
        if (isSprinting)
        {
            if (playerStats.currentStamina > 10)
            {
                speed = runningSpeed;
            }
        }
        else
        {
            speed = walkingSpeed;
        }
    }
}
