using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Billboard : MonoBehaviour
{
    // Start is called before the first frame update
    Quaternion originalRotation;
    void Start()
    {
        originalRotation=transform.rotation;
    }

    // Update is called once per frame
    void Update()
    {
        transform.rotation=Camera.main.transform.rotation*originalRotation;
    }
}
