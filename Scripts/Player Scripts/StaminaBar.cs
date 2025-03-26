using UnityEngine;
using UnityEngine.UI;

public class StaminaBar : MonoBehaviour
{
    public Slider staminaSlider;
    public void SetHealth(float stamina)
    {
        staminaSlider.value = stamina;
    }
}
