using System.Linq;
using System.Collections.Generic;
using UnityEngine;

[DisallowMultipleComponent]
public class IKControl : MonoBehaviour
{
    #region Variables
    [SerializeField] private Transform armature;    // armature
    [SerializeField] private List<Transform> IKs;   // list of all IKnodes
    private Vector3 lastHigherPos = Vector3.zero,   // last higher leg
        bodyPivot = Vector3.zero;                   // intersection directly under the body
    
    private RaycastHit hit;                         // hit out struct for the raycast functions
    private Ray ray;                                // ray for the raycast
    #endregion

    #region Gizmo
    void OnDrawGizmos()
    {
        // draw gizmos to check legs' IK position
        Gizmos.color = Color.yellow;
        foreach (Transform t in IKs)
        {
            // Draw a yellow sphere for each leg
            Gizmos.DrawWireSphere(t.position, .1f);
            Gizmos.DrawLine(t.position, t.position + t.up);
        }

        // draw a yello sphere for the body
        Gizmos.DrawWireSphere(bodyPivot, .1f);
    }
    #endregion

    private void Update()
    {
        SetIKPositions();
    }

    private void SetIKPositions()
    {
        // set roverìs height based on ground height with offset
        Vector3 offset = new Vector3(0.0f, 0.025f, 0.0f);
        ray = new(transform.position + new Vector3(0.0f, 10.0f, 0.0f), Vector3.down); // start the ray 10m higher to surely hit the ground
        if (Physics.Raycast(ray, out hit, Mathf.Infinity))
        {
            transform.position = hit.point + offset;
        }
        

        // set rover's leg positions based on ground height with offset
        offset = new Vector3(0.0f, 0.05f, 0.0f);
        foreach (Transform t in IKs)
        {
            ray = new(t.position + new Vector3(0.0f, 10.0f, 0.0f), Vector3.down); // start the ray 10m higher to surely hit the ground
            if (Physics.Raycast(ray, out hit, Mathf.Infinity))
            {
                // set position based on hit height
                t.position = Vector3.Lerp(t.position, hit.point + offset, 0.4f);
                // set rotation based on hit normal
                t.up = Vector3.Lerp(t.up, hit.normal, 0.5f);

                if (lastHigherPos.y < t.position.y)
                {
                    lastHigherPos = hit.point + new Vector3(0.0f, 0.1f, 0.0f);
                }
            }
        }
    }
}