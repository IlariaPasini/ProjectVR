using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public struct Task{
    string name;
    string description;
    int target;
    int current_done;
    Action<int> on_update;
}

public class TaskSystem : MonoBehaviour
{
    // Start is called before the first frame update

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void GetTask(string name){
        
    }
}
