using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class SniperEnemy : Unit
{
    // Start is called before the first frame update
    void Start()
    {
        speed = 5f;
    }

    // Update is called once per frame
    void Update()
    {
        MoveUnit();
    }

    protected override void MoveUnit()  // Moves to random places
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

            float distance = Vector3.Distance(transform.position, destination);
            if (distance < 1.0f)
            {
                isStopped = true;
            }
        }
    }
    protected IEnumerator PickDestination()  // Waits and then picks a random destination
    {
        yield return new WaitForSeconds(1);
        destination = new Vector3(Random.Range(-xBound, xBound), 1, Random.Range(-zBound, zBound));
        isStopped = false;
        isWaiting = false;
    }
}
