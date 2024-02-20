using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using System;
using UnityEngine.Events;

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

public class DialogueSystem : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] private float speed=.1f;
    TextMeshProUGUI tmp;
    [SerializeField] Dialogue dialogues;
    Dialogue defDialogue;
    [SerializeField] UnityEvent onStartTalking;
    [SerializeField] UnityEvent events;
    UnityEvent defEvents, defStartEvents=new UnityEvent();
    [SerializeField] bool interruptable=true;
    bool talking=false;
    int counter=0, size=0;
    //[SerializeField] string text;
    void Awake(){
        defStartEvents=onStartTalking;
        defDialogue=dialogues;
        defEvents=events;
    }
    void Start()
    {
        tmp=GetComponentInChildren<TextMeshProUGUI>();
        if(tmp==null)
            throw new DialogueSystemException("No TextMesh");
        
        if(dialogues==null)
            enabled=false;
        size=dialogues.texts.Length;

        
        //events.AddListener(()=>{dialogues=defDialogue; events=defEvents;});
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SetDialogueTemp(Dialogue dialogue){
        dialogues=dialogue;
        counter=0;
        size=dialogue.texts.Length;
        UnityEvent tempEvents=new UnityEvent();
        tempEvents.AddListener(ResetDialogue);
        events=tempEvents;
    }
    public void SetDialogueTemp(Dialogue dialogue, UnityEvent newEvents){
        dialogues=dialogue;
        counter=0;
        size=dialogue.texts.Length;
        UnityEvent tempEvents=newEvents;
        tempEvents.AddListener(ResetDialogue);
        events=tempEvents;
    }
    public void SetDialogueTemp(Dialogue dialogue, UnityEvent newEvents, UnityEvent startEvents){
        dialogues=dialogue;
        counter=0;
        size=dialogue.texts.Length;
        onStartTalking=startEvents;
        UnityEvent tempEvents=newEvents;
        tempEvents.AddListener(ResetDialogue);
        events=tempEvents;
    }

    public void ResetDialogue(){
        dialogues=defDialogue; 
        counter=0;
        size=defDialogue.texts.Length;
        events=defEvents;
        onStartTalking=defStartEvents;
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

    

    public void Talk(){
        if(!interruptable && talking)
            return;
        counter%=size;
        if(counter==size)
            events.Invoke();
        StopAllCoroutines();
        tmp.text="";
        onStartTalking.Invoke();
        StartCoroutine(talk());
        counter++;
        
    }

    private IEnumerator talk(){
        CharEnumerator e=dialogues.texts[counter].GetEnumerator();
        talking=true;
        while(e.MoveNext()){
            char c=e.Current;
            tmp.text+=c;

            yield return new WaitForSeconds(speed);
            if(tmp.isTextOverflowing){
                tmp.text="";
                tmp.text+=c;
            }

        }


        yield return new WaitForSeconds(1.0f);
        tmp.text="";
        events.Invoke();
        counter%=size;

        talking=false;

        
    }
}
