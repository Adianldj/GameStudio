using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Base : MonoBehaviour
{
    private float _baseHealth = 100;
    private float _currentHealth;
    public Image healthBar;
    public int _enemiesDefeated;

    void Start()
    {
        _currentHealth = _baseHealth;
        _enemiesDefeated = 0;
    }

    void Damage(float amount)
    {
        _currentHealth -= amount;
        healthBar.fillAmount = _currentHealth / _baseHealth;

        if (_currentHealth <= 0)
        {
            FindObjectOfType<MenuManager>().PlayerLoses();
        }
    }

    void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.GetComponent<Enemy>())
        {
            Destroy(other.gameObject);
            Damage(10);

        }
    }
}
