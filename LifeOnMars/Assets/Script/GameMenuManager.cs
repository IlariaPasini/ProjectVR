using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.InputSystem;
using UnityEngine.UI;
//using UnityEngine.Rendering;

public class GameMenuManager : MonoBehaviour
{
    [SerializeField] private GameObject menu;
    [SerializeField] private InputActionProperty showButton;
    [SerializeField]
    AudioMixerGroup group;
    [SerializeField]
    string volumeName="Volume";
    //voglio che la UI sia sempre ancorata al braccio del giocatore
    [SerializeField] private Transform bracelet;
    [SerializeField] private float spawnDistance = 0.005f;

    // Start is called before the first frame update
    void Start()
    {
        //faccio sì che l'oggetto sia disattivato all'avvio del gioco
        menu.SetActive(false);
        showButton.action.started+=Toggle;
    }
    public void OnDestroy(){
        showButton.action.started-=Toggle;
    }   

    public void Toggle(InputAction.CallbackContext context){
        menu.SetActive(!menu.activeSelf);
    }


    public void AudioSetting(float value){
        group.audioMixer.SetFloat(volumeName,value);
    }

    public void Quit(){
        #if UNITY_EDITOR
        if(Application.isEditor){
            EditorApplication.ExitPlaymode();
        }else
        #endif
        Application.Quit();
    }

    public void ChangeAlpha(float value){
        Image i=GetComponentInChildren<Image>(true);
        if(i==null)
            return;
        Color c=i.color;
        c.a=value;
        i.color=c;

    }
}
