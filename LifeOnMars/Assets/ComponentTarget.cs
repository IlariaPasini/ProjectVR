using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using Unity.VisualScripting;
using Unity.XR.CoreUtils;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.XR.Interaction.Toolkit;

[Serializable]
public class ComponentTarget : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField]
    Material emptyMaterial;
    MeshRenderer mr;
    Material originalMat;
    [SerializeField]
    bool with_object=true;
    [SerializeField]
    GameObject requiredComponent;
    [SerializeField]
    string expected_name;
    [SerializeField]
    string task_to_update;

    public UnityEvent onComplete;
    [SerializeField]
    bool done=false;
    void Start()
    {
        //Load();
        mr=GetComponent<MeshRenderer>();
        originalMat=mr.material;
        
        Material[] newMaterials=new Material[mr.materials.Length];

        for(int i=0;i<mr.materials.Length;i++){
            newMaterials[i]=emptyMaterial;
        }
        mr.materials=newMaterials;
    }

    public void Awake(){
        Load();
    }
    public void Save(){
        BinaryFormatter formatter=new BinaryFormatter();
        FileStream fs=new FileStream(Application.persistentDataPath+"/"+name+".savedobj", FileMode.Create);

        string json=JsonUtility.ToJson(this);
        byte[] byteArray=Encoding.UTF8.GetBytes(json);
        fs.Write(byteArray,0, byteArray.Length);

        fs.Close();
    }

    public void Load(){
       
        string path=Application.persistentDataPath+"/"+name+".savedobj";
        print(path);
        if(File.Exists(path)){
            //BinaryFormatter formatter=new BinaryFormatter();
            FileStream fs=new FileStream(path, FileMode.Open);
            byte[] byteArray=new byte[1024];
            fs.Read(byteArray,0,1024);
            string json=Encoding.UTF8.GetString(byteArray);
            JsonUtility.FromJsonOverwrite(json, this);
            //Instantiate(formatter.Deserialize(fs) as GameObject);  
            //Destroy(gameObject);  
            fs.Close();

            if(done){
                gameObject.SetActive(false);
            }
        }
        
    }
    
    void OnTriggerEnter(Collider other)
    {   
        if(with_object && other.gameObject==requiredComponent) {
            //other.gameObject.SetActive(false);
            

            other.GetComponent<XRGrabInteractable>().enabled=false;
            other.GetComponent<Rigidbody>().isKinematic=true;
            other.transform.position=transform.position;
            other.transform.rotation=transform.rotation;
            other.transform.localScale=transform.localScale;
            
            onComplete.Invoke();
            TaskSystem.instance.UpdateTask(task_to_update,1);
            done=true;

            SavableObject savable;
            if(other.TryGetComponent<SavableObject>(out savable)){
                savable.Save();
            }
            Save();
            gameObject.SetActive(false);
        }

        Storable s;
        if(!with_object && other.TryGetComponent<Storable>(out s) && s.ItemName==expected_name){

            s.enabled=false;
            other.GetComponent<XRGrabInteractable>().enabled=false;

            other.GetComponent<Rigidbody>().isKinematic=true;

            other.transform.position=transform.position;
            other.transform.rotation=transform.rotation;
            other.transform.localScale=transform.localScale;
            
            onComplete.Invoke();
            TaskSystem.instance.UpdateTask(task_to_update,1);
            done=true;
            SavableObject savable;
            if(other.TryGetComponent<SavableObject>(out savable)){
                savable.Save();
            }
            Save();
            gameObject.SetActive(false);
            
        }
    }
}
