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


    /// <summary>
    /// Fa partire l'animazione di fade e cambia giorno con il Fade
    /// </summary>
    public void NextDayFade(){
        if(TaskSystem.instance.TaskCompleted>=DaySystem.TaskForTheDay){
            TaskSystem.instance.ResetTasks();
            Fader.NextDay();
        }else{
            onNextDayDeny.Invoke();
        }
    }
    // Update is called once per frame
    public void NextDay(){
        if(TaskSystem.instance.TaskCompleted>=DaySystem.TaskForTheDay){
            TaskSystem.instance.ResetTasks();
            DaySystem.DayNumber++;
        }else{
            onNextDayDeny.Invoke();
        }
    }
}
