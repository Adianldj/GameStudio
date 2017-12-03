using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Base : MonoBehaviour
{
    private float _baseHealth = 100;
    public float _currentHealth;
    public Image healthBar;

    void Start()
    {
        _currentHealth = _baseHealth;
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
            Damage(10);
            Destroy(other.gameObject);
            FindObjectOfType<EnemyManager>()._enemiesLeft--;
            if (FindObjectOfType<EnemyManager>()._enemiesLeft <= 0)
            {
                MenuManager.instance.PlayerWins();
            }
        }
    }
}
