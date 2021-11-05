using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : MonoBehaviour
{
    public enum ItemTypeState
    {
        None,
        Battery,
    }

    public int typeNum = 0;

    private ItemTypeState itemType;

    void Start()
    {
        itemType = (ItemTypeState)typeNum;
    }

    public int GetTypeInt()
    {
        return typeNum;
    }
}
