using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class TargetMarkerPair{
    
    [SerializeField]
    public Transform target;
    [SerializeField]
    public Transform marker;

    public TargetMarkerPair(Transform target,Transform marker)
    {
        this.target=target;
        this.marker=marker;
    }
}   

public class PositionToMap : MonoBehaviour
{
    static PositionToMap map; 
    [SerializeField]
    private GameObject markerPrefab;
    [SerializeField]
    private Transform playerMarker;
    [SerializeField]
    private List<TargetMarkerPair> pairs;

    [SerializeField] private Transform playerTarget, baseTarget;
    [SerializeField]
    private float worldScale = 11.0f,
        mapScale = 0.1f;

    void Awake(){
        if(map==null){
            map=this;
        }else{
            Destroy(gameObject);
        }
    }
    void Start()
    {
        

        for(int i=0;i<pairs.Count;i++)
        {
            pairs[i].marker.localPosition = new Vector3(pairs[i].target.position.x-baseTarget.position.x, pairs[i].target.position.z-baseTarget.position.z, 0.1f) / worldScale * mapScale;
        }
    }

    public void VisualizeSecondaryMarkers(bool enable){
        foreach(TargetMarkerPair t in pairs){
            t.marker.gameObject.SetActive(enable);
        }
    }

    public static void AddMarker(Transform target){
        if(map==null)
            return;
        Transform marker=Instantiate(map.markerPrefab,map.transform.parent).transform;

        marker.localPosition = new Vector3(target.position.x-map.baseTarget.position.x, target.position.z-map.baseTarget.position.z, 0.1f) / map.worldScale * map.mapScale;
        map.pairs.Add(new TargetMarkerPair(target, marker));

    }

    public static void RemoveMarker(Transform target){
        if(map==null)
            return;
        TargetMarkerPair pair=map.pairs.Find((pair)=>{return pair.target==target;});
        if(pair==null)
            return;
        Destroy(pair.marker.gameObject);
        map.pairs.Remove(pair);
    }
    // Update is called once per frame
    void Update()
    {
        
        playerMarker.localPosition = new Vector3(playerTarget.position.x- baseTarget.position.x, playerTarget.position.z- baseTarget.position.z, 0.1f) / worldScale * mapScale;
        
    }
}
