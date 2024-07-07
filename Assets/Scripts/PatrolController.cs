using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PatrolController : MonoBehaviour
{
    [SerializeField]
    float speed;

    [SerializeField]
    float lifeTime;

    [SerializeField]
    int maxPathPoints;


    Transform _nextPoint;
    Rigidbody2D _rb;
    Transform[] _pathPoints;

    float _aliveTime;
    int _maxPathPoints = 0;

    private void Awake()
    {
        _rb = GetComponent<Rigidbody2D>();
    }

    private void FixedUpdate()
    {
        Vector2 currentPos = _rb.position;
        Vector2 targetPos = _nextPoint.position;
        Vector2 movePos = Vector2.MoveTowards(currentPos, targetPos, speed * Time.deltaTime);

        _rb.MovePosition(movePos);
        if (Vector2.Distance(_rb.position, targetPos) < 0.2F)
        {
            if (_aliveTime <= 0.0F)
            {

                Next();
                _aliveTime = lifeTime;

                _maxPathPoints--;
            }
            else
                _aliveTime -= Time.deltaTime;
        }

    }

    private void Start()
    {
        _rb.position = _pathPoints[0].position;
        _maxPathPoints = Random.Range(maxPathPoints / 2, maxPathPoints);
        Next();
    }

    private void Next()
    {
        if (_maxPathPoints <= 0)
        {
            Destroy(gameObject);
            return;
        }

        int pointNumber = Random.Range(1, _pathPoints.Length);

        foreach (Transform pathPoint in _pathPoints) 
        {
            if(pathPoint.name == "Point "+pointNumber.ToString())
            {
                _nextPoint = pathPoint;
                return;
            }
        }
    }

    public void SetPathPoints(Transform[] pathPoints)
    {
        _pathPoints = pathPoints;
    }
}