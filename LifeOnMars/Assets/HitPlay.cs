using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitPlay : MonoBehaviour
{
    // Start is called before the first frame update
    new AudioSource audio;
    void Start()
    {
        audio=GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void OnCollisionEnter(Collision c)
    {
        audio?.Play();
    }
}
