using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor.Build.Reporting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Fader : MonoBehaviour
{
    // Start is called before the first frame update
    Animator anim;
    ChangeScene changeScene;

    public static Fader instance;
    private static int nextScene=0;
    void Start()
    {
        if(instance==null || instance.IsDestroyed()){
            instance=this;
        }else{
            Destroy(gameObject);
        }
        anim=GetComponent<Animator>();
        changeScene=GetComponent<ChangeScene>();
        SceneManager.sceneLoaded += FadeIn;

        DaySystem.onDayChange+=(_)=>{anim.Play("FadeIn");};
    }

    public void FadeIn(Scene s, LoadSceneMode ls){
        anim.Play("FadeIn");
    }

    void OnDestroy(){
        SceneManager.sceneLoaded-=FadeIn;
    }
    public static void GotoScene(int id){
        nextScene=id;
        instance.anim.Play("FadeOut");
    }

    public static void PlayFadeIn(){
        instance.anim.Play("FadeIn");
    }
    public static void GotoScene(int id, ChangeScene caller){
        nextScene=id;
        instance.anim.Play("FadeOut");
        instance.changeScene=caller;
    }

    public static void NextDay(){
        instance.anim.Play("FadeOutDay");
    }

    public void ChangeSceneOnFade(){
        changeScene.GotoScene(nextScene);
    }

    public void ChangeDayOnFade(){
        DaySystem.DayNumber++;
    }
}
