using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class Storable : MonoBehaviour
{
    // Start is called before the first frame update
    RaycastHit? hit;
    XRGrabInteractable grab;
    void Start()
    {
        grab=GetComponent<XRGrabInteractable>();
    }

    void Update(){  
        if(grab.interactorsSelecting.Count>0){
            XRRayInteractor ray_interactor=grab.interactorsSelecting[0] as XRRayInteractor;

            ray_interactor.TryGetCurrentRaycast(out hit,out _,out _,out _,out _);

        }
    }

    public void OnSelect(){
    }

    // Update is called once per frame
    public void OnDeselect()
    {

        if(hit.HasValue){
            Rover rover;
            print(hit.Value.transform.name);
            if(hit.Value.transform.TryGetComponent<Rover>(out rover)){
                
                StartCoroutine(delayedStore(rover));
            }
        }
    }

    IEnumerator delayedStore(Rover rover){
        yield return new WaitForSeconds(0.15f);
        rover.Store(gameObject);
    }
}
