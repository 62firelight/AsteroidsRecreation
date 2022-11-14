using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoopOutsideView : MonoBehaviour
{
    private float loopCooldown = 0.0f;

    // Update is called once per frame
    void Update()
    {
        Vector2 bottomLeftPoint = Camera.main.ScreenToWorldPoint(Vector3.zero);
        Vector2 topRightPoint = Camera.main.ScreenToWorldPoint(new Vector3(Camera.main.pixelWidth, Camera.main.pixelHeight, 0));

        if (loopCooldown <= 0.0f && !Utility.isVisible(GetComponent<Renderer>(), Camera.main))
        {
            bool posChanged = false;

            // Object is outside the left or right boundary
            if (transform.position.x < bottomLeftPoint.x || transform.position.x > topRightPoint.x)
            {
                transform.position = new Vector3(-transform.position.x, transform.position.y, transform.position.z);
                posChanged = true;
            }

            // Object is outside the bottom or top boundary
            if (transform.position.y < bottomLeftPoint.y || transform.position.y > topRightPoint.y)
            {
                transform.position = new Vector3(transform.position.x, -transform.position.y, transform.position.z);
                posChanged = true;
            }

            if (posChanged)
            {
                // Delay the next position change slightly,
                // so that the object doesn't get stuck out of bounds
                // teleporting to different positions
                loopCooldown = 0.5f;
            }
        }
        else if (loopCooldown > 0)
        {
            loopCooldown -= Time.deltaTime;
        }
    }
}
