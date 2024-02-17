using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PositionToMap : MonoBehaviour
{
    [SerializeField]
    private List<Transform> staticPointers,
        dynamicPointers;

    [SerializeField] private Transform playerTarget, baseTarget;
    [SerializeField]
    private float worldScale = 11.0f,
        mapScale = 0.1f;

    void Start()
    {
        foreach (Transform t in staticPointers)
        {
            t.localPosition = new Vector3(baseTarget.position.x, baseTarget.position.z, 0.1f) / worldScale * mapScale;
        }
    }

    // Update is called once per frame
    void Update()
    {
        foreach (Transform t in dynamicPointers)
        {
            t.localPosition = new Vector3(playerTarget.position.x, playerTarget.position.z, 0.1f) / worldScale * mapScale;
        }
    }
}
