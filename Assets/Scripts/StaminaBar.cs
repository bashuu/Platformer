using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StaminaBar : MonoBehaviour
{
    public Slider slider;
    public PlayerData playerData;
    public Image fill;
    private void Start()
    {
        playerData.stamina = playerData.maxStamina;
        setMaxStamina(playerData.maxStamina);
    }

    private void Update()
    {
        setStamina(playerData.stamina);
        setColor(playerData.stamina);
    }

    public void setStamina(float stamina)
    {
        slider.value = stamina;
        

    }

    public void setMaxStamina(float stamina)
    {
        slider.maxValue = stamina;
    }

    public void setColor(float stamina)
    {
        int blue = calculateBlue();
        fill.GetComponent<Image>().color = new Color32(255, (byte)blue, 0, 255);

    }

    private int calculateBlue()
    {
        int tmp = Mathf.RoundToInt(playerData.stamina * 100 / playerData.maxStamina);

        return 225 * tmp / 100;
    }
}
