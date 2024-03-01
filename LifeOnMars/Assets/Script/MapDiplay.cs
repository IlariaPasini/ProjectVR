using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.XR.Interaction.Toolkit;

public class MapDiplay : MonoBehaviour
{
    [SerializeField] private Transform buttonPivot;
    [SerializeField] private Transform map;
    [SerializeField] private Transform player;
    [SerializeField] private GameObject hologram;

    float spawnDistance;

    // default transform
    bool thrown = false;
    private XRGrabInteractable grab;
    private MeshRenderer mr;

    private RaycastHit? hit;                    // raycast hit struct
    private XRRayInteractor ray_interactor;     // ray interactor
    // lanciare un raycast dal ray interactor
    // vedere se l'oggetto � la mappa
    // spostare l'oggetto sulla mano
    // al rilascio spostare l'oggetto
    private bool mapEnabled;

    void findMap()
    {
        map = FindObjectOfType<PositionToMap>()?.transform.root;
        mapEnabled = map != null;
        if (mapEnabled)
        {
            map.GetComponent<XRSimpleInteractable>().selectExited.AddListener(onMapEnabled);
            grab.enabled = true;
        }
        else
        {
            grab.enabled = false;
        }
    }

    /*public void Start(){
        mr = GetComponent<MeshRenderer>();
        grab = GetComponent<XRGrabInteractable>();

        SceneManager.activeSceneChanged+=(_,_)=>{
            findMap();
        };

        findMap();
        ResetMapPosition();
    }*/

    public void Start(){
        mr = GetComponent<MeshRenderer>();
        grab = GetComponent<XRGrabInteractable>();

        SceneManager.activeSceneChanged+=findMap;

        findMap();
        ResetMapPosition();
    }

    void findMap(Scene s1, Scene s2){
        findMap();
    }

     void OnDestroy(){
        SceneManager.activeSceneChanged-=findMap;
    }

    private void Update()
    {
        if (!mapEnabled) return;
        if (map.gameObject.active)
        {
            if ((Vector3.Distance(player.position, map.position) - spawnDistance > 2f))
            {
                map.gameObject.SetActive(false);
                onMapEnabled(new());
            }
            else
            {
                spawnDistance = Mathf.Max(1.0f, Mathf.Min(Vector3.Distance(player.position, map.position), spawnDistance));
            }
        }
    }

    public void OnSelect(SelectEnterEventArgs args)
    {
        hologram.SetActive(false);
    }

    public void OnDeselect(SelectExitEventArgs args)
    {
        GetComponent<Rigidbody>().isKinematic = false;
        thrown = true;
    }

    public void onMapEnabled(SelectExitEventArgs args)
    {
        // disable mr
        mr.enabled = true;
        // disable grabbable
        grab.enabled = true;

        hologram.SetActive(true);

        spawnDistance = 0;
    }

    public void ResetMapPosition()
    {
        // reset cosino sul bracciale
        GetComponent<Rigidbody>().isKinematic = true;
        transform.localPosition = Vector3.zero;
        transform.localRotation = Quaternion.identity;

        thrown = false;
    }

    public void hide()
    {
        // disable mr
        mr.enabled = false;
        // disable grabbable
        grab.enabled = false;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (!thrown) return;
        if (!mapEnabled)
        {
            ResetMapPosition();
        }
        else
        {
            // display map on collision point
            map.position = collision.GetContact(0).point;
            map.gameObject.SetActive(true);

            // set spawn distance
            spawnDistance = Vector3.Distance(player.position, map.position);

            // reset map chip
            ResetMapPosition();
            hide();
        }
    }
}
