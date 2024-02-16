using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class MapDiplay : MonoBehaviour
{
    [SerializeField] private Transform pivot;

    private XRRayInteractor ray_interactor;
    // lanciare un raycast dal ray interactor
    // vedere se l'oggetto è la mappa
    // spostare l'oggetto sulla mano
    // al rilascio spostare l'oggetto

    private void Start()
    {
        // get ray interactor from this hand
        ray_interactor = GetComponent<XRRayInteractor>();
    }

    public void OnSelect(SelectEnterEventArgs args)
    {
        print("select\n");
    }
}
