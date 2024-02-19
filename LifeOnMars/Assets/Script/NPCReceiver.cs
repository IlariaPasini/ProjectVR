using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[Serializable]
public class ItemDialoguePair{
    [SerializeField]
    public string name;
    [SerializeField]
    public Dialogue dialogue;
    [SerializeField]
    public UnityEvent onTalkStart;
    [SerializeField]
    public UnityEvent onTalkEnd;

    public bool take;
} 
public class NPCReceiver : Receiver
{
    // Start is called before the first frame update
    [SerializeField]
    List<ItemDialoguePair> dialogues;
    [SerializeField]
    Dialogue nullDialogue;
    [SerializeField]
    UnityEvent nullDialogueStartEvent;
    public DialogueSystem ds;
    
    protected void Start()
    {
        ds=GetComponentInChildren<DialogueSystem>(true);
    }

    // Update is called once per frame
    public override bool CanReceive(Storable s)
    {
        ItemDialoguePair idp=dialogues.Find((pair)=>pair.name==s.ItemName);
        if(idp!=null){
            ds.SetDialogueTemp(idp.dialogue, idp.onTalkEnd, idp.onTalkStart);
            ds.Talk();
            return idp.take;
        }

        ds.SetDialogueTemp(nullDialogue,new UnityEvent(), nullDialogueStartEvent);
        ds.Talk();
        return false;
    }
}
