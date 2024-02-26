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
    [SerializeField]
    int other_scene=-1;
    [SerializeField]
    LoadSceneMode mode=LoadSceneMode.Additive;
    public static bool other_scene_loaded=false;

    public void Start(){
        if(!other_scene_loaded && other_scene!=-1){
            other_scene_loaded=true;
            Scene s=SceneManager.GetSceneByBuildIndex(other_scene);
            AsyncOperation op=SceneManager.LoadSceneAsync(other_scene, mode);
            
        }
    }
    /// <summary>
    /// Cambia la scena chiamando l'animazione di fade
    /// </summary>
    /// <param name="id"></param>
    public void GoToSceneWithFade(int id){
        Fader.GotoScene(id,this);
    }
    
    public void GotoScene(int id){
        //SceneManager.LoadScene(id);
        Scene s=SceneManager.GetSceneByBuildIndex(id);
        if(s.isLoaded){
            MovePlayer(s);
            Fader.PlayFadeIn();
        }else{
            AsyncOperation op=SceneManager.LoadSceneAsync(id, mode);
            SceneManager.sceneLoaded+=OnSceneLoaded;
        }
    }

    void OnSceneLoaded(Scene s, LoadSceneMode m){
        MovePlayer(s);
        SceneManager.sceneLoaded-=OnSceneLoaded;
    }

    void MovePlayer(Scene s){
        Transform xro=FindObjectOfType<XROrigin>()?.transform.root;
        SceneManager.SetActiveScene(s);
        print(SceneManager.GetActiveScene().name);
        GameObject targetObj=GameObject.Find(target);
        if(mode==LoadSceneMode.Additive){
            SceneManager.MoveGameObjectToScene(xro.gameObject, SceneManager.GetActiveScene());
            xro.GetComponentInChildren<XROrigin>().transform.position=targetObj.transform.position;
            xro.GetComponentInChildren<SafeBounds>().ResetPosition=targetObj.transform.position;
        }
        
    }
    public void ChangeDay(){
        if(TaskSystem.instance.TaskCompleted==DaySystem.TaskForTheDay){
            TaskSystem.instance.ResetTasks();
            DaySystem.DayNumber++;
        }
    }

    public void ResetDay(){
        
        TaskSystem.instance.ResetTasks();
    }
}
