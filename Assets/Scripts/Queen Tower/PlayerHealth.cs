using System;
using System.Collections;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    [Header("Health Values")]
    [SerializeField] private float maxHealth = 100;
    [SerializeField] private float health;

    [Header("Audio")]
    [SerializeField] private AudioClip audioClip;
    
    private WaitForSeconds regenTick = new WaitForSeconds(0.1f);
    private Coroutine regen;

    private Animator Animdeth;

    void Awake()
    {
        health = maxHealth;

        Animdeth = GetComponent<Animator>();
    }

    public void HealthIncrease(int amount = 1)
    {
        if ((health += amount) > maxHealth) return;
        health += amount;
    }

    public void HealthDecrease(int amount = 1,int delay = 0)
    {
        health -= amount;
        HealthChange();

        if (regen != null)
        {
            StopCoroutine(regen);
        }

        regen = StartCoroutine(RegenHealth());
    }

    void DamagePlayer(int amount) 
    {
        
    }

    private void HealthChange()
    {
        try
        {
            AudioManager.Instance.PlaySoundOnce(audioClip);
        }
        catch (NullReferenceException)
        {
            Debug.LogError("AudioManager null");
        }
        if (health <= 0) Die();
    }

    public void Die()
    {
        //Play death animation here 

        Animdeth.SetTrigger("Deathdeath");

        try
        {
            LevelManager.instance.LoadScene("Mary QOS");
        }
        catch (System.NullReferenceException)
        {
            Debug.LogError("Level Manager Null");
        }
    }

    private IEnumerator RegenHealth()
    {
        yield return new WaitForSeconds(2);

        while (health < maxHealth)
        {
            health += maxHealth / 25;
            yield return regenTick;
        }
        regen = null;
    }

    public float getHealth()
    {
        return health;
    }

    public float getMaxHealth()
    {
        return maxHealth;
    }


}