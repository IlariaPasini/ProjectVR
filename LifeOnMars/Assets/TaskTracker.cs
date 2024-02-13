using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class TaskTracker : MonoBehaviour
{
    // Start is called before the first frame update
    TextMeshProUGUI tmp;
    [SerializeField]
    string taskToTrack;
    void Start()
    {
        tmp=GetComponent<TextMeshProUGUI>();
        TaskSystem.Init();
        TaskSystem.instance.on_new_task+=(Task task)=>{
            if(task.name==taskToTrack){
                tmp.text=task.ToString();

                TaskSystem.instance.GetTask(task.name).on_update+= ()=>{
                    print("Updated!");
                    tmp.text=task.ToString();
                };
                TaskSystem.instance.GetTask(task.name).on_done+= ()=>{
                    print("Updated!");
                    tmp.text=task.ToString()+"\n Bravo Shinji!";
                };
                
            }
        };


    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
