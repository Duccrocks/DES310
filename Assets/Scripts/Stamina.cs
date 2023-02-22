using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class StaminaBar : MonoBehaviour
{
    //References to UI.
    public Slider staminaBar;

    [SerializeField] private GameObject background;

    [SerializeField] private GameObject fillArea;

    [SerializeField] private GameObject image;
    
    [SerializeField][Range(500,2000)] float maxStamina = 1000f;

    //The players current stamina
    [NonSerialized] public float currentStamina;

    //How long stamina will take to tick
    private const float regenTick = 0.1f;
    private Coroutine regen;

    private void Start()
    {
        //Initalizes variables.
        currentStamina = maxStamina;
        staminaBar.maxValue = maxStamina;
        staminaBar.value = maxStamina;
    }

    /// <summary>
    ///     Responsible for actively changing the stamina.
    /// </summary>
    /// <param name="amount">The reduction in stamina coming in as a parameter.</param>
    public void UseStamina(float amount)
    {
        //Turns on the UI in the case it's turned off.
        background.SetActive(true);
        fillArea.SetActive(true);
        image.SetActive(true);
        //Lowers the stamina.
        if (currentStamina - amount >= 0f)
        {
            //Decreases the stamina over time.
            currentStamina -= amount;
            //Changes the stamina bar
            staminaBar.value = currentStamina;
            if (regen != null) StopCoroutine(regen);
            //Starts regenerating.
            regen = StartCoroutine(RegenStamina());
        }
    }

    private IEnumerator RegenStamina()
    {
        //Waits 2 seconds to do the below code
        yield return new WaitForSeconds(2);

        //While their is still stamina left for the user to deplete
        while (currentStamina < maxStamina)
        {
            //Regens the stamina at a 25th of the max stamina every regenTick.
            currentStamina += maxStamina / 25;
            //Changes the stamina bar
            staminaBar.value = currentStamina;
            //How long it takes to loop
            yield return new WaitForSeconds(regenTick);
        }

        //Regen is none 
        regen = null;

        //After 1 second of not sprinting disables the GUI for the Sprinting
        Invoke(nameof(DisableBar), 1);
    }

    /// <summary>
    ///     Disables the sprinting slider.
    /// </summary>
    public void DisableBar()
    {
        background.SetActive(false);
        fillArea.SetActive(false);
        image.SetActive(false);
    }
}