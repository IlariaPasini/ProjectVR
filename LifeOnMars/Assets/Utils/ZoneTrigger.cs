using System.Collections;
using System.Collections.Generic;
using Unity.XR.CoreUtils;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.XR.Interaction.Toolkit;

public class ZoneTrigger : MonoBehaviour
{
    [SerializeField]
    private UnityEvent enterEvents, exitEvents;
    // Start is called before the first frame update
    void OnTriggerEnter(Collider other){
        if(other.GetComponent<XROrigin>()){
            enterEvents.Invoke();
        }   
    }

    void OnTriggerExit(Collider other){
        if(other.GetComponent<XROrigin>()){
            exitEvents.Invoke();
        }   
    }

    
}
