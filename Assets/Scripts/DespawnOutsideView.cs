using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DespawnOutsideView : MonoBehaviour
{
    // Update is called once per frame
    void Update()
    {
        if (!isVisible(GetComponent<Renderer>(), Camera.main))
        {
            Destroy(gameObject);
        }
    }

    public bool isVisible(Renderer renderer, Camera camera) {
      Plane[] planes = GeometryUtility.CalculateFrustumPlanes(camera);
      return GeometryUtility.TestPlanesAABB(planes, renderer.bounds);
   }
}
