using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    public GameObject healthBar;
    public GameObject easeHealthBar;
    private Slider healthSlider;
    private Slider easeHealthSlider;

    public float lerpDuration = 1;

    private void Awake()
    {
        healthSlider = healthBar.GetComponent<Slider>();
        easeHealthSlider = easeHealthBar.GetComponent<Slider>();
    }
    public void SetMaxHealth(int health)
    {
        healthSlider.maxValue = health;
        easeHealthSlider.maxValue = health;
        healthSlider.value = health;
        easeHealthSlider.value = health;
    }
    public void SetHealth(int health)
    {
        healthSlider.value = health;
        StartCoroutine(LerpValue(easeHealthSlider.value, health));
        IEnumerator LerpValue(float start, float end)
        {
            float timeElapsed = 0;
            while (timeElapsed < lerpDuration)
            {
                float t = timeElapsed / lerpDuration;
                easeHealthSlider.value = Mathf.Lerp(start, end, t);
                timeElapsed += Time.deltaTime;
                yield return null;
            }
            easeHealthSlider.value = end;
        }
    }
}
