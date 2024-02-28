using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class ChangeDay : MonoBehaviour
{
    // Start is called before the first frame update

    [SerializeField]
    UnityEvent onNextDayDeny;

    [SerializeField]
    bool debugMode;
    [SerializeField]
    bool active=true;

    public bool Active { get => active; set => active = value; }

    void Start()
    {
        #if !UNITY_EDITOR
        debugMode=false;
        #endif
    }


    /// <summary>
    /// Fa partire l'animazione di fade e cambia giorno con il Fade
    /// </summary>
    public void NextDayFade(){
        if((TaskSystem.instance.TaskCompleted>=DaySystem.TaskForTheDay || debugMode) && Active){
            TaskSystem.instance.ResetTasks();
            Fader.NextDay();
        }else{
            onNextDayDeny.Invoke();
        }
    }
    // Update is called once per frame
    public void NextDay(){
        if((TaskSystem.instance.TaskCompleted>=DaySystem.TaskForTheDay || debugMode) && Active){
            TaskSystem.instance.ResetTasks();
            DaySystem.DayNumber++;
        }else{
            onNextDayDeny.Invoke();
        }
    }

    public void ResetDay(){
        if((TaskSystem.instance.TaskCompleted>=DaySystem.TaskForTheDay || debugMode) && Active){
            DaySystem.TaskForTheDay = 0;
            TaskSystem.instance.ResetTasks();
        }else{
            onNextDayDeny.Invoke();
        }
    }
}
