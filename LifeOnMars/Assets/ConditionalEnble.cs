using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ConditionalEnble : MonoBehaviour
{
    [SerializeField]
    string task_name;

    void Start()
    {
        
    }

    void Update(){
        SceneManager.activeSceneChanged+=(_,_)=>{
            if(TaskSystem.instance.GetTask(task_name)!=null)
                gameObject.SetActive(true);
            
        };
        if(TaskSystem.instance.GetTask(task_name)==null){
            gameObject.SetActive(false);
        }
        enabled=false;   
    }
}
