using System.Collections;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    [SerializeField] private int maxHealth = 100;
    [SerializeField] private int health;

    private WaitForSeconds regenTick = new WaitForSeconds(0.1f);
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

    public void HealthDecrease(int amount = 1)
    {
        health -= amount;
        HealthChange();

        if (regen != null)
        {
            StopCoroutine(regen);
        }

        regen = StartCoroutine(RegenHealth());
    }

    private void HealthChange()
    {
        if (health <= 0) Die();
    }

    public void Die()
    {
        //Play death animation here 

        LevelManager.instance.LoadScene("Library");
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
}