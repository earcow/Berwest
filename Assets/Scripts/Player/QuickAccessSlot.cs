using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class QuickAccessSlot
{
    public QuickAccessSlot(GameObject content)
    {
        this.Content = content;
    }
    public GameObject Content;

}
