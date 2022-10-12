using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

public abstract class Unit : MonoBehaviour
{
    [SerializeField]
    protected float speed;

    protected bool isStopped = true;
    protected bool isWaiting = false;
    protected Vector3 destination;

    protected static float xBound = 19.9f;
    protected static float zBound = 10.9f;

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
