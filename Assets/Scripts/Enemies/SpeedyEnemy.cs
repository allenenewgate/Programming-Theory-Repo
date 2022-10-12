using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpeedyEnemy : Unit
{
    // Start is called before the first frame update
    void Start()
    {
        speed = 6.0f;
    }

    // Update is called once per frame
    void Update()
    {
        MoveUnit();
    }
}
