using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossController : MonoBehaviour
{
    // 5 PLAYER BULLETS DESTROYS BOSS => 10 PUNTOS
    // (Parecido a los acumuladores de puntos, con una sola bala del boss te destruyen)

    [Header("Stats")]
    [SerializeField]
    private float points;

    private int health = 5; 
    
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            TakeDamage(1);
            Destroy(other.gameObject); 
        }
    }

    public void TakeDamage(int damage)
    {
        health -= damage;
        if (health <= 0)
        {
            UIController.Instance.IncreaseScore(points); 
            Destroy(gameObject); 
        }
    }

    public float GetPoints()
    {
        return points;
    }
}
