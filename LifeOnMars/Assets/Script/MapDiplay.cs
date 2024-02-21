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
    public void Start(){
        SceneManager.activeSceneChanged+=(_,_)=>{
            map=FindObjectOfType<PositionToMap>().transform.root;
            enabled=map!=null;
        };
        enabled=map!=null;
    }

    public void OnSelect(SelectEnterEventArgs args)
    {
        // on select get current ray interactor
        if (args.interactorObject is XRRayInteractor)
        {
            ray_interactor = args.interactorObject as XRRayInteractor;
            GetComponent<MeshRenderer>().enabled = false;
            buttonPivot.gameObject.SetActive(true);
        }
    }

    public void OnDeselect(SelectExitEventArgs args)
    {
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

        // remove last ray interactor
        ray_interactor = null;
    }
}
