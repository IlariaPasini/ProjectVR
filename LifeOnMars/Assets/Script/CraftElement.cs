using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//creazione diretta dal menu
[CreateAssetMenu(fileName = "New Craft", menuName = "ScriptableObjects/Craft", order =2)]
public class CraftElement : ScriptableObject
{

   [SerializeField]public string secondElement;
   [SerializeField]public GameObject createdElement;
}
