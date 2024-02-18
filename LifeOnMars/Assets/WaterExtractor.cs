using System;
using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using Mono.Cecil.Mdb;
using UnityEngine;

public class WaterExtractor : Receiver
{
    [SerializeField]
    Material temp;
    Material[] starting;
    public static Action<bool> onEnable;
    MeshRenderer mr;
    [SerializeField]
    string task_name;
    [SerializeField]
    string expected_storable;
    // Start is called before the first frame update
    void Start()
    {
        mr=GetComponent<MeshRenderer>();
        ChangeMaterial();
        onEnable+=(b)=>GetComponent<MeshRenderer>().enabled=b;
    }

    void ChangeMaterial(){
        starting=mr.materials;
        Material[] newMaterials=mr.materials;
        for(int i=0;i<newMaterials.Length;i++){
            newMaterials[i]=temp;
        }
        mr.materials=newMaterials;
        mr.enabled=false;
    }

    public static void EnableHologram(bool e){
        onEnable?.Invoke(e);
    }
    // Update is called once per frame
    public override bool CanReceive(Storable s)
    {
        return s.ItemName==expected_storable;
    }
    public override void Receive(GameObject g)
    {
            mr.enabled=true;
            mr.materials=starting;
            g.SetActive(false);
            TaskSystem.instance.UpdateTask(task_name,1);
            enabled=false;
        
    }
}
