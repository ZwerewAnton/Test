using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHealthController : MonoBehaviour
{
    private int health = 100;
    public GameObject spawnZone;
    public GameObject player;
    Text healthUI;
    private int currentHealth;
    public bool isDead = false;
    void Start()
    {
        currentHealth = health;
        spawnZone = GameObject.FindGameObjectWithTag("PlayerSpawnZone");
        healthUI = GameObject.FindGameObjectWithTag("Health").GetComponent<Text>();
        healthUI.text = "Health: " + currentHealth.ToString();
    }
    void Update()
    {
        isDead = false;

        if (currentHealth <= 0)
        {
            isDead = true;
            currentHealth = health;
            healthUI.text = "Health: " + currentHealth.ToString();
        }
    }

    public void Hurt(int damage)
    {
        currentHealth -= damage;
        healthUI.text = "Health: " + currentHealth.ToString();
    }
    public bool IsDead()
    {
        return isDead;
    }
}
