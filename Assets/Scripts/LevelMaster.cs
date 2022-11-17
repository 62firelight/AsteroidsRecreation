using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelMaster : MonoBehaviour
{
    public Transform asteroidPrefab;

    public int initialNumberOfAsteroids = 4;

    public float defaultNextLevelCooldown = 2.0f;

    public Transform bigSaucerPrefab;

    public Transform smallSaucerPrefab;

    public float defaultBigSaucerCooldown = 15.0f;

    public PlayerUtility playerUtility;

    private int numberOfAsteroids;

    private Vector2[][] boundaries;

    private float nextLevelCooldown = 0.0f;

    private float bigSaucerCooldown = 0.0f;

    private float smallSaucerCooldown = 0.0f;

    // Start is called before the first frame update
    void Start()
    {
        // Ensure that the level master object is always centered on the origin
        transform.position = Vector3.zero;

        // Get coordinates for bottom-left point and top-right point of screen
        Vector2 bottomLeftPoint = Camera.main.ScreenToWorldPoint(Vector3.zero);
        Vector2 topLeftPoint = Camera.main.ScreenToWorldPoint(new Vector3(0, Camera.main.pixelHeight, 0));
        Vector2 topRightPoint = Camera.main.ScreenToWorldPoint(new Vector3(Camera.main.pixelWidth, Camera.main.pixelHeight, 0));
        Vector2 bottomRightPoint = Camera.main.ScreenToWorldPoint(new Vector3(Camera.main.pixelWidth, 0, 0));

        // Create boundary points for each edge of the screen
        Vector2[] leftBoundaryPoints = new Vector2[] { bottomLeftPoint, topLeftPoint };
        Vector2[] topBoundaryPoints = new Vector2[] { topLeftPoint, topRightPoint };
        Vector2[] rightBoundaryPoints = new Vector2[] { topRightPoint, bottomRightPoint };
        Vector2[] bottomBoundaryPoints = new Vector2[] { bottomRightPoint, bottomLeftPoint };
        boundaries = new Vector2[][] { leftBoundaryPoints, topBoundaryPoints, rightBoundaryPoints, bottomBoundaryPoints };

        numberOfAsteroids = initialNumberOfAsteroids + GameMaster.levelsCompleted;
        SpawnAsteroids();
        // Each asteroid is technically 7 asteroids because of how they split
        GameMaster.asteroidsLeft = numberOfAsteroids * 7;

        bigSaucerCooldown = defaultBigSaucerCooldown;
        smallSaucerCooldown = defaultBigSaucerCooldown * 1.5f;

        // Set up screen boundaries
        // Currently disabled
        EdgeCollider2D[] edges = GetComponents<EdgeCollider2D>();
        if (edges.Length == 4)
        {
            // Set boundary points
            edges[0].points = leftBoundaryPoints;
            edges[1].points = topBoundaryPoints;
            edges[2].points = rightBoundaryPoints;
            edges[3].points = bottomBoundaryPoints;
        }
        else
        {
            Debug.LogError("LevelMaster has less than or more than 4 edge colliders");
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (nextLevelCooldown > 0)
        {
            nextLevelCooldown -= Time.deltaTime;

            if (nextLevelCooldown <= 0)
            {
                NextLevel();
            }
        }
        else if (GameMaster.playerLives > 0 && GameMaster.asteroidsLeft <= 0)
        {
            nextLevelCooldown = defaultNextLevelCooldown;

            if (playerUtility != null)
            {
                playerUtility.HidePlayer();
            }
        }

        if (bigSaucerCooldown > 0)
        {
            bigSaucerCooldown -= Time.deltaTime;

            if (bigSaucerCooldown <= 0)
            {
                SpawnSaucer(false);
                bigSaucerCooldown = defaultBigSaucerCooldown;
            }
        }

        if (smallSaucerCooldown > 0)
        {
            smallSaucerCooldown -= Time.deltaTime;

            if (smallSaucerCooldown <= 0)
            {
                SpawnSaucer(true);
                smallSaucerCooldown = defaultBigSaucerCooldown * 1.5f;
            }
        }

        // Whenever R is pressed, restart the game
        if (Input.GetKeyDown(KeyCode.R))
        {
            RestartGame();
        }

        // Whenever N is pressed, progress to the next level
        if (GameMaster.playerLives > 0 && Input.GetKeyDown(KeyCode.N))
        {
            NextLevel();
        }
    }

    void OnGUI()
    {
        GUI.skin.label.alignment = TextAnchor.UpperCenter;
        if (GameMaster.playerLives > 0)
        {
            GUI.Label(new Rect(0, 0, Camera.main.pixelWidth, Camera.main.pixelHeight), "Score: " + GameMaster.score + "    Lives: " + GameMaster.playerLives);
        }
        else
        {
            GUI.Label(new Rect(0, 0, Camera.main.pixelWidth, Camera.main.pixelHeight), "Game Over\nPress R to restart");
        }
    }

    void SpawnAsteroids()
    {
        if (boundaries == null)
        {
            Debug.LogError("Screen boundaries has not been initialized before attempting to spawn asteroids!");
            return;
        }

        for (int i = 0; i < numberOfAsteroids; i++)
        {
            // Select random boundary edge
            int randomBoundaryIndex = Random.Range(0, boundaries.Length);
            Vector2[] randomBoundary = boundaries[randomBoundaryIndex];

            // Select random position on the selected boundary edge
            Vector2 firstPos = randomBoundary[0];
            Vector2 secondPos = randomBoundary[1];
            Vector2 randomPos = new Vector2(Random.Range(firstPos.x, secondPos.x), Random.Range(firstPos.y, secondPos.y));

            // Add small offset to create variety in random positions
            randomPos += new Vector2(Random.Range(-0.5f, 0.5f), Random.Range(-0.5f, 0.5f));

            Instantiate(asteroidPrefab, randomPos, transform.rotation);
        }
    }

    void SpawnSaucer(bool isSmallSaucer)
    {
        // Flip a coin to see if the saucer should spawn on the left or right side
        int rightSide = Random.Range(0, 2);
        Vector2 bottomLeftPoint = boundaries[0][0];
        Vector2 topRightPoint = boundaries[1][1];

        // Decide saucer position based on coin flip above
        Vector2 saucerPosition;
        if (rightSide == 0)
        {
            saucerPosition = new Vector2(bottomLeftPoint.x - 0.5f, Random.Range(bottomLeftPoint.y + 0.5f, topRightPoint.y - 0.5f));
        }
        else
        {
            saucerPosition = new Vector2(topRightPoint.x + 0.5f, Random.Range(bottomLeftPoint.y + 0.5f, topRightPoint.y - 0.5f));
        }

        // Spawn big saucer
        Transform saucer;
        if (isSmallSaucer)
        {
            saucer = Instantiate(smallSaucerPrefab);
        }
        else
        {
            saucer = Instantiate(bigSaucerPrefab);
        }
        saucer.position = saucerPosition;
    }

    void NextLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        GameMaster.levelsCompleted++;
    }

    void RestartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        GameMaster.Reset();
    }
}
