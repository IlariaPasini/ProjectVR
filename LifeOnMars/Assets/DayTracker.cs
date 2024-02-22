using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class DayTracker : MonoBehaviour
{
    // Start is called before the first frame update
    TextMeshProUGUI tmp;
    void Start()
    {
        tmp=GetComponent<TextMeshProUGUI>();
        StartCoroutine(Polling());
    }

    // Update is called once per frame
    IEnumerator Polling()
    {   
        while(true){
        tmp.text="Giorno "+DaySystem.DayNumber+"\n Task del giorno: "+DaySystem.TaskForTheDay;
        yield return new WaitForSeconds(1);
        }
    }
}
