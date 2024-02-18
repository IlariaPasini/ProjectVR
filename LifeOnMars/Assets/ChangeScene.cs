using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ChangeScene : MonoBehaviour
{
    // Start is called before the first frame update
    public void GotoScene(int id){
        SceneManager.LoadScene(id);
    }

    public void ChangeDay(){
        DaySystem.day_number++;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
