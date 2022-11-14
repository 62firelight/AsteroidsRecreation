using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Utility
{
    public static bool isVisible(Renderer renderer, Camera camera)
    {
        Plane[] planes = GeometryUtility.CalculateFrustumPlanes(camera);
        return GeometryUtility.TestPlanesAABB(planes, renderer.bounds);
    }
}
