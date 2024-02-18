using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

[Serializable]
public class SavableObject : MonoBehaviour
{
    [SerializeField]
    Vector3 position,scale;
    [SerializeField]
    Quaternion rotation;
    [SerializeField]
    bool isStatic=false, active=true;
    // Start is called before the first frame update

    void Start()
    {
        Load();
        //DontDestroyOnLoad(gameObject);

        
    }

    public void MakeStatic(){
        Rigidbody rb;
        XRBaseInteractable interactable;

        if(TryGetComponent(out rb)){
            rb.isKinematic=true;
        }
        if(TryGetComponent(out interactable)){
            interactable.enabled=false;
        }
    }

    public bool IsStatic(){
        Rigidbody rb;
        XRBaseInteractable interactable;

        if(TryGetComponent(out rb)){
            return rb.isKinematic;
        }
        if(TryGetComponent(out interactable)){
            return interactable.enabled;
        }
        return true;
    }

    public void Save(){
        //BinaryFormatter formatter=new BinaryFormatter();
        position=transform.position;
        rotation=transform.rotation;
        scale=transform.localScale;
        FileStream fs=new FileStream(Application.persistentDataPath+"/"+name+".savedobj", FileMode.Create);

        string json=JsonUtility.ToJson(this);
        byte[] byteArray=Encoding.UTF8.GetBytes(json);
        fs.Write(byteArray,0, byteArray.Length);

        isStatic=IsStatic();
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

            if(!active){
                gameObject.SetActive(false);
            }
            if(isStatic){
                MakeStatic();
            }
            transform.position=position;
            transform.rotation=rotation;
            transform.localScale=scale;
        }
        
    }
}
