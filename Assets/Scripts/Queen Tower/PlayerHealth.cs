using System;
using System.Collections;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    [Header("Health")]
    [SerializeField] private float maxHealth = 100;
    [SerializeField] private float health;
    [SerializeField] private float regenCooldown = 3f;

    [Header("Audio")] 
    [SerializeField] private AudioClip hurtClip;

    private Animator deathAnimator;
    private bool hasDied;
    private Coroutine regen;
    private readonly WaitForSeconds regenTick = new(0.01f);

    private void Awake()
    {
        health = maxHealth;

        deathAnimator = GetComponent<Animator>();
        
    }

    public void HealthIncrease(int amount = 1)
    {
        if ((health += amount) > maxHealth) return;
        health += amount;
    }

    public void HealthDecrease(int amount = 1, int delay = 0)
    {
        health -= amount;
        HealthChange();

        if (regen != null) StopCoroutine(regen);

        regen = StartCoroutine(RegenHealth());
    }

    private void HealthChange()
    {
        if (health <= 0 && !hasDied)
        {
            Die();
            hasDied = true;
            return;
        }

        try
        {
            AudioManager.Instance.PlaySoundOnce(hurtClip);
        }
        catch (NullReferenceException)
        {
            Debug.LogError("AudioManager Null");
        }
    }

    public void Die()
    {
        //Play death animation here 
        try
        {
            deathAnimator.SetTrigger("Deathdeath");
        }
        catch (NullReferenceException)
        {
            Debug.LogError("Player animator null when performing death animation.");
        }


        try
        {
            LevelManager.instance.LoadScene("Mary QOS");
        }
        catch (NullReferenceException)
        {
            Debug.LogError("Level Manager Null");
        }
    }

    private IEnumerator RegenHealth()
    {
        yield return new WaitForSeconds(regenCooldown);

        while (health < maxHealth)
        {
            health += maxHealth / 250;
            yield return regenTick;
        }

        regen = null;
    }

    public float GetHealth()
    {
        return health;
    }

    public float GetMaxHealth()
    {
        return maxHealth;
    }
}