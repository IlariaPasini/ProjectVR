using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Item", menuName = "ScriptableObjects/Item", order = 2)]
public class ScriptableItem : ScriptableObject
{
    public string ItemName;
    public GameObject prefab;

    public static bool operator==(ScriptableItem A, ScriptableItem B){
        return A.ItemName==B.ItemName;

    }

    public static bool operator!=(ScriptableItem A, ScriptableItem B){
        return A.ItemName!=B.ItemName;
    }
}
