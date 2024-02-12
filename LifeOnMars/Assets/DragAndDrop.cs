using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class DragAndDrop : MonoBehaviour
{
    // Start is called before the first frame update
    GameObject last_selected=null;
    void Start()
    {
        
    }

    // Update is called once per frame

    public void TryStore()
    {   
        RaycastHit? hit;
        XRRayInteractor r=GetComponent<XRRayInteractor>();
        r.TryGetCurrentRaycast(out hit,out _,out _,out _,out _);

        
        if(r.interactablesSelected.Count>0){
            GameObject selected=r.interactablesSelected[0].transform.gameObject;
            if(hit.HasValue){
                Rover rover;
                print(hit.Value.transform.name);
                if(hit.Value.transform.TryGetComponent<Rover>(out rover)){
                    rover.Store(selected);
                }
            }
        }
        last_selected=null;
        
    }
}
