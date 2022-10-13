using UnityEngine;

public class SpeedyEnemy : Unit // INHERITANCE
{
    void Start()
    {
        player = GameObject.Find("Player").gameObject;
        speed = 6.0f;
        health = 2;
    }

    void Update()
    {
        MoveUnit();
    }

    // POLYMORPHISM
    protected override void Fire()
    {
        return;
    }
}
