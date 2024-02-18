using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class PersistentDataManager : MonoBehaviour
{
    // Start is called before the first frame update
    PersistentDataManager instance;

    GameObject[] toSerialize;
    void Start()
    {
        DontDestroyOnLoad(gameObject);
        if(instance==null){
            instance=this;
        }else{
            Destroy(gameObject);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
