using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Text;
using UnityEngine;
using UnityEngine.Events;

public class Waterable : MonoBehaviour
{
    // Start is called before the first frame update
    bool watered=false;
    int day_watered=-1;
    public UnityEvent waterableCallback;
    public UnityEvent onNewDay;
    public void Start(){
        //Load();
        DontDestroyOnLoad(gameObject);
    }
    public void Water()
    {
        if(watered)
            return;

        watered=true;
        day_watered=DaySystem.day_number;


        waterableCallback.Invoke();


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
