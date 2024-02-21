using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.XR.Interaction.Toolkit;

public class MapDiplay : MonoBehaviour
{
    [SerializeField] private Transform buttonPivot;
    [SerializeField] private Transform map;

    private RaycastHit? hit;                    // raycast hit struct
    private XRRayInteractor ray_interactor;     // ray interactor
    // lanciare un raycast dal ray interactor
    // vedere se l'oggetto � la mappa
    // spostare l'oggetto sulla mano
    // al rilascio spostare l'oggetto
    private bool mapEnabled;
    public void Start(){
        map = FindObjectOfType<PositionToMap>()?.transform.root;
        mapEnabled = map != null;
        SceneManager.activeSceneChanged+=(_,_)=>{
            map=FindObjectOfType<PositionToMap>()?.transform.root;
            mapEnabled = map!=null;
        };
    }

    public void OnSelect(SelectEnterEventArgs args)
    {
        if (!mapEnabled) return;
        ray_interactor=args.interactorObject.transform.parent.GetComponentInChildren<XRRayInteractor>();
        // on select get current ray interactor
        ray_interactor = args.interactorObject as XRRayInteractor;
        GetComponent<MeshRenderer>().enabled = false;
        buttonPivot.gameObject.SetActive(true);
        
    }

    public void OnDeselect(SelectExitEventArgs args)
    {
        if (!mapEnabled) return;
        ray_interactor=args.interactorObject.transform.parent.GetComponentInChildren<XRRayInteractor>();
        // try get current raycast from current ray interactor
        ray_interactor.TryGetCurrentRaycast(out hit, out _, out _, out _, out _);
        if (hit.HasValue)
        {
            buttonPivot.gameObject.SetActive(false);
            GetComponent<MeshRenderer>().enabled = true;
            map.position = hit.Value.point;
            map.gameObject.SetActive(true);
        }
        else{
            buttonPivot.gameObject.SetActive(false);
            GetComponent<MeshRenderer>().enabled = true;
            map.position = Vector3.down*100;
            map.gameObject.SetActive(false);
        }

        // remove last ray interactor
        ray_interactor = null;
    }
}
