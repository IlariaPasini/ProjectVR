using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

//Classe Vinz singleton: TaskSystem

public class TaskListDisplay : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI tasklistText;
    // Start is called before the first frame update
    void Start()
    {
        TaskSystem.Init();
    }

    // Update is called once per frame
    //Non ho capito bene se la print va comunque fatta in update
    void Update()
    {
        List<Task> task_list = TaskSystem.instance.GetTasks();
        if(task_list.Count != 0){
            foreach (Task task in task_list)
            {
                Debug.Log(task.ToString());
                tasklistText.text += task.ToString() + "\n";
            }
        }else{
            tasklistText.text = "Non ci sono task disponibili!";
        }
    }
}
