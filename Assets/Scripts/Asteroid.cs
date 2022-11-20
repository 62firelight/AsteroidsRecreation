using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Asteroid : MonoBehaviour
{

    public GameObject[] asteroidPrefabs;

    public Transform asteroidPrefab;

    public Transform explosionPrefab;

    public int minFloatSpeed = 40;

    public int maxFloatSpeed = 50;

    private int floatSpeed = 50;

    private int timesSplit = 0;

    private Rigidbody2D rb;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();

        // Attempt to get list of asteroids from level master object in the scene
        GameObject levelMasterObj = GameObject.FindWithTag("LevelMaster");
        if (levelMasterObj != null)
        {
            LevelMaster levelMaster = levelMasterObj.GetComponent<LevelMaster>();

            if (levelMaster != null)
            {
                asteroidPrefabs = levelMaster.asteroidPrefabs;
            }
        }
        else
        {
            asteroidPrefabs = new GameObject[] { asteroidPrefab.gameObject };
        }

        // Ignore collisions for asteroid and boundary layer
        Physics2D.IgnoreLayerCollision(3, 3, true);
        Physics2D.IgnoreLayerCollision(3, 6, true);

        // Assign different speeds based on asteroid size
        // (Smaller asteroids are faster and vice versa)
        switch (timesSplit)
        {
            case 0:
                floatSpeed = Random.Range(50, 60);
                break;
            case 1:
                floatSpeed = Random.Range(60, 80);
                break;
            case 2:
                floatSpeed = Random.Range(70, 90);
                break;
            default:
                break;
        }

        transform.Rotate(0.0f, 0.0f, Random.Range(0.1f, 360.0f));

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

        if (timesSplit <= 2)
        {
            IncreaseScore();

            if (timesSplit < 2)
            {
                Split();
            }
        }

        Destroy(gameObject);
        GameMaster.asteroidsLeft--;

        // Spawn explosions
        for (int i = 0; i < 5; i++)
        {
            Transform explosion = Instantiate(explosionPrefab);
            explosion.position = transform.position;
        }
        
    }

    void IncreaseScore()
    {
        // Calculate score to add based on asteroid size
        // Smaller asteroids give higher score
        switch (timesSplit)
        {
            case 0:
                GameMaster.IncreaseScore(0);
                break;
            case 1:
                GameMaster.IncreaseScore(1);
                break;
            case 2:
                GameMaster.IncreaseScore(2);
                break;
            default:
                break;
        }
    }

    void Split()
    {
        // Select random asteroid prefab
        int randomFirstAsteroidIndex = Random.Range(0, asteroidPrefabs.Length);
        GameObject randomFirstAsteroid = asteroidPrefabs[randomFirstAsteroidIndex];

        // Select random asteroid prefab
        int randomSecondAsteroidIndex = Random.Range(0, asteroidPrefabs.Length);
        GameObject randomSecondAsteroid = asteroidPrefabs[randomSecondAsteroidIndex];

        // Split into two asteroids
        Transform firstAsteroid = Instantiate(randomFirstAsteroid).GetComponent<Transform>();
        firstAsteroid.position = transform.position;
        firstAsteroid.Rotate(0.0f, 0.0f, Random.Range(0.1f, 360.0f));
        firstAsteroid.localScale /= 2.0f;
        firstAsteroid.GetComponent<Asteroid>().SetTimesSplit(timesSplit + 1);

        Transform secondAsteroid = Instantiate(randomSecondAsteroid).GetComponent<Transform>();
        secondAsteroid.position = transform.position;
        secondAsteroid.Rotate(0.0f, 0.0f, Random.Range(0.1f, 360.0f));
        secondAsteroid.localScale /= 2.0f;
        secondAsteroid.GetComponent<Asteroid>().SetTimesSplit(timesSplit + 1);
    }

    public void SetTimesSplit(int timesSplit)
    {
        this.timesSplit = timesSplit;
    }
}
