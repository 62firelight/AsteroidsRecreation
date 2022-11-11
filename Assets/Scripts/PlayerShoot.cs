using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerShoot : MonoBehaviour
{

    public Transform firePoint;

    public KeyCode shootKey = KeyCode.Space;

    public Transform projectilePrefab;

    public float projectileSpeed = 10.0f;

    private Collider2D collider;

    // Start is called before the first frame update
    void Start()
    {
        collider = GetComponent<Collider2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(shootKey))
        {
            // Create projectile, set its position and rotation
            Transform projectile = Instantiate(projectilePrefab);
            projectile.position = firePoint.position;
            projectile.rotation = firePoint.rotation;

            // Ignore collisions between player and projectile
            Collider2D projectileCollider = projectile.gameObject.GetComponent<Collider2D>();
            if (projectileCollider != null && collider != null) 
            {
                Physics2D.IgnoreCollision(projectileCollider, collider);
            }
            
            // Add force to the projectile
            Rigidbody2D projectileRb = projectile.gameObject.GetComponent<Rigidbody2D>();
            projectileRb.AddForce(transform.up * projectileSpeed);
        }
    }
}
