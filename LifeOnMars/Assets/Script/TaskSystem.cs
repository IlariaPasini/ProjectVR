using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem.Interactions;
using UnityEngine.SceneManagement;

[Serializable]
public class Task{
    public string name;
    public bool done;
    public string description;
    public int target;
    public int current_done;
    public Action on_update;
    public Action on_done;

    public UnityEvent events;


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

    /// <summary>
    /// Aggiorna il valore di una task e ritorna vero se la task è completata
    /// </summary>
    /// <param name="amount"></param>
    /// <returns></returns>
    public bool Update(int amount){
        current_done+=amount;
        on_update?.Invoke();

        if(current_done==target){
            done=true;
            if(on_done!=null)
                on_done();
            events.Invoke();
        }
        return current_done>=target;
    }

    
    /// <summary>
    /// Cast a stringa, viene printato come "nome:descrizione valore_corrente/valore_obiettivo"
    /// </summary>
    /// <returns></returns>
    public override string ToString(){
        return name+": "+description+" "+current_done+"/"+target;
    }
}

public class TaskSystem
{
    // Start is called before the first frame update
    public Action on_any_task_update;
    public Action<Task> on_new_task;

    public Action onTasksDone;
    Dictionary<string, Task> tasks=new Dictionary<string, Task>();
    public static TaskSystem instance;
    int scene=-1;

    /// <summary>
    /// Inizializza il singleton, dovrebbe essere chiamato almeno una volta (>=1) nella scena
    /// </summary>
    public static void Init()
    {
        if(instance==null){
            instance=new TaskSystem();
        }

        SceneManager.activeSceneChanged+=(_,_)=>{
            if(instance!=null)
                instance.on_new_task=null;
            };

    }

    /// <summary>
    /// Ottiene una task cercandola per nome
    /// </summary>
    /// <param name="task_name"></param>
    /// <returns></returns>

    public Task GetTask(string task_name){
        if(!tasks.ContainsKey(task_name))
            return null;
        return tasks[task_name];
    }
    /// <summary>
    /// Incrementa di "amount" il valore corrente di una task, se viene completata la task viene rimossa dal dictionary
    /// </summary>
    /// <param name="task_name"></param>
    /// <param name="amount"></param>

    public void UpdateTask(string task_name, int amount){
        if(!tasks.ContainsKey(task_name))
            return;


        tasks[task_name].Update(amount);
        on_any_task_update?.Invoke();
        // if(tasks[task_name].Update(amount)){
        //     tasks.Remove(task_name);
        //     on_new_task(null);
        // }
    }

    public int TaskCompleted{
        get{
            return tasks.Count((pair)=>pair.Value.done);
        }
    }

    public bool AllTasksDone{
        get{
            return tasks.Count((pair)=>pair.Value.done)==tasks.Count;
        }
    }
    /// <summary>
    /// Aggiunge una task, se è gia presente una task con lo stesso nome non fa nulla
    /// </summary>
    /// <param name="task_name"></param>
    /// <param name="task"></param>
    public void AddTask(string task_name, Task task){
        if(tasks.ContainsKey(task_name))
            return;
        tasks.Add(task_name,task);
        on_any_task_update?.Invoke();
        if(on_new_task!=null)
            on_new_task(task);
    }

    public void ResetTasks(){
        tasks.Clear();
        on_any_task_update?.Invoke();
        if(on_new_task!=null)
            on_new_task(null);

    }
    //metodo che permette di ottenere la lista di Task presenti nel dizionario
    public List<Task> GetTasks(){
        return tasks.Values.ToList();
    }

}
