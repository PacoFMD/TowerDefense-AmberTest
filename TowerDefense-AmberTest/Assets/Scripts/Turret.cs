using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Turret : MonoBehaviour
{
    public Transform target;
    public float range = 15, speedRotation = 10, damage = 25;
    public float fireRate = 1, fireCountDown = 0;

    public GameObject bulletPreb;
    public Transform barrel;

    // Start is called before the first frame update
    void Start()
    {
        //on start repeating Find the enemy, which is the target, every second
        InvokeRepeating("FindTarget", 0, 1);
    }

    void FindTarget()
    {
        GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");
        float shortDistance = Mathf.Infinity; //to set the shortest distance for the moment
        GameObject nearestEnemy = null;

        foreach (GameObject enemy in enemies)
        {
            //calculate my position, and the enemy, to set the new shortest distance
            // and validate the enemy distance found
            float distanceToEnemy = Vector3.Distance(transform.position, enemy.transform.position);

            if (distanceToEnemy < shortDistance)
            {
                shortDistance = distanceToEnemy;
                nearestEnemy = enemy;
            }
        }

        if(nearestEnemy != null && shortDistance <= range)
        {
            //setting the objetive reference
            target = nearestEnemy.transform;
        }
        else
        {
            target = null;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (target == null)
            return;

        //to make a smooth rotation
        Vector3 dir = target.position - this.transform.position;
        Quaternion lookAt = Quaternion.LookRotation(dir);
        Vector3 rotation = Quaternion.Lerp(this.transform.rotation, lookAt, Time.deltaTime * speedRotation).eulerAngles;
        this.transform.rotation = Quaternion.Euler(0, rotation.y, 0);

        //shoot rate
        if(fireCountDown <= 0)
        {
            Shoot();
            fireCountDown = 1 / fireRate;
        }
        fireCountDown -= Time.deltaTime;
    }

    void Shoot()
    {
        GameObject bulletGO = Instantiate(bulletPreb, barrel.position, barrel.rotation);
        Bullet bullet = bulletGO.GetComponent<Bullet>();
       
        //hit an objet apply damage
        if(bullet != null)
        {
            bullet.SetDamage(damage);
            bullet.GetTarget(target);
        }
    }
}
