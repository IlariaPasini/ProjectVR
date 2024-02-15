using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class EnableDisableMenuPanels : MonoBehaviour
{
    [SerializeField] private GameObject taskMenu;
    [SerializeField] private bool isTaskMenuEnabled = false;
    
    public void ButtonClicked(){
        isTaskMenuEnabled = !isTaskMenuEnabled;
        taskMenu.SetActive(isTaskMenuEnabled);
    }
    
    // Start is called before the first frame update
    /*void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }*/
}
