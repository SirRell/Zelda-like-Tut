using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "New Item", menuName = "Item")]
public class Items : ScriptableObject
{
    public Sprite itemDisplay;
    public string itemName;
    public string itemDescription;
    public bool isKey;

}
