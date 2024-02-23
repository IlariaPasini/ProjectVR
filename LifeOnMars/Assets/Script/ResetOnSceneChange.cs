using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ResetOnSceneChange : MonoBehaviour
{
    // Start is called before the first frame update
    Vector3 position;
    Quaternion rotation;
    void Start()
    {
        position=transform.position;
        rotation=transform.rotation;

        SceneManager.activeSceneChanged+=Reset;
    }

    void OnDestroy(){
        SceneManager.activeSceneChanged-=Reset;
    }

    // Update is called once per frame
    void Reset(Scene s, Scene s2)
    {
        transform.position=position;
        transform.rotation=rotation;
    }
}
