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
[RequireComponent(typeof(Outline))]
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

    public static Action<string> onOutlineEnable, onOutlineDisable;
    
    protected void Start()
    {
        ds=GetComponentInChildren<DialogueSystem>(true);

        // outline callback
        onOutlineEnable += enableOutline;
        onOutlineDisable += disableOutline;
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

    protected override void enableOutline(string s)
    {
        GetComponent<Outline>().enabled = true;
    }

    protected override void disableOutline(string s)
    {
        GetComponent<Outline>().enabled = false;
    }
}
