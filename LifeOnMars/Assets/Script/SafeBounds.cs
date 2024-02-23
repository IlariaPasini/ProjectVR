using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SafeBounds : MonoBehaviour
{
    // Start is called before the first frame update
    float z;
    private Vector3 resetPosition;

    public Vector3 ResetPosition { get => resetPosition; set => resetPosition = value; }

    void Start()
    {
        ResetPosition=transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        if((transform.position.y<-30 && transform.position.y>-100) || (transform.position.y<-1800)){
            transform.position=ResetPosition;
        }   
    }


}
