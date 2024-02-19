using System.Collections;
using System.Collections.Generic;
using System.Linq.Expressions;
using TMPro;
using Unity.XR.CoreUtils;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

public class ChangeScene : MonoBehaviour
{
    [SerializeField]
    string target="TestDestination";
    /// <summary>
    /// Cambia la scena chiamando l'animazione di fade
    /// </summary>
    /// <param name="id"></param>
    public void GoToSceneWithFade(int id){
        Fader.GotoScene(id);
    }
    
    public void GotoScene(int id){
        //SceneManager.LoadScene(id);
        
        AsyncOperation op=SceneManager.LoadSceneAsync(id, LoadSceneMode.Additive);
        SceneManager.sceneLoaded+=OnSceneLoaded;
        
    }

    void OnSceneLoaded(Scene s, LoadSceneMode m){
        Transform xro=FindObjectOfType<XROrigin>().transform.root;
        SceneManager.SetActiveScene(s);
        SceneManager.MoveGameObjectToScene(xro.gameObject, SceneManager.GetActiveScene());
        xro.position=GameObject.Find(target).transform.position;

        SceneManager.sceneLoaded-=OnSceneLoaded;
    }

    public void ChangeDay(){
        if(TaskSystem.instance.TaskCompleted==DaySystem.TaskForTheDay){
            TaskSystem.instance.ResetTasks();
            DaySystem.DayNumber++;
        }
    }
}
