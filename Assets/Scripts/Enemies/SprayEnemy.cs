using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SprayEnemy : Unit
{
    // Start is called before the first frame update
    void Start()
    {
        speed = 3f;
    }

    // Update is called once per frame
    void Update()
    {
        MoveUnit();
    }

    protected override void Fire()
    {
        
    }
}
