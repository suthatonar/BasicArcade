using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class item : MonoBehaviour
{
    public ItemStatus itemStatus;
    public ObjectType type;
    public itemType item_Type;


    public enum itemType
    {
        Banana,Grape,Orange
    }

    public enum ObjectType
    {
        Item
    }

    public enum ItemStatus
    {
        None, Grabbed,InBucket
    }
}
