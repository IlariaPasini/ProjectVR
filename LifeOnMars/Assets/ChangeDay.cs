using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class ChangeDay : MonoBehaviour
{
    // Start is called before the first frame update

    [SerializeField]
    UnityEvent onNextDayDeny;
    void Start()
    {
        
    }

    // Update is called once per frame
    public void NextDay(){
        if(TaskSystem.instance.TaskCompleted==DaySystem.TaskForTheDay){
            TaskSystem.instance.ResetTasks();
            DaySystem.DayNumber++;
        }else{
            onNextDayDeny.Invoke();
        }
    }
}
