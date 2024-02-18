using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SimpleTaskGiver : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField]
    Task task;
    [SerializeField]
    int day;

    public void Start(){
        DaySystem.onDayChange+=(d)=>{
            if(day==d){
                DaySystem.TaskForTheDay++;
            }
        };
    }
    public void GiveTask()
    {
        TaskSystem.instance.AddTask(task.name, task);
    }

    
}
