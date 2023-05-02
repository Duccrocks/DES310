using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBarManager : MonoBehaviour
{
    [SerializeField] Image[] hearts;
    [SerializeField] PlayerHealth health;
    public int segs;
    // Start is called before the first frame update
    // Update is called once per frame
    void Update()
    {
        int current = (int)health.getHealth() / 100;
        for (int i=0;i<hearts.Length;++i)
        {
            
            if (current>i)
            {
                hearts[i].fillAmount = 1;
            }
            else if(current<i)
            {
                hearts[i].fillAmount = 0;
            }
            else if (i==current)
            {
                int quad = (int)(health.getHealth()/(100/segs)) % segs;
                hearts[i].fillAmount = quad * (1.0f/segs);
            }
        }
    }
}
