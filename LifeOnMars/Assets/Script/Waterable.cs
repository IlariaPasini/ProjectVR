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

    WaterableData data=new WaterableData();
    public UnityEvent waterableCallback;
    public UnityEvent<int> onNewDay;
    [SerializeField]
    string taskName;
    [SerializeField]
    List<GameObject> phases; 
    public void Start(){
        //Load();

        
        UpdateState(DaySystem.DayNumber);
        DaySystem.onDayChange+=UpdateState;
    }
    
    public void UpdateState(int day){
        if(data.watered){
            int day_since_water=Mathf.Clamp(day-data.day_watered,0,phases.Count-1);
            foreach(GameObject t in phases){
                t.SetActive(false);
            }
            if(day_since_water>=0)
                phases[day_since_water].SetActive(true);
        }
        else{
            phases[0].SetActive(true);
        }
    }

    public void OnDestroy(){
        DaySystem.onDayChange-=UpdateState;
    }
    public void Water()
    {
        if(data.watered)
            return;

        data.watered=true;
        data.day_watered=DaySystem.DayNumber;


        waterableCallback.Invoke();
        TaskSystem.instance.UpdateTask(taskName, 1);
        //Save();

    }

    public void Save(){
        //BinaryFormatter formatter=new BinaryFormatter();
        FileStream fs=new FileStream(Application.persistentDataPath+"/"+name+".savedobj", FileMode.Create);

        string json=JsonUtility.ToJson(data);
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
            JsonUtility.FromJsonOverwrite(json, data);
            //Instantiate(formatter.Deserialize(fs) as GameObject);  
            //Destroy(gameObject);  
            fs.Close();
        }
        
    }
}
