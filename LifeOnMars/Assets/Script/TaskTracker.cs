using System.Collections;
using System.Collections.Generic;
using JetBrains.Annotations;
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
        
        TaskSystem.instance.on_any_task_update+=Refresh;
        Refresh();
        // TaskSystem.instance.on_new_task+=(_)=>{
        //     UpdateTask();
        // };
        // UpdateTask();

    }

    public void OnDestroy(){
        TaskSystem.instance.on_any_task_update-=Refresh;
    }

    public void Refresh(){
        List<Task> tasks=TaskSystem.instance?.GetTasks();
        if(tasks==null){
            tmp.text="";
            return;
        }
        if(tasks.Count>task_id){
            tmp.text=tasks[task_id].ToString();
        }else{
            tmp.text="";
            return;
        }
    }

    void UpdateTask(Task task){
        tmp.text=task.ToString();
    }
    // Update is called once per frame
    void UpdateTask()
    {
        List<Task> tasks=TaskSystem.instance.GetTasks();
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
