using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WanderArea : MonoBehaviour
{
    public Vector3 wanderDistance = new Vector3(1, 1, 1);
    public Color color = Color.white;

#if UNITY_EDITOR

    void OnDrawGizmos()
    {
        // Draw a semitransparent blue cube at the transforms position
        Gizmos.color = color;
        Gizmos.DrawWireCube(transform.position, wanderDistance);
    }

#endif
}
