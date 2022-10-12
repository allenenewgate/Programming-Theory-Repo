using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

public abstract class Unit : MonoBehaviour
{
    [SerializeField]
    protected float speed;

    protected Vector3 destination;
    protected float fireCoolDown = 0.2f;

    protected const float xBound = 19.9f;
    protected const float zBound = 10.9f;
    

    protected virtual void MoveUnit()  // Base movement is to chase the player
    {
        Vector3 target = GameObject.Find("Player").transform.position;
        Vector3 direction = (target - transform.position).normalized;

        transform.Translate(direction * speed * Time.deltaTime);
    }

    protected virtual void Fire()
    {

    }
}
