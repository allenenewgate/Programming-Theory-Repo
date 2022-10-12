using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

public abstract class Unit : MonoBehaviour
{
    [SerializeField]
    protected float speed;
    [SerializeField]
    protected GameObject bullet;

    protected bool canFire = true;
    protected Vector3 fireAngle;
    protected Vector3 fireDirection;
    protected Vector3 destination;
    protected int health = 1;

    protected const float xBound = 19.9f;
    protected const float zBound = 10.9f;
    

    protected virtual void MoveUnit()  // Base movement is to chase the player
    {
        Vector3 target = GameObject.Find("Player").transform.position;
        Vector3 direction = (target - transform.position).normalized;

        transform.Translate(direction * speed * Time.deltaTime);
    }

    protected abstract void Fire();

    protected virtual IEnumerator FireCoolDown(float coolDown)
    {
        canFire = false;
        yield return new WaitForSeconds(coolDown);
        canFire = true;
    }
}
