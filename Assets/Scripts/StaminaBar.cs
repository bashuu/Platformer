using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StaminaBar : MonoBehaviour
{
    public Slider slider;

    public void setStamina(float stamina)
    {
        slider.value = stamina;
    }

    public void setMaxStamina(float stamina)
    {
        slider.maxValue = stamina;
    }
}
