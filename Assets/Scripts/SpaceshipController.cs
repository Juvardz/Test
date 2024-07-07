using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;
using static UnityEngine.GridBrushBase;


public class SpaceshipController : MonoBehaviour
{
    [Header("Movement")]
    [SerializeField]
    float speed = 10.0F;

    [SerializeField]
    Vector2 edges;

    [SerializeField]
    bool handleClamp;

    [Header("Rotation")]
    [SerializeField]
    bool mouseRotation;

    [SerializeField]
    float rotationSpeed = 5000;

    [SerializeField]
    float rotationTime = 0.05F;

    [Header("Fire")]
    [SerializeField]
    Transform firePoint;

    [SerializeField]
    GameObject bulletPrefab;

    [SerializeField]
    float bulletLifeTime;

    [SerializeField]
    float firetimeout;

    [Header("Animations")]
    [SerializeField]
    float dieTime;

    [SerializeField]
    float spawnWaitTime;

     [SerializeField]
    string fireSoundSFX;

    Vector2 _move = Vector2.zero;
    Vector2 _mouseScreenPoint;

    Rigidbody2D _rigidbody;

    float _rotationDirection;
    float _fireTimer;

    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        HandleInputMove();
        HandleInputRotation();
        HandleFire();
    }

    private void FixedUpdate()
    {
        HandleRotation();

        if (_move.sqrMagnitude == 0.0F)
            return;
        HandleMove();
        HandleClamp();
        HandleTeleport();
    }

    #region Fire
    private void HandleFire()
    {
        _fireTimer -= Time.deltaTime;
        if (Input.GetButton("Fire1"))
        {
            if(_fireTimer > 0)
            {
                return;
            }

            GameObject bullet = Instantiate(bulletPrefab, firePoint.position, transform.rotation);

            Vector2 direction = (firePoint.position - transform.position).normalized;

            BulletController controller = bullet.GetComponent<BulletController>();
            controller.SetDirection(direction);

            Destroy(bullet, bulletLifeTime);
            _fireTimer = firetimeout;

            SoundManager.Instance.PlaySFX(fireSoundSFX);
        }
    }

    #endregion

    #region Rotation
    private void HandleInputRotation()
    {
        if (Input.GetKey(KeyCode.E))
            _rotationDirection = -1.0F;
        else if (Input.GetKey(KeyCode.Q))
            _rotationDirection = +1.0F;
        else
            _rotationDirection = 0.0F;

        _mouseScreenPoint = Input.mousePosition;
    }

    private void HandleRotation()
    {
        if(mouseRotation)
        {
            Vector2 currentScreenPoint = Camera.main.WorldToScreenPoint(_rigidbody.position);
            Vector2 direction = (_mouseScreenPoint - currentScreenPoint).normalized;
            float angleZ = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
            _rigidbody.MoveRotation(angleZ);
            return;
        }
        else
        {
            float currentRotation = _rigidbody.rotation;
            if (_rotationDirection != 0.0F)
            {
                float targetRotation = currentRotation + _rotationDirection * rotationSpeed * Time.deltaTime;
                float rotation = Mathf.Lerp(currentRotation, targetRotation, rotationTime);
                _rigidbody.rotation = rotation;
            }
        }
    }

    #endregion

    #region Campling or Teleport
    private void HandleTeleport()
    {
        if (handleClamp)
            return;

        Vector2 currentPosition = _rigidbody.position;
        if (currentPosition.x > 0.0F && currentPosition.x >= edges.x)
        {
            _rigidbody.position = new Vector2(-edges.x + 0.01F, currentPosition.y);
        }
        else if (currentPosition.x < 0.0F && currentPosition.x <= -edges.x)
        {
            _rigidbody.position = new Vector2(edges.x - 0.01F, currentPosition.y);
        }

        if (currentPosition.y > 0.0F && currentPosition.y >= edges.y)
        {
            _rigidbody.position = new Vector2(currentPosition.x, - edges.y + 0.01F);
        }
        else if (currentPosition.y < 0.0F && currentPosition.y <= -edges.y)
        {
            _rigidbody.position = new Vector2(currentPosition.x, edges.y - 0.01F);
        }
    }

    private void HandleClamp()
    {
        if(!handleClamp)
            return;

        float x = Mathf.Clamp(_rigidbody.position.x, -edges.x, edges.x);
        float y = Mathf.Clamp(_rigidbody.position.y, -edges.y, edges.y);

        _rigidbody.position = new Vector2(x, y);
    }

    #endregion

    #region Movement
    private void HandleMove()
    {
        Vector2 direction = _move.normalized;
        Vector2 currentPosition = _rigidbody.position;
        _rigidbody.MovePosition(currentPosition + direction * speed * Time.fixedDeltaTime);
    }

    private void HandleInputMove()
    {
        float x = Input.GetAxisRaw("Horizontal");
        float y = Input.GetAxisRaw("Vertical");

        _move = new Vector2(x, y);
    }
    #endregion

    #region Die

    public void Die()
    {
     Collider2D collider = GetComponent<Collider2D>();
     collider.enabled = false;

     SpaceshipController controller = GetComponent<SpaceshipController>();
     controller.enabled = false;

     StartCoroutine(DieCoroutine());
    }

    private IEnumerator DieCoroutine()
    {
        SpriteRenderer render = GetComponentInChildren<SpriteRenderer>();
        Color color = render.color;
        while(color.a > 0.0F)
        {
            color.a -= 0.1F;
            render.color = color;
            yield return new WaitForSeconds(dieTime);
        }

        yield return new WaitForSeconds(spawnWaitTime);

        //MOVE TO LEVELMANAGER AS RELOAD => 10 PUNTOS

        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);

        UIController.Instance.DecreaseLives();
        //GO TO GAMEOVER WHEN HasLives IS FALSE => 10 PUNTOS
    }

    #endregion
}
