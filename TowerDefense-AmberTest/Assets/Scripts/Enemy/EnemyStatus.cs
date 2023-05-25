using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class EnemyStatus : MonoBehaviour
{
    public float health, speed, damage;

    public int currentWayPoint = 0;

    Transform targetWayPoint;

    Gamemanager gamemanager;
    GameStatus gameStatus;


    public void Start()
    {
       gamemanager = GameObject.Find("Gamemanager").GetComponent<Gamemanager>();
       gameStatus = GameObject.Find("Gamemanager").GetComponent<GameStatus>();

    }

    private void Update()
    {
        // check if we have somewhere to walk
        if (currentWayPoint < gamemanager.wayPointList.Length)
        {
            if (targetWayPoint == null)
                targetWayPoint = gamemanager.wayPointList[currentWayPoint];
            Move();
        }
        else
        {
            MakeDamage();
        }
    }

    // default values for an invoke
    public void OnObjectSpawn()
    {
        health = 100;
        speed = 5;
    }

    public float TakeDamage(float _Amount)
    {
        health -= _Amount;
        if (health <= 0)
        {
            gamemanager.EnemiesLeft();
            this.gameObject.SetActive(false);
        }
        return health;
    }

    void Move()
    {
        // rotate towards the target
        transform.forward = Vector3.RotateTowards(transform.forward, targetWayPoint.position - transform.position, speed * Time.deltaTime, 0.0f);

        // move towards the target
        transform.position = Vector3.MoveTowards(transform.position, targetWayPoint.position, speed * Time.deltaTime);

        if (transform.position == targetWayPoint.position)
        {
            currentWayPoint++;
            targetWayPoint = gamemanager.wayPointList[currentWayPoint-1];
        }
    }

    void MakeDamage()
    {
        gameStatus.TakeDamage(damage);
        gamemanager.EnemiesLeft();
        Destroy(this.gameObject);

    }


    public void SetSpeed(float _Amount)
    {
        speed = _Amount;
    }

    public float GetSpeed()
    {
        return speed;
    }
}

