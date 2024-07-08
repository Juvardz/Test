using System.Collections;
using System.Collections.Generic;
using JetBrains.Annotations;
using UnityEngine;

public class CentinelController : MonoBehaviour
{
    [SerializeField]
    float points;
    
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            SpaceshipController controller = collision.gameObject.GetComponent<SpaceshipController>();
            controller.Die();

            Destroy(gameObject);
        }
    }

    public float GetPoints()
        {
            return points;
        }
}
