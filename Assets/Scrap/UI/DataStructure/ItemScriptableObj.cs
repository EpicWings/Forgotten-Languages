using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu]
public class ItemScriptableObj : ScriptableObject
{
    [field: SerializeField]
    public bool IsStackable { get; set; }

    [field: SerializeField]
    public int MaxStack { get; set; } = 1;

    [field: SerializeField]
    public int ID => GetInstanceID();

    [field: SerializeField]
    public string Name { get; set; }

    [field: SerializeField]
    [field: TextArea]
    public string Description { get; set; }

    [field: SerializeField]
    public Sprite Icon { get; set; }

}
