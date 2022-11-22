using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Wand : Weapon
{
    public Transform ProjectileEnjectSocket;
    public Transform MuzzleSocket;

    public GameObject Muzzle;

    [SerializeField] private Character _character;

    private void Awake()
    {
        _character = GetComponentInParent <Character> ();
    }



    public override void ExecutePrimaryAction()
    {
        _character.SelectedSpell.TryCastSpell(MuzzleSocket, ref Muzzle, ProjectileEnjectSocket);
        Debug.Log("Волшебная палочка выполняет основное действие");
    }

    public override void ExecuteSecondaryAction()
    {
        _character.SelectedSpell.TryCastSpellAlt(MuzzleSocket, ref Muzzle, ProjectileEnjectSocket);
        Debug.Log("Волшебная палочка выполняет второстепенное действие");
    }
}
