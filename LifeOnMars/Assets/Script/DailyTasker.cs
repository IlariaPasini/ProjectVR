using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class DailyTask{
    public List<Task> task;
}

public class DailyTasker : MonoBehaviour
{

    [SerializeField] List<DailyTask> taskPerDay;
    void Start()
    {
        DaySystem.onDayChange+=OnDayChange;
        OnDayChange(0);
    }

    void OnDestroy(){
        DaySystem.onDayChange-=OnDayChange;
    }
    // Update is called once per frame
    void OnDayChange(int day)
    {
        if(taskPerDay.Count>day){
            foreach(Task t in taskPerDay[day].task){
                DaySystem.TaskForTheDay++;
                TaskSystem.instance.AddTask(t.name,t);
            }
        }
    }
}
