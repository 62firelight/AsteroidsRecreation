using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelMaster : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        // Ensure that the level master object is always centered on the origin
        transform.position = Vector3.zero;

        EdgeCollider2D[] edges = GetComponents<EdgeCollider2D>();

        if (edges.Length == 4)
        {
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
        // Whenever R is pressed, restart the current scene
        if (Input.GetKeyDown(KeyCode.R))
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
    }

    void OnGUI()
    {
        GUI.skin.label.alignment = TextAnchor.UpperCenter;
        GUI.Label(new Rect(0, 0, Camera.main.pixelWidth, Camera.main.pixelHeight), "Score: " + GameMaster.score);
    }
}
