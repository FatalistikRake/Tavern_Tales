using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName="Item", menuName = "Scriptable Objects/Item")]

public class hotbar : ScriptableObject
{


    [Header("Properties")]
    public float cooldown;
    public itemType item_type;
    public Sprite item_sprite;
    
 
}

public enum itemType {boccale};
