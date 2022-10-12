using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SprayEnemy : Unit
{
    // Start is called before the first frame update
    void Start()
    {
        fireCoolDown = 0.2f;
        speed = 3f;
    }

    // Update is called once per frame
    void Update()
    {
        MoveUnit();
        if (canFire)
        {
            RotateUnit();
            Fire();
        }
    }

    protected override void Fire()
    {
        Instantiate(bullet, transform.position, transform.rotation);
        
        StartCoroutine(FireCoolDown(fireCoolDown));
    }

    private void RotateUnit()
    {
        transform.rotation = Quaternion.Euler(0, Random.Range(-180, 180), 0);
    }
}
