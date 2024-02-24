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

    float spawnDistance;

    // default transform
    private XRGrabInteractable grab;
    private MeshRenderer mr;

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

        mr = GetComponent<MeshRenderer>();
        grab = GetComponent<XRGrabInteractable>();

        map.GetComponent<XRSimpleInteractable>().selectExited.AddListener(onEnableMap);
    }

    private void Update()
    {
        if (!mapEnabled) return;
        if (map)
        {
            if ((Vector3.Distance(player.position, map.position) - spawnDistance > 2f))
            {
                map.gameObject.SetActive(false);
                onEnableMap(new());
            }
            else
            {
                spawnDistance = Mathf.Max(1.0f, Mathf.Min(Vector3.Distance(player.position, map.position), spawnDistance));
            }
        }
    }

    public void OnSelect(SelectEnterEventArgs args)
    {
        GetComponent<Rigidbody>().isKinematic = false;
    }

    public void OnDeselect(SelectExitEventArgs args)
    {
        GetComponent<Rigidbody>().isKinematic = false;
    }

    public void ResetMapPosition()
    {
        // reset cosino sul bracciale
        GetComponent<Rigidbody>().isKinematic = true;
        transform.localPosition = Vector3.zero;
        transform.localRotation = Quaternion.identity;
    }

    public void disable()
    {
        // disable mr
        mr.enabled = false;
        // disable grabbable
        grab.enabled = false;
    }

    public void onEnableMap(SelectExitEventArgs args)
    {
        // disable mr
        mr.enabled = true;
        // disable grabbable
        grab.enabled = true;

        spawnDistance = 0;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (!mapEnabled)
        {
            ResetMapPosition();
        }
        else
        {
            // display map
            map.position = collision.GetContact(0).point;
            map.gameObject.SetActive(true);

            spawnDistance = Vector3.Distance(player.position, map.position);

            ResetMapPosition();
            disable();
        }
    }
}
