using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : Unit
{
    // Start is called before the first frame update
    void Start()
    {
        speed = 10f;
    }

    // Update is called once per frame
    void Update()
    {
        MoveUnit();
    }

    protected override void MoveUnit()
    {
        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");

        transform.Translate(Vector3.right * horizontalInput * speed * Time.deltaTime);
        transform.Translate(Vector3.forward * verticalInput * speed * Time.deltaTime);
        transform.position = new Vector3(Mathf.Clamp(transform.position.x, -xBound, xBound), transform.position.y, 
            Mathf.Clamp(transform.position.z, -zBound, zBound));   // Constrains the player to the play arena through clamping (x,z) coords
    }    
}
