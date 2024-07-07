using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowAndRetreatAI : MonoBehaviour
{
   [SerializeField]
   Transform target;

   [SerializeField]
   float speed;

   [SerializeField]
   float stopDistance;

   [SerializeField]
   float retreatDistance;

   [Header("Fire")]
    [SerializeField]
    Transform firePoint;

    [SerializeField]
    GameObject bulletPrefab;

    [SerializeField]
    float bulletLifeTime;

    [SerializeField]
    float firetimeout;

    float _fireTimer;

   Rigidbody2D _rigidbody;

   private void Awake()
   {
        _rigidbody = GetComponent<Rigidbody2D>();
        _fireTimer = firetimeout;
   }

#region FixedUpdate
    
   private void FixedUpdate()
   {
        float distance = Vector2.Distance(_rigidbody.position, target.position);

        if(distance > stopDistance)
        {
            _rigidbody.position = Vector2.MoveTowards(_rigidbody.position, target.position, speed * Time.fixedDeltaTime);
        }
        else if(distance < retreatDistance)
        {
            _rigidbody.position = Vector2.MoveTowards(_rigidbody.position, target.position, -speed * Time.fixedDeltaTime);
        }
        else if(distance < stopDistance && distance > retreatDistance)
        {
            _rigidbody.position = this._rigidbody.position;
        }

        transform.right = target.position - transform.position;
        //transform.LookAt(target.position);

        //FIRE TO PLAYER => 40 PUNTOS

        HandleFireBoss();

   }

    
#endregion
   

#region FireBoss
    

        private void HandleFireBoss()
    {
        _fireTimer -= Time.deltaTime;

            if(_fireTimer > 0.0F)
            {
                return;
            }

            GameObject bullet = Instantiate(bulletPrefab, firePoint.position, transform.rotation);

            Vector2 direction = (firePoint.position - transform.position).normalized;

            BossBullController controller = bullet.GetComponent<BossBullController>();
            controller.SetDirection(direction);

            Destroy(bullet, bulletLifeTime);
            _fireTimer = firetimeout;

        }

   }

#endregion