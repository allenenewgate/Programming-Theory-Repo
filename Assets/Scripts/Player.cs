using UnityEngine;

public class Player : Unit
{
    [SerializeField]
    private GameManager gameManager;

    // Start is called before the first frame update
    void Start()
    {
        fireCoolDown = 0.15f;
        speed = 10f;
        health = 3;
    }

    // Update is called once per frame
    void Update()
    {
        if (gameManager.isGameActive && !gameManager.isGamePaused)
        {
            MoveUnit();
            AimWeapon(GetMouseTarget());
            if (canFire && Input.GetMouseButton(0))
            {
                Fire();
            }
        }
    }

    protected override void MoveUnit()  // Override to control movement based on input
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
        float radius = 0.9f;
        Vector3 direction = GetMouseTarget() - transform.position;
        direction.Normalize();
        Vector3 spawnPos = transform.position + direction * radius;
        spawnPos.y = 1;

        Instantiate(bullet, spawnPos, transform.rotation);
        StartCoroutine(FireCoolDown(fireCoolDown));
    }

    private Vector3 GetMouseTarget()  // Raycasts a point based on mouse location then uses that point to return a Vector3 with the y coord locked to the play level
    {
        Ray ray = GameObject.Find("Main Camera").GetComponent<Camera>().ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit))
        {
            return new Vector3(hit.point.x, 1, hit.point.z);
        }
        throw new UnityException("Mouse not hitting play area");
    }

    private void AimWeapon(Vector3 target)  // aims weapon at a target given it
    {
        Vector3 rotationDirection = target - transform.position;  // gets direction vector to the target given
        float angle = Mathf.Atan2(rotationDirection.x, rotationDirection.z) * Mathf.Rad2Deg;  // gets the angle from the x plane in degrees

        transform.localRotation = Quaternion.Euler(0, angle, 0);  // transforms that angle into a rotation and makes player face that way
    }

    protected override void HitUnit()  // Overridden to control game state
    {
        base.HitUnit();
        if (health <= 0)
        {
            gameManager.GameOver();
        }
    }

    protected override void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Enemy") || other.CompareTag("EnemyBullet"))
        {
            Destroy(other.gameObject);
            HitUnit();
        }
    }
}
