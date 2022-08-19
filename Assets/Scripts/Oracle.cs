using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Oracle : MonoBehaviour
{
    public static Oracle Instance { get; private set; }

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(this);
        }
        else
        {
            Instance = this;
        }
    }




}
