using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class RoomRoomMove : MonoBehaviour
{
    // Start is called before the first frame update

    [SerializeField]
    bool smooth=true;
    private Coroutine c;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    public void MoveTowards(Transform target){

        LocomotionProvider[]  providers=GetComponents<LocomotionProvider>();
        print(providers.Length);
        foreach(LocomotionProvider c in providers){
            c.enabled=false;
            print(c.name);
        }
            
        c=StartCoroutine(Move(target, ()=>{
            foreach(LocomotionProvider c in providers){
                if (!(c is ContinuousMoveProviderBase) && !(c is ContinuousTurnProviderBase) )
                    c.enabled=true;
            }
                
        }));
        
    }
    IEnumerator Move(Transform target, Action a){

        while(Vector3.Distance(transform.position,target.position)>0.5f){
            transform.position=Vector3.MoveTowards(transform.position,target.position, 6.0f*Time.deltaTime);
            yield return new WaitForEndOfFrame();
        }
        a();
    }
}
