using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

public class TaskGiver : NPCReceiver
{
    [SerializeField]
    Task task;
    [SerializeField]
    string expected_object;
    [SerializeField]
    bool answer_on_receive;
    [SerializeField]
    Dialogue defDialogue;
    [SerializeField]
    Dialogue taskDialogue;
    [SerializeField]
    UnityEvent onAnswerStart;
    [SerializeField]
    UnityEvent onTaskDialogue;
    [SerializeField]
    Dialogue answer;
    [SerializeField]
    int day;

    public new void Start(){
        base.Start();

        onTaskDialogue.AddListener(GiveTask);
        UnityEvent emptyEvent=new UnityEvent();
        if(DaySystem.DayNumber==day){
            DaySystem.TaskForTheDay++;
            ds.SetDialoguePermanent(taskDialogue, onTaskDialogue);
        }
        if(ds!=null){
            DaySystem.onDayChange+=(d)=>{
                print(d+" "+ day);
                if(day==d){
                    DaySystem.TaskForTheDay++;
                    ds.SetDialoguePermanent(taskDialogue, onTaskDialogue);
                }else{
                    if(ds==null){
                        print("Dialogue System is Null");
                    }else
                        ds.SetDialoguePermanent(defDialogue, emptyEvent);
                }
            };
        }

    }

    /// <summary>
    /// Registra la task nel Task System
    /// </summary>
    public void GiveTask(){
        TaskSystem.instance.AddTask(task.name, task);
        TaskSystem.instance.GetTask(task.name).on_done+=()=>ds.SetDialoguePermanent(defDialogue, new UnityEvent());
    }

    /// <summary>
    /// Makes sure the receiver wants that object
    /// </summary>
    /// <param name="s"></param>
    /// <returns></returns>
    public override bool CanReceive(Storable s)
    {

        return (day==DaySystem.DayNumber && s.ItemName==expected_object) || base.CanReceive(s);
    }
    /// <summary>
    /// Riceve un oggetto solo se è l'oggetto che si aspetta (expected object) e aggiorna la relativa task, se answer_on_receive, allora cambia pure il dialogo del dialogue system
    /// </summary>
    /// <param name="g"></param>
    public override void Receive(GameObject g)
    {
        base.Receive(g);
        if(g.GetComponent<Storable>().ItemName!=expected_object)
            return;
        
        TaskSystem.instance.UpdateTask(task.name, 1);
        // task.Update(1);

        if(answer_on_receive){
            onAnswerStart.Invoke();
            ds.SetDialogueTemp(answer);
            ds.Talk();
        }
    }
}
