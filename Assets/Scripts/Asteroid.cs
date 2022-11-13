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

        // Ignore collisions for asteroid and boundary layer
        Physics2D.IgnoreLayerCollision(3, 3, true);
        Physics2D.IgnoreLayerCollision(3, 6, true);

        floatSpeed = Random.Range(40, 50);

        // Get coordinates for bottom-left point and top-right point of screen
        Vector2 bottomLeftPoint = Camera.main.ScreenToWorldPoint(Vector3.zero);
        Vector2 topRightPoint = Camera.main.ScreenToWorldPoint(new Vector3(Camera.main.pixelWidth, Camera.main.pixelHeight, 0));

        // Get a random point on the diagonal line between the two points above
        Vector2 randomPointOnDiagonal = new Vector2(Random.Range(bottomLeftPoint.x, topRightPoint.x), Random.Range(bottomLeftPoint.y, topRightPoint.y)); 

        // Get a random direction vector pointing towards the random point, then normalize it
        Vector2 randomDirection = new Vector2(randomPointOnDiagonal.x - transform.position.x, randomPointOnDiagonal.y - transform.position.y);
        randomDirection.Normalize();
        
        // Hurtle towards the random point we have selected
        rb.AddForce(randomDirection * floatSpeed);
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
        }

        Destroy(gameObject);
    }

    public void SetIsChild(bool state)
    {
        isChild = state;
    }
}
