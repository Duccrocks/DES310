using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(PlayerMovement))]
public class SprintController : MonoBehaviour
{
    //How long stamina will take to tick
    private const float regenTick = 0.01f;

    [Header("Stamina Settings")]
    [SerializeField] [Range(10, 500)] private float maxStamina = 100f;

    [SerializeField] private float staminaDrain = 1f;
    [SerializeField] private float staminaRegenDelay = 1.5f;

    [Header("HUD")]
    [SerializeField] private Slider staminaBar;

    private Coroutine regen;


    //The players current stamina
    public float CurrentStamina { get; private set; }

    private void Start()
    {
        //Initalises variables.
        CurrentStamina = maxStamina;
        try
        {
            staminaBar.maxValue = maxStamina;
            staminaBar.value = maxStamina;
        }
        catch (NullReferenceException)
        {
            Debug.LogError("Stamina bar slider null.");
        }
    }

    /// <summary>
    ///     Responsible for actively changing the stamina.
    /// </summary>
    public void UseStamina()
    {
        //Turns on the UI in the case it's turned off.
        staminaBar.gameObject.SetActive(true);

        //Lowers the stamina.
        if (CurrentStamina - staminaDrain >= 0f)
        {
            //Decreases the stamina over time.
            CurrentStamina -= staminaDrain * Time.deltaTime * 50;
            //Changes the stamina bar
            staminaBar.value = CurrentStamina;
            if (regen != null) StopCoroutine(regen);
            //Starts regenerating.
            regen = StartCoroutine(RegenStamina());
        }
    }

    private IEnumerator RegenStamina()
    {
        //Waits 2 seconds before regenerating stamina.
        yield return new WaitForSeconds(staminaRegenDelay);

        //While their is still stamina left for the user to deplete
        while (CurrentStamina < maxStamina)
        {
            //Regenerates the stamina at a 25th of the max stamina every regenTick.
            CurrentStamina += maxStamina * Time.deltaTime;
            //Changes the stamina bar
            staminaBar.value = CurrentStamina;
            //How long it takes to loop
            yield return new WaitForSeconds(regenTick);
        }

        //Don't regen anymore. 
        regen = null;

        //After 1 second of not sprinting disables the GUI for the Sprinting
        Invoke(nameof(DisableBar), 1);
    }

    /// <summary>
    ///     Disables the sprinting slider.
    /// </summary>
    public void DisableBar()
    {
        staminaBar.gameObject.SetActive(false);
    }
}