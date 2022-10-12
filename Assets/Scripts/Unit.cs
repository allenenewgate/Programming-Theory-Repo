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

    private float xBound = 19.9f;
    private float zBound = 10.9f;
    private Vector3 destination;

    protected virtual void MoveUnit()
    {
        if (isStopped && !isWaiting)
        {
            isWaiting = true;
            StartCoroutine(PickDestination());
        }
        else if (!isStopped)
        { 
            Vector3 direction = (destination - transform.position).normalized;

            transform.Translate(direction * speed * Time.deltaTime);
            Debug.Log(destination);

            float distance = Vector3.Distance(transform.position, destination);
            if (distance < 1.0f)
            {
                isStopped = true;
            }
        }
    }

    protected IEnumerator PickDestination()
    {
        yield return new WaitForSeconds(3);
        destination =  new Vector3(Random.Range(-xBound, xBound), 1, Random.Range(-zBound, zBound));
        isStopped = false;
        isWaiting = false;
    }
    

    protected virtual void ConstrainUnit()
    {

    }

    protected virtual void Fire()
    {

    }
}
