using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitPlay : MonoBehaviour
{
    // Start is called before the first frame update
    #nullable enable
    new AudioSource? audio;
    Vector3 startPos;
    Quaternion startRot;
    void Start()
    {
        audio=GetComponent<AudioSource>();
        startPos=transform.position;
        startRot=transform.rotation;
    }

    // Update is called once per frame
    void OnCollisionEnter(Collision c)
    {
        if(audio!=null)
            audio.Play();
    }

    public void Reset(){
        
        transform.position=startPos;
        transform.rotation=startRot;
    }
}
