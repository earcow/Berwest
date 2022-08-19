using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class WizardCracker : Weapon
{
    public override void ExecutePrimaryAction()
    {
        Debug.Log("Хлопушка кидается с обратным отсчётом.");
    }

    public override void ExecuteSecondaryAction()
    {
        Debug.Log("Хлопушка кидается в режиме взрыва при столкновении.");
    }
}
