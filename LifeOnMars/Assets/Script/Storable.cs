using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Text;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class Storable : MonoBehaviour
{
    // Start is called before the first frame update
    RaycastHit? hit;
    XRGrabInteractable grab;
    XRRayInteractor ray_interactor;
    StorableData data=new StorableData();
    [SerializeField]
    string item_name="Default";
    [SerializeField]
    float scaleFactor=0.01f, speed=1;
    public string ItemName { get => item_name;}
    Vector3 origScale;

    void Start()
    {
        data.item_name=item_name;
        grab=GetComponent<XRGrabInteractable>();

        grab.selectEntered.AddListener(OnSelect);
        grab.selectExited.AddListener(OnDeselect);
        origScale=transform.localScale;
    }

    public void OnSelect(SelectEnterEventArgs args){
        Rigidbody rb=GetComponent<Rigidbody>();
        if(rb!=null){
            rb.isKinematic=false;
        }

        if(args.interactorObject is XRRayInteractor){
            ray_interactor=args.interactorObject as XRRayInteractor;
        }
    }


    public void OnDeselect(SelectExitEventArgs args)
    {
        ray_interactor=args.interactorObject.transform.parent.GetComponentInChildren<XRRayInteractor>();
        ray_interactor.TryGetCurrentRaycast(out hit,out _,out _,out _,out _);
        if(hit.HasValue){
            Receiver receiver;
            print(hit.Value.transform.name);
            if(hit.Value.transform.TryGetComponent<Receiver>(out receiver) && receiver.CanReceive(this)){
                grab.enabled=false;
                GetComponent<Collider>().enabled=false;
                Rigidbody rb=GetComponent<Rigidbody>();
                rb.isKinematic=true;
                rb.useGravity=false;
                StartCoroutine(delayedStore(receiver));
            }
        }
    }

    public void Save(){
        //BinaryFormatter formatter=new BinaryFormatter();
        data.position=transform.position;
        data.rotation=transform.rotation;
        data.scale=transform.localScale;

        FileStream fs=new FileStream(Application.persistentDataPath+"/"+name+".savedobj", FileMode.Create);

        string json=JsonUtility.ToJson(data);
        byte[] byteArray=Encoding.UTF8.GetBytes(json);
        fs.Write(byteArray,0, byteArray.Length);

        
        fs.Close();
    }

    IEnumerator delayedStore(Receiver receiver){
        
        while(transform.localScale.x>0 && Vector3.Distance(transform.position, receiver.transform.position)>0.05f){

            transform.localScale-=Vector3.one*scaleFactor*Time.deltaTime;
            
            transform.position=Vector3.MoveTowards(transform.position, receiver.transform.position, Time.deltaTime*speed);
            yield return new WaitForEndOfFrame();
        }
        transform.localScale=origScale;
        grab.enabled=true;
        GetComponent<Collider>().enabled=true;
        Rigidbody rb=GetComponent<Rigidbody>();
        rb.isKinematic=false;
        rb.useGravity=true;
        receiver.Receive(gameObject);

    }


}
