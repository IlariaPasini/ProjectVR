using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class StorableData
{
    [SerializeField]
    public string item_name="Default";
    [SerializeField]
    public bool given=false;
    [SerializeField]
    public bool used=false;
    [SerializeField]
    public bool in_rover=false;

    [SerializeField]
    public Vector3 position, scale;
    [SerializeField]
    public Quaternion rotation;
}
