using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameConfigurator : MonoBehaviour
{
    // holds lock values to manage the Windows cursor
    [SerializeField] CursorLockMode lockMode;

    void Awake()
    {
        lockMode = CursorLockMode.Locked;
        Cursor.lockState = lockMode;
        
    }

    private void OnValidate()
    {
        Cursor.lockState = lockMode;
    }
}
