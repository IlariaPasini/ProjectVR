using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class TaskTracker : MonoBehaviour
{
    // Start is called before the first frame update
    TextMeshProUGUI tmp;
    [SerializeField]
    int task_id;
    bool tracking=false;
    void Start()
    {
        tmp=GetComponent<TextMeshProUGUI>();
        TaskSystem.Init();
        
        TaskSystem.instance.on_new_task+=(_)=>{
            UpdateTask();
        };
        UpdateTask();

    }

    // Update is called once per frame
    void UpdateTask()
    {
        List<Task> tasks=TaskSystem.instance.GetTasks();
        print(tasks.Count);
        if(!tracking && tasks.Count>task_id){
            tmp.text=tasks[task_id].ToString();
            tasks[task_id].on_update+= ()=>{
                    print("Updated!");
                    tmp.text=tasks[task_id].ToString();
                };
            tasks[task_id].on_done+= ()=>{
                    print("Updated!");
                    tmp.text=tasks[task_id].ToString();
                };
        }

        else if(tasks.Count<=task_id){
            tmp.text="";
            tracking=false;
        }
    }
}
