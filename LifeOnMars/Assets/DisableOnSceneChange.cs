using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DisableOnSceneChange : MonoBehaviour
{
    // Start is called before the first frame update
    Scene s;
    void Start()
    {
        s=gameObject.scene;   
        SceneManager.sceneLoaded+=DeactivateOnSceneChange;
    }

    // Update is called once per frame
    void DeactivateOnSceneChange(Scene s, LoadSceneMode mode)
    {
        gameObject.SetActive(this.s==s);
    }
}
