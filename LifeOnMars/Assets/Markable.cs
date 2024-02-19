using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Markable : MonoBehaviour
{
    void Start(){
        PositionToMap.AddMarker(transform);
    }
    void OnEnable(){
        PositionToMap.AddMarker(transform);
    }

    void OnDisable(){
        PositionToMap.RemoveMarker(transform);
    }


    // Update is called once per frame
    void Update()
    {
        
    }
}
