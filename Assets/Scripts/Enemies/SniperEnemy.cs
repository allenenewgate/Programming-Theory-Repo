using System.Collections;
using System.Collections.Generic;
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
}
