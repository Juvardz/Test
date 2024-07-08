using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletController : MonoBehaviour
{
    [SerializeField]
    private float speed;

    private Rigidbody2D _rigidbody;
    private Vector2 _direction;

    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
    }

    private void Start()
    {
        _rigidbody.velocity = _direction * speed * Time.deltaTime;
    }

    void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Enemy"))
        {
            CentinelController controller = other.gameObject.GetComponent<CentinelController>();
            float points = controller.GetPoints();
            LevelManager.Instance.IncreaseScore((int)points);
            Destroy(other.gameObject);
        }
        else if (other.gameObject.CompareTag("Boss"))
        {
            BossController controller = other.gameObject.GetComponent<BossController>();
            controller.TakeDamage(1);
            Destroy(gameObject);
        }
    }


    public void SetDirection(Vector2 direction)
    {
        _direction = direction;
    }
}
