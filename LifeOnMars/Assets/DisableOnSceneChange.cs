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
        DeactivateOnSceneChange(SceneManager.GetActiveScene(),SceneManager.GetActiveScene());
        SceneManager.activeSceneChanged+=DeactivateOnSceneChange;
    }

    void OnDestroy(){
        SceneManager.activeSceneChanged-=DeactivateOnSceneChange;
    }

    // Update is called once per frame
    void DeactivateOnSceneChange(Scene s,Scene s2)
    {
        gameObject.SetActive(this.s==s2);
    }
}
