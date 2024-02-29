using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class DayTracker : MonoBehaviour
{
    // Start is called before the first frame update
    TextMeshProUGUI tmp;
    [SerializeField] int lastDay=5;
    void Start()
    {
        tmp=GetComponent<TextMeshProUGUI>();
        
        StartCoroutine(Polling());
    }

    // Update is called once per frame
    IEnumerator Polling()
    {   
        while(true){

        tmp.text="Giorno "+(DaySystem.DayNumber+1) + "\nTask completate: " + TaskSystem.instance.TaskCompleted  + "\nTask del giorno: " + DaySystem.TaskForTheDay;
        yield return new WaitForSeconds(1);
            if(DaySystem.DayNumber==lastDay){
                tmp.text=" La missione è finita, tempo di tornare!\n Sali sulla navicella fuori!";
                break;
            }
        }
    }
}
