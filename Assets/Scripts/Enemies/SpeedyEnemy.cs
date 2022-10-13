using UnityEngine;

public class SpeedyEnemy : Unit
{
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.Find("Player").gameObject;
        speed = 6.0f;
        health = 2;
    }

    // Update is called once per frame
    void Update()
    {
        MoveUnit();
    }

    protected override void Fire()
    {
        throw new System.NotImplementedException();
    }
}
