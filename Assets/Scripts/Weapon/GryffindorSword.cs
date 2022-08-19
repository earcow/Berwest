using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class GryffindorSword : Weapon
{


    public override void ExecutePrimaryAction()
    {
        Debug.Log("Меч Грифиндора наносит удар.");
    }

    public override void ExecuteSecondaryAction()
    {
        Debug.Log("Меч Грифиндора парирует атаку.");
    }
}
