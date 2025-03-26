using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PlayerStats : MonoBehaviour
{
    [Header("External References")]
    private PlayerMotor playerMotor;
    private PlayerWeapon playerWeapon;
    public HealthBar healthBar;

    public GameObject staminaBar;
    private Slider staminaSlider;
    public GameObject enemyPrefab;

    [Header("General properties")]
    public int maxHealth = 100;
    public int currentHealth;
    public float maxStamina = 100f;
    public float currentStamina;
    public float runCost;

    private void Awake()
    {
        playerMotor = GetComponent<PlayerMotor>();
        playerWeapon = GetComponent<PlayerWeapon>();
        staminaSlider = staminaBar.GetComponent<Slider>();
    }
    void Start()
    {
        currentHealth = maxHealth;
        currentStamina = maxStamina;
    }
    private void Update()
    {
        if (currentStamina <= 0)
        {
            playerMotor.speed = playerMotor.walkingSpeed;
        }
        if (Input.GetKeyUp(KeyCode.N))
        {
            TakeDamage(20);
        }
        if (Input.GetKeyUp(KeyCode.M))
        {
            Heal(20);
        }
        if (Input.GetKeyUp(KeyCode.B))
        {
            Instantiate(enemyPrefab, transform.position, Quaternion.identity);
        }
    }
    public void TakeDamage(int damage)
    {
        currentHealth -= damage;
        healthBar.SetHealth(currentHealth);
        if (currentHealth <= 0)
        {
            Die();
        }
    }
    private void Die ()
    {
        playerMotor.isSprinting = false;
        SceneManager.LoadSceneAsync(SceneManager.GetActiveScene().buildIndex);
    }
    public void Heal(int healAmount)
    {
        if ((currentHealth += healAmount) <= maxHealth)
        {
            currentHealth += healAmount;
        }
        else
        {
            currentHealth = maxHealth;
        }
        healthBar.SetHealth(currentHealth);
    }
    public void LoseStamina()
    {
        currentStamina -= runCost * Time.deltaTime;
        staminaSlider.value = currentStamina;
    }
    public void RegenStamina()
    {
        currentStamina += runCost * Time.deltaTime;
        staminaSlider.value = currentStamina;
    }
}
