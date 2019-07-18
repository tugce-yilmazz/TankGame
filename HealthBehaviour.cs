using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBehaviour : MonoBehaviour
{
    public Text healthText;
    public Image healthbar;
    float health = 100f;

    public void TakeDamage(float amount)
    {
        health -= amount;
        healthText.text = string.Format("%{0}", health);
        healthbar.fillAmount = health / 100f;
        if (health <= 0)
        {
            Destroy(gameObject);
        }
    }
}
