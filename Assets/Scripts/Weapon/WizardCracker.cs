using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class WizardCracker : Weapon
{
    public override void ExecutePrimaryAction()
    {
        Debug.Log("�������� �������� � �������� ��������.");
    }

    public override void ExecuteSecondaryAction()
    {
        Debug.Log("�������� �������� � ������ ������ ��� ������������.");
    }
}
