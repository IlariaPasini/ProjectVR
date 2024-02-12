using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEditor.PackageManager;
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

    [SerializeField] UnityEvent events;
    [SerializeField] bool interruptable=true;
    bool talking=false;
    int counter=0, size=0;
    //[SerializeField] string text;
    void Start()
    {
            tmp=GetComponentInChildren<TextMeshProUGUI>();
        if(tmp==null)
            throw new DialogueSystemException("No TextMesh");
        
        if(dialogues==null)
            enabled=false;
        size=dialogues.texts.Length;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Talk(){
        if(!interruptable && talking)
            return;

        StopAllCoroutines();
        tmp.text="";
        StartCoroutine(talk());
        counter++;
        if(counter>=size){
            events.Invoke();
        }
        counter%=size;
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

        talking=false;
        
    }
}
