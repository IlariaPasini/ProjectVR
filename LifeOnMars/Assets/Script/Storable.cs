using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class Storable : MonoBehaviour
{
    // Start is called before the first frame update
    RaycastHit? hit;
    XRGrabInteractable grab;
    XRRayInteractor ray_interactor;
    [SerializeField]
    string item_name="Default";

    public string ItemName { get => item_name; set => item_name = value; }

    void Start()
    {
        grab=GetComponent<XRGrabInteractable>();

        grab.selectEntered.AddListener(OnSelect);
        grab.selectExited.AddListener(OnDeselect);
    }

    // void Update(){  
    //     if(grab.interactorsSelecting.Count>0 && grab.interactorsSelecting[0] is XRRayInteractor){
    //         //XRRayInteractor ray_interactor=grab.interactorsSelecting[0] as XRRayInteractor;

    //         ray_interactor.TryGetCurrentRaycast(out hit,out _,out _,out _,out _);

    //     }
    // }

    public void OnSelect(SelectEnterEventArgs args){
        if(args.interactorObject is XRRayInteractor){
            ray_interactor=args.interactorObject as XRRayInteractor;

            //ray_interactor.TryGetCurrentRaycast(out hit,out _,out _,out _,out _);

        }
    }

    // Update is called once per frame
    public void OnDeselect(SelectExitEventArgs args)
    {
        ray_interactor.TryGetCurrentRaycast(out hit,out _,out _,out _,out _);
        if(hit.HasValue){
            Receiver receiver;
            print(hit.Value.transform.name);
            if(hit.Value.transform.TryGetComponent<Receiver>(out receiver)){
                
                StartCoroutine(delayedStore(receiver));
            }
        }
    }

    IEnumerator delayedStore(Receiver receiver){
        yield return new WaitForSeconds(0.15f);
        receiver.Receive(gameObject);
    }
}
