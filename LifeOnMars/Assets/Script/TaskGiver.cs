using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TaskGiver : Receiver
{
    [SerializeField]
    Task task;
    [SerializeField]
    string expected_object;
    [SerializeField]
    bool answer_on_receive;
    [SerializeField]
    Dialogue answer;

    /// <summary>
    /// Registra la task nel Task System
    /// </summary>
    public void GiveTask(){
        TaskSystem.instance.AddTask(task.name, task);
    }
    /// <summary>
    /// Riceve un oggetto solo se è l'oggetto che si aspetta (expected object) e aggiorna la relativa task, se answer_on_receive, allora cambia pure il dialogo del dialogue system
    /// </summary>
    /// <param name="g"></param>
    public override void Receive(GameObject g)
    {
        Storable s;
        if(g.TryGetComponent<Storable>(out s) && s.ItemName==expected_object){
            base.Receive(g);
            task.Update(1);

            if(answer_on_receive){
                DialogueSystem ds=GetComponentInChildren<DialogueSystem>();
                ds.SetDialogue(answer);
            }
            
        }
            

    }
}
