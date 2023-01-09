using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EntityData : ScriptableObject
{
    [SerializeField]
    private string entity_name_;
    public string entity_name { get => entity_name_; }

    [SerializeField]
    private int index_no_;
    public int index_no { get => index_no_; }
}
