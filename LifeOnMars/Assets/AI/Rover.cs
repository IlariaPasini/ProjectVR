using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.XR.Interaction.Toolkit;

public class Rover : Receiver
{
    // Start is called before the first frame update
    [SerializeField] Transform target;

    
    public List<GameObject> stored_objects=new List<GameObject>();

    [SerializeField]Transform anchor;
    [SerializeField] int max_items=5;
    public Action on_inv_update;
    void Start()
    {
        
    }

    // Update is called once per frame
    public override void Receive(GameObject g){
        if(stored_objects.Count<max_items){
            stored_objects.Add(g);
            g.SetActive(false);
            if(on_inv_update!=null){
                on_inv_update();
            }
        }
        
    }

    public void Pop(){
        GameObject popped;
        if(stored_objects.Count>0){

            popped=stored_objects.Last();
            stored_objects.Remove(popped);

            popped.transform.SetParent(anchor);
            popped.SetActive(true);
            popped.GetComponent<Rigidbody>().isKinematic=true;
            popped.transform.localPosition=Vector3.zero;
            popped.GetComponent<Rigidbody>().isKinematic=false;
            //.position=Vector3.zero;
            
        }
    }

    public void Get(int id){
        GameObject popped;
        if(stored_objects.Count>id){
            popped=stored_objects[id];
            stored_objects.RemoveAt(id);

            popped.transform.SetParent(anchor);
            popped.SetActive(true);
            popped.GetComponent<Rigidbody>().isKinematic=true;
            popped.transform.localPosition=Vector3.zero;
            popped.GetComponent<Rigidbody>().isKinematic=false;
            
            if(on_inv_update!=null){
                on_inv_update();
            }
        }
    }
    void Update()
    {
        GetComponent<NavMeshAgent>().SetDestination(target.position);
    }

    public void SetDestination(Transform t){
        GetComponent<NavMeshAgent>().SetDestination(t.position);
    }
}
