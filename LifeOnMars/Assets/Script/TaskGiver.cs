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
    // Start is called before the first frame update
    void Start()
    {
        
    }

    public void GiveTask(){
        TaskSystem.instance.AddTask(task.name, task);
    }
    // Update is called once per frame
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
