using System.Collections;
using UnityEngine;

public class SniperEnemy : Unit
{
    private bool isStopped = false;
    private bool isWaiting = false;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.Find("Player").gameObject;
        fireCoolDown = 1f;
        destination = new Vector3(Random.Range(-xBound, xBound), 1, Random.Range(-zBound, zBound));
        speed = 5f;
        StartCoroutine(FireCoolDown(fireCoolDown));
    }

    // Update is called once per frame
    void Update()
    {
        MoveUnit();
        if (canFire)
        {
            Fire();
        }
    }

    protected override void Fire()
    {
        float radius = 0.9f;
        Vector3 direction =  player.transform.position - transform.position;
        direction.Normalize();
        Vector3 spawnPos = transform.position + direction * radius;
        spawnPos.y = 1;

        Instantiate(bullet, spawnPos, transform.rotation);
        StartCoroutine(FireCoolDown(fireCoolDown));
    }

    protected override void MoveUnit()  // Moves to random places and always faces player
    {
        // Movement section
        if (isStopped && !isWaiting)
        {
            isWaiting = true;
            StartCoroutine(PickDestination());
        }
        else if (!isStopped)
        {
            Vector3 direction = (destination - transform.position).normalized;

            transform.Translate(direction * speed * Time.deltaTime, Space.World);

            float distance = Vector3.Distance(transform.position, destination);
            if (distance < 1.0f)
            {
                isStopped = true;
            }
        }
        RotateUnit();
    }

    private void RotateUnit()
    { 
        // this is similar to the player AimWeapon() in that it will rotate to always face the player's position
        Vector3 rotationDirection = player.transform.position - transform.position;
        float angle = Mathf.Atan2(rotationDirection.x, rotationDirection.z) * Mathf.Rad2Deg;

        transform.rotation = Quaternion.Euler(0, angle, 0);
    }

    protected IEnumerator PickDestination()  // Waits and then picks a random destination
    {
        yield return new WaitForSeconds(0.5f);
        destination = new Vector3(Random.Range(-xBound, xBound), 1, Random.Range(-zBound, zBound));
        isStopped = false;
        isWaiting = false;
    }
}
