using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    public Slider healthSlider;

    public void SetSlider(float healthAmount)
    {
        healthSlider.value = healthAmount;
    }

    public void SetSliderMax(float healthAmount)
    {
        healthSlider.maxValue = healthAmount;
        SetSlider(healthAmount);
    }
}
