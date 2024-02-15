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
        //transform.rotation=Camera.main.transform.rotation*originalRotation;

        transform.rotation=Quaternion.LookRotation(Vector3.ProjectOnPlane(-Camera.main.transform.position+transform.position,Vector3.up), Vector3.up);
        
        //transform.rotation=Quaternion.Euler(new Vector3(0,-Camera.main.transform.rotation.eulerAngles.y,0));
    }
}
