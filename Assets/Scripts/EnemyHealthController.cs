using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyHealthController : MonoBehaviour
{
    private int health = 40;
    public Text killsUI;
    public int kills = 0;
    public SpawnerController s_controller;
    private int currentHealth;

    void Start()
    {
        s_controller = GameObject.FindGameObjectsWithTag("Respawn")[0].GetComponent<SpawnerController>();
        killsUI = GameObject.FindGameObjectWithTag("Kills").GetComponent<Text>();
        currentHealth = health;
    }
    void Update()
    {
        if(currentHealth <= 0)
        {
            kills++;
            killsUI.text = "Kills: " + kills.ToString();
            Destroy(gameObject);
            s_controller.SpawnEnemy();
        }
    }

    public void Hurt(int damage)
    {
        currentHealth -= damage;
    }
}
