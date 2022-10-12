using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : Unit
{
    private float fireCoolDown = 0.15f;

    // Start is called before the first frame update
    void Start()
    {
        speed = 10f;
        health = 3;
    }

    // Update is called once per frame
    void Update()
    {
        MoveUnit();
        AimWeapon(GetMouseTarget());
        if (canFire && Input.GetMouseButton(0))
        {
            Fire();
        }
    }

    protected override void MoveUnit()
    {
        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");

        transform.Translate(Vector3.right * horizontalInput * speed * Time.deltaTime, Space.World);
        transform.Translate(Vector3.forward * verticalInput * speed * Time.deltaTime, Space.World);
        transform.position = new Vector3(Mathf.Clamp(transform.position.x, -xBound, xBound), transform.position.y, 
            Mathf.Clamp(transform.position.z, -zBound, zBound));   // Constrains the player to the play arena through clamping (x,z) coords
    }

    protected override void Fire()
    {
        float radius = 0.5f;
        Vector3 direction = GetMouseTarget() - transform.position;
        direction.Normalize();
        Vector3 spawnPos = transform.position + direction * radius;
        spawnPos.y = 1;

        Instantiate(bullet, spawnPos, transform.rotation);
        Debug.Log(direction);
        StartCoroutine(FireCoolDown(fireCoolDown));
    }

    private Vector3 GetMouseTarget()
    {
        Ray ray = GameObject.Find("Main Camera").GetComponent<Camera>().ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit))
        {
            return new Vector3(hit.point.x, 1, hit.point.z);
        }
        throw new UnityException("Mouse not hitting play area");
    }

    private void AimWeapon(Vector3 target)
    {
        float angle;
        Vector3 rotationDirection = target - transform.position;

        angle = Mathf.Atan2(rotationDirection.x, rotationDirection.z) * Mathf.Rad2Deg;
        transform.localRotation = Quaternion.Euler(0, angle, 0);
    }
}
