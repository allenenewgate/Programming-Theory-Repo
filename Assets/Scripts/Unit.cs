using System.Collections;
using UnityEngine;

public abstract class Unit : MonoBehaviour
{
    [SerializeField]
    protected float speed;
    [SerializeField]
    protected GameObject bullet;
    
    protected GameObject player;
    protected bool canFire = true;
    protected float fireCoolDown = 1f;
    protected Vector3 fireAngle;
    protected Vector3 fireDirection;
    protected Vector3 destination;
    protected int health = 1;

    protected const float xBound = 19.9f;
    protected const float zBound = 10.9f;

    private void Start()
    {
        player = GameObject.Find("Player").gameObject;
    }

    protected virtual void MoveUnit()  // Base movement is to chase the player
    {
        Vector3 target = player.transform.position;
        Vector3 direction = (target - transform.position).normalized;

        transform.Translate(direction * speed * Time.deltaTime, Space.World);
    }

    protected abstract void Fire();  // multiple units will fire, but each one is different

    protected IEnumerator FireCoolDown(float coolDown)  // units that fire will all have a cool down that can call this
    {
        canFire = false;
        yield return new WaitForSeconds(coolDown);
        canFire = true;
    }

    protected virtual void HitUnit()  // decrease health of target then destroy if health hits 0, Player overrides to handle the game state
    {
        health--;
        if (health <= 0 && !CompareTag("Player"))
        {
            Destroy(gameObject);
        }
    }

    private void OnCollisionEnter(Collision collision) // Handles collision with the player to destroy the enemy and hurt the player
    {
        if (CompareTag("Player") && collision.gameObject.CompareTag("Enemy"))
        {
            Destroy(collision.gameObject);
            HitUnit();
        }
    }

    protected virtual void OnTriggerEnter(Collider other)  // when a bullet hits anything but the player it will hurt the enemy
    {
        if (other.CompareTag("Bullet") && !CompareTag("Player"))
        {
            Destroy(other.gameObject);
            HitUnit();
        }
    }
}
