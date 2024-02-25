using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

public class EnableAtDay : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField]
    int day;
    [SerializeField]
    UnityEvent events;
    void Start()
    {
        if(day==DaySystem.DayNumber){
            events.Invoke();
        }
        DaySystem.onDayChange+=Callback;
        
    }

    void OnDestroy(){
        DaySystem.onDayChange-=Callback;
    }

    // Update is called once per frame
    void Callback(int day)
    {
        if(day==DaySystem.DayNumber)
            events.Invoke();
    }
}
