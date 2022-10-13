using UnityEngine;

public class SprayEnemy : Unit // INHERITANCE
{
    void Start()
    {
        player = GameObject.Find("Player").gameObject;
        fireCoolDown = 0.3f;
        StartCoroutine(FireCoolDown(fireCoolDown));
        speed = 3f;
    }

    void Update()
    {
        MoveUnit();
        if (canFire)
        {
            RotateUnit();
            Fire();
        }
    }

    // POLYMORPHISM
    protected override void Fire()
    {
        Instantiate(bullet, transform.position, transform.rotation);
        
        StartCoroutine(FireCoolDown(fireCoolDown));
    }

    // ABSTRACTION
    private void RotateUnit()
    {
        transform.rotation = Quaternion.Euler(0, Random.Range(-180, 180), 0);
    }
}
