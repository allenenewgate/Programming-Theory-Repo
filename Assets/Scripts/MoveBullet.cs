using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveBullet : MonoBehaviour
{
    [SerializeField]
    private float speed;
    private const float xBound = 25f;
    private const float zBound = 13f;
    Vector3 destination;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector3.forward * speed * Time.deltaTime);

        // Destroy object if it goes too far
        if (transform.position.x >= xBound || transform.position.x <= -xBound || transform.position.z >= zBound || transform.position.z <= -zBound)
        {
            Destroy(gameObject);
        }
    }
}
