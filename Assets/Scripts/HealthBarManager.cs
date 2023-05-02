using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBarManager : MonoBehaviour
{
    [SerializeField] Image bar;
    [SerializeField] PlayerHealth health;
  
    // Start is called before the first frame update
    // Update is called once per frame
    void Update()
    {
        bar.fillAmount = health.getHealth() / health.getMaxHealth();
    }
}
