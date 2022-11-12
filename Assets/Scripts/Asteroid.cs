using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Asteroid : MonoBehaviour
{

    public Transform asteroidPrefab;

    public int minFloatSpeed = 40;

    public int maxFloatSpeed = 50;

    private int floatSpeed = 50;

    private bool isChild = false;

    private Rigidbody2D rb;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();

        floatSpeed = Random.Range(40, 50);

        // Hurtle towards center of screen
        Vector2 centerDirection = new Vector2(0 - transform.position.x, 0 - transform.position.y);
        centerDirection.Normalize();
        rb.AddForce(centerDirection * floatSpeed);
        // TODO: Add more variety to all the possible initial directions
    }

    void OnCollisionEnter2D(Collision2D other) 
    {
        if (other.gameObject.tag == "Asteroid")
        {
            return;
        }

        if (!isChild)   
        {
            Vector2 randomDirection;

            Transform firstAsteroid = Instantiate(asteroidPrefab);
            firstAsteroid.rotation = new Quaternion(firstAsteroid.rotation.x, firstAsteroid.rotation.y, Random.Range(0.0f, 360.0f), firstAsteroid.rotation.w);
            firstAsteroid.localScale /= 2.0f;
            randomDirection = new Vector2(Random.Range(-1.0f, 1.0f), Random.Range(-1.0f, 1.0f));
            randomDirection.Normalize();
            firstAsteroid.GetComponent<Rigidbody2D>().AddForce(randomDirection * floatSpeed);
            firstAsteroid.GetComponent<Asteroid>().SetIsChild(true);

            Transform secondAsteroid = Instantiate(asteroidPrefab);
            secondAsteroid.rotation = new Quaternion(secondAsteroid.rotation.x, secondAsteroid.rotation.y, Random.Range(0.0f, 360.0f), secondAsteroid.rotation.w);
            secondAsteroid.localScale /= 2.0f;
            randomDirection = new Vector2(Random.Range(-1.0f, 1.0f), Random.Range(-1.0f, 1.0f));
            randomDirection.Normalize();
            secondAsteroid.GetComponent<Rigidbody2D>().AddForce(randomDirection * floatSpeed);
            secondAsteroid.GetComponent<Asteroid>().SetIsChild(true);

            Physics2D.IgnoreCollision(firstAsteroid.GetComponent<Collider2D>(), secondAsteroid.GetComponent<Collider2D>());
        }

        Destroy(gameObject);
    }

    public void SetIsChild(bool state)
    {
        isChild = state;
    }
}
