using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DaySystem
{
    private static int day_number = 0;

    private static int task_for_the_day=0;

    public static Action<int> onDayChange;

    public static int DayNumber { get => day_number; 
    set{ day_number = value; 
        task_for_the_day=0;
        if(onDayChange!=null) 
            onDayChange(value);
    }}

    public static int TaskForTheDay { get => task_for_the_day; set => task_for_the_day = value; }
}
