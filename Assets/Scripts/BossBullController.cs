using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossBullController : MonoBehaviour
{
      [SerializeField]
    float speed;

    Rigidbody2D _rigidbody;

    Vector2 _direction;

    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
    }
    void Start()
    {
        _rigidbody.velocity = _direction * speed * Time.deltaTime;
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
       
        
            if (other.gameObject.CompareTag("Player"))
        {
            SpaceshipController controller = other.gameObject.GetComponent<SpaceshipController>();
            controller.Die();

            Destroy(gameObject);
        }

            



        
    }
    public void SetDirection(Vector2 direction)
    {
        _direction = direction;
    }
}
