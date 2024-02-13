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

    Stack<GameObject> stored_objects=new Stack<GameObject>();

    [SerializeField]Transform anchor;
    void Start()
    {
        
    }

    // Update is called once per frame
    public override void Receive(GameObject g){
        stored_objects.Push(g);
        g.SetActive(false);
    }

    public void Pop(){
        GameObject popped;
        if(stored_objects.TryPop(out popped)){
            popped.transform.SetParent(anchor);
            popped.SetActive(true);
            popped.GetComponent<Rigidbody>().isKinematic=true;
            popped.transform.localPosition=Vector3.zero;
            popped.GetComponent<Rigidbody>().isKinematic=false;
            //.position=Vector3.zero;
            
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
