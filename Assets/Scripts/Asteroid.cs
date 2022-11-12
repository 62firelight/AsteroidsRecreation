using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Asteroid : MonoBehaviour
{

    public Transform asteroidPrefab;

    private bool shotOnce = false;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnCollisionEnter2D(Collision2D other) 
    {
        if (!shotOnce)   
        {
            Transform firstAsteroid = Instantiate(asteroidPrefab);
            firstAsteroid.rotation = new Quaternion(firstAsteroid.rotation.x, firstAsteroid.rotation.y, Random.Range(0, 360), firstAsteroid.rotation.w);
            firstAsteroid.localScale /= 2.0f;
            firstAsteroid.GetComponent<Asteroid>().SetShotOnce(true);

            Transform secondAsteroid = Instantiate(asteroidPrefab);
            secondAsteroid.rotation = new Quaternion(secondAsteroid.rotation.x, secondAsteroid.rotation.y, Random.Range(0, 360), secondAsteroid.rotation.w);
            secondAsteroid.localScale /= 2.0f;
            secondAsteroid.GetComponent<Asteroid>().SetShotOnce(true);
        }

        Destroy(gameObject);
    }

    public void SetShotOnce(bool state)
    {
        shotOnce = state;
    }
}
