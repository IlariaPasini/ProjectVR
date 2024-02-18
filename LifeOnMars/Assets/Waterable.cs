using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Text;
using UnityEngine;
using UnityEngine.Events;

[Serializable]
public class Waterable : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField]
    bool watered=false;
    [SerializeField]
    int day_watered=-1;
    public UnityEvent waterableCallback;
    public UnityEvent<int> onNewDay;

    //List<GameObject> phases; 
    public void Start(){
        Load();

        
        if(watered){
            int day_since_water=Mathf.Clamp(DaySystem.day_number-day_watered,0,transform.childCount-1);
            foreach(Transform t in transform){
                t.gameObject.SetActive(false);
            }
            if(day_since_water>=0)
                transform.GetChild(day_since_water).gameObject.SetActive(true);
        }
        transform.GetChild(0).gameObject.SetActive(true);
    }
    public void Water()
    {
        if(watered)
            return;

        watered=true;
        day_watered=DaySystem.day_number;


        waterableCallback.Invoke();
        Save();

    }

    public void Save(){
        //BinaryFormatter formatter=new BinaryFormatter();
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
        }
        
    }
}
