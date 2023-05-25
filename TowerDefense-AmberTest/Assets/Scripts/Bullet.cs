using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float speed = 70;
    float damage;

    Transform target;

    public GameObject particleEffectImpact;

    public void GetTarget(Transform _target)// get the hitted object as reference
    {
        target = _target;
    }
    
    void Update()
    {
        if(target == null)
        {
            Destroy(this.gameObject);
        }

        // validate the target is really close to the bullet
        // related to the direction and distance
        Vector3 dir = target.position - this.transform.position;
        float distance = speed * Time.deltaTime;

        if(dir.magnitude <= distance)
        {
            HitEnemy();
        }
        transform.Translate(dir.normalized * distance, Space.World);
    }

    // on hit, apply damage & particle effect
    public void HitEnemy()
    {
        GameObject particle = Instantiate(particleEffectImpact, transform.position, transform.rotation);
        Destroy(particle, 2f);
        target.transform.GetComponent<EnemyStatus>().TakeDamage(damage);
        Destroy(this.gameObject);
    }

    public void SetDamage(float _damage)
    {
        damage = _damage;
    }
}