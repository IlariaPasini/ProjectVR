using System;
using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
//using Mono.Cecil.Mdb;
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
    Dictionary<Transform, Material> dict=new Dictionary<Transform, Material>();
    // Start is called before the first frame update
    void Start()
    {
        mr=GetComponent<MeshRenderer>();
        ChangeMaterial();
        onEnable+=EnableMaterials;
    }

    void ChangeMaterial(){

        foreach(Transform t in transform){
            MeshRenderer mr_temp=t.GetComponent<MeshRenderer>();
            if(mr_temp!=null){
                dict.Add(t, mr_temp.material);
                mr_temp.material=temp;
                mr_temp.enabled=false;
            }
        }
    }

    void EnableMaterials(bool enable){
        foreach(Transform t in transform){
            MeshRenderer mr_temp=t.GetComponent<MeshRenderer>();
            if(mr_temp!=null){
                mr_temp.enabled=enable;
            }
        }
    }

    void RealizeMaterials(){
        foreach(Transform t in transform){
            MeshRenderer mr_temp=t.GetComponent<MeshRenderer>();
            if(mr_temp!=null){
                mr_temp.material=dict[t];
                mr_temp.enabled=true;
            }
        }
        Markable m;
        if(TryGetComponent<Markable>(out m))
            m.enabled=false;

        onEnable-=EnableMaterials;
        Destroy(this);

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
        RealizeMaterials();
        g.SetActive(false);
        TaskSystem.instance.UpdateTask(task_name,1);
        enabled=false;
        
    }
}
