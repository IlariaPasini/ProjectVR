using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class ItemDialoguePair{
    [SerializeField]
    public string name;
    [SerializeField]
    public Dialogue dialogue;

    public bool take;
} 
public class NPCReceiver : Receiver
{
    // Start is called before the first frame update
    [SerializeField]
    List<ItemDialoguePair> dialogues;
    [SerializeField]
    Dialogue nullDialogue;
    public DialogueSystem ds;
    protected void Start()
    {
        ds=GetComponentInChildren<DialogueSystem>(true);
    }

    // Update is called once per frame
    public override bool CanReceive(Storable s)
    {
        ItemDialoguePair idp=dialogues.Find((pair)=>pair.name==s.name);
        if(idp!=null){
            ds.SetDialogueTemp(idp.dialogue);
            ds.Talk();
            return idp.take;
        }

        ds.SetDialogueTemp(nullDialogue);
        ds.Talk();
        return false;
    }
}
