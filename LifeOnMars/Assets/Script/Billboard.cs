using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Billboard : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField]
    bool positiveAxis=false;
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        //transform.rotation=Camera.main.transform.rotation*originalRotation;

        Turn();
        
        //transform.rotation=Quaternion.Euler(new Vector3(0,-Camera.main.transform.rotation.eulerAngles.y,0));
    }

    public void Turn(){
        Vector3 differenceVector=-Camera.main.transform.position+transform.position;
        if(positiveAxis)
            differenceVector*=-1;
        transform.rotation=Quaternion.LookRotation(Vector3.ProjectOnPlane(differenceVector,Vector3.up), Vector3.up);
    }
}
