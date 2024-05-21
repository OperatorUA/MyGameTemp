using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New item", menuName = "Items/Resources/Default resource")]
public class ItemData : EntityData
{
    public GameObject prefab;
    public string id;
    public string description;
    public Sprite icon;
    public int count;
}
