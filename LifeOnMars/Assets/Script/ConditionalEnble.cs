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
        SceneManager.activeSceneChanged+=OnActiveScene;
    }

    void OnDestroy(){
        SceneManager.activeSceneChanged-=OnActiveScene;
    }
    

    void OnActiveScene(Scene s, Scene s2){
                    if(TaskSystem.instance?.GetTask(task_name)!=null)
                gameObject.SetActive(true);
    }



}
