using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using Unity.VisualScripting;
using UnityEditor.VersionControl;
using UnityEngine;

[Serializable]
public class Task{
    public string name;
    public string description;
    public int target;
    public int current_done;
    public Action on_update;
    public Action on_done;


    public Task(string name, string description, int target){
        this.name=name;
        this.description=description;
        this.target=target;
    }
    public Task(string name, string description, int target, Action callback){
        this.name=name;
        this.description=description;
        this.target=target;
        on_update+=callback;
    }

    public bool Update(int amount){
        current_done+=amount;
        
        if(on_update!=null)
            on_update();

        if(current_done==target){
            if(on_done!=null)
                on_done();
        }
        return current_done>=target;
    }

    public override string ToString(){
        return name+": "+description+" "+current_done+"/"+target;
    }
}

public class TaskSystem
{
    // Start is called before the first frame update
    public Action<Task> on_new_task;
    Dictionary<string, Task> tasks=new Dictionary<string, Task>();
    public static TaskSystem instance;

    public static void Init()
    {
        
        
        if(instance==null){
            instance=new TaskSystem();
        }
        instance.on_new_task=null;
    }

    public Task GetTask(string task_name){
        if(!tasks.ContainsKey(task_name))
            return null;
        return tasks[task_name];
    }

    public void UpdateTask(string task_name, int amount){
        if(tasks[task_name].Update(amount)){
            tasks.Remove(task_name);
        }
    }

    public void AddTask(string task_name, Task task){
        if(tasks.ContainsKey(task_name))
            return;
        tasks.Add(task_name,task);
        if(on_new_task!=null)
            on_new_task(task);
    }
}
