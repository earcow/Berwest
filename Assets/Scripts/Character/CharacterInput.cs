using System.Collections;
using System.Collections.Generic;
using UnityEngine;

abstract public class CharacterInput : MonoBehaviour
{
    public bool JumpFlag;
    public bool SprintFlag;

    public Vector2 Look;
    public Vector2 Move;

    abstract public float GetInputMagnitude(); 




}
