using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using System;
using UnityEngine.Events;
using UnityEngine.UI;

[System.Serializable]
public class DialogueSystemException : System.Exception
{
    public DialogueSystemException() { }
    public DialogueSystemException(string message) : base(message) { }
    public DialogueSystemException(string message, System.Exception inner) : base(message, inner) { }
    protected DialogueSystemException(
        System.Runtime.Serialization.SerializationInfo info,
        System.Runtime.Serialization.StreamingContext context) : base(info, context) { }
}

[RequireComponent(typeof(AudioSource))]
public class DialogueSystem : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] private float speed=.1f, persistance=1.0f;
    TextMeshProUGUI tmp;
    [SerializeField] Dialogue dialogues;
    Dialogue defDialogue;
    [SerializeField] UnityEvent onTalkStart, onTalk, onTalkEnd;
    UnityEvent defEvents, defStartEvents=new UnityEvent();
    [SerializeField] bool interruptable=true, auotmaticTalk=false, withPanel=false;
    [SerializeField] GameObject panel;
    AudioSource audioSource;
    bool talking=false;
    int counter=0, size=0;

    public bool AuotmaticTalk { get => auotmaticTalk; set => auotmaticTalk = value; }

    //[SerializeField] string text;
    void Awake(){
        defStartEvents=onTalk;
        defDialogue=dialogues;
        defEvents=onTalkEnd;
    }
    void Start()
    {
        audioSource=GetComponent<AudioSource>();
        if(panel==null && withPanel){
            panel=transform.parent.GetComponentInChildren<Image>(true)?.gameObject;
            
        }
        if(withPanel){
            panel.SetActive(false);
            onTalkStart.AddListener(()=>panel.SetActive(true));
            onTalkEnd.AddListener(()=>panel.SetActive(false));
        }

        tmp=GetComponentInChildren<TextMeshProUGUI>(true);
        if(tmp==null)
            throw new DialogueSystemException("No TextMesh");
        tmp.text="";

        if(dialogues==null)
            enabled=false;
        size=dialogues.texts.Length;

        
        //events.AddListener(()=>{dialogues=defDialogue; events=defEvents;});
    }


    public void SetDialogueTemp(Dialogue dialogue){
        dialogues=dialogue;
        counter=0;
        size=dialogue.texts.Length;
        UnityEvent tempEvents=new UnityEvent();
        tempEvents.AddListener(ResetDialogue);
        onTalkEnd=tempEvents;
    }
    public void SetDialogueTemp(Dialogue dialogue, UnityEvent newEvents){
        dialogues=dialogue;
        counter=0;
        size=dialogue.texts.Length;
        UnityEvent tempEvents=newEvents;
        tempEvents.AddListener(ResetDialogue);
        onTalkEnd=tempEvents;
    }
    public void SetDialogueTemp(Dialogue dialogue, UnityEvent newEvents, UnityEvent startEvents){
        dialogues=dialogue;
        counter=0;
        size=dialogue.texts.Length;
        onTalk=startEvents;
        UnityEvent tempEvents=newEvents;
        tempEvents.AddListener(ResetDialogue);
        onTalkEnd=tempEvents;
    }

    public void ResetDialogue(){
        StopAllCoroutines();
        dialogues=defDialogue; 
        counter=0;
        size=defDialogue.texts.Length;
        onTalkEnd=defEvents;
        onTalk=defStartEvents;
        if(withPanel){
            panel.SetActive(false);
        }
    }
    public void SetDialoguePermanent(Dialogue dialogue, UnityEvent newEvents){
        defDialogue=dialogue;
        defEvents=newEvents;

        ResetDialogue();
    }
    public void SetDialoguePermanent(Dialogue dialogue, UnityEvent newEvents, UnityEvent startEvents){
        defDialogue=dialogue;
        defEvents=newEvents;
        defStartEvents=startEvents;
        ResetDialogue();
    }

    public void PlayAudio(){
        if(dialogues.audio.Length>counter){
            audioSource.clip=dialogues.audio[counter];
            audioSource.Play();
        }
    }    

    public void Talk(){
        if(!interruptable && talking)
            return;

        if(withPanel)
            panel.SetActive(false);
            
        if(counter==size)
            onTalkEnd.Invoke();
        
        counter%=size+1;
        StopAllCoroutines();

        if(counter==0)
            onTalkStart.Invoke();
        tmp.text="";


        if(counter<size){
            onTalk.Invoke();
            PlayAudio();
            StartCoroutine(talk());
        }else{
            audioSource.Stop();
        }

        counter++;
        print("Counter +1 "+counter);
    }



    private IEnumerator talk(){
        CharEnumerator e=dialogues.texts[counter].GetEnumerator();

        talking=true;
        if (panel)
            panel.SetActive(true);
        while(e.MoveNext()){
            char c=e.Current;
            tmp.text+=c;

            yield return new WaitForSeconds(speed);
            if(tmp.isTextOverflowing){
                tmp.text="";
                tmp.text+=c;
            }

        }


        yield return new WaitForSeconds(persistance);
        tmp.text="";
        if(panel)
            panel.SetActive(false);

        talking=false;

        if(counter==size){
            onTalkEnd.Invoke();
            counter%=size; 
            yield break;
        }
            
        
        else if(auotmaticTalk && counter<size)
            Talk();
        else{
            counter%=size;  
        }

    }
}
