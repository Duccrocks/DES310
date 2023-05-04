using System.Collections;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    [SerializeField] private float maxHealth = 100;
    [SerializeField] private float health;

    private WaitForSeconds regenTick = new WaitForSeconds(0.01f);
    private Coroutine regen;

    void Awake()
    {
        health = maxHealth;
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
        if (health <= 0) Die();
    }

    public void Die()
    {
        //Play death animation here 
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
        yield return new WaitForSeconds(3);

        while (health < maxHealth)
        {
            health += maxHealth / 250;
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