using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Wand : Weapon
{
    public Transform ProjectileEnjectSocket;
    public Transform MuzzleSocket;

    private GameObject _muzzle;

    private bool isCastActive = false;

    [SerializeField] private SpellBasic _selectedSpell;
    public SpellBasic SelectedSpell
    {
        get { return _selectedSpell; }
        set { _selectedSpell = value; }
    }

    private void DisplayMizzle(bool isAbort)
    {
        if (isAbort && _muzzle != null)
        {
            DestroyImmediate(_muzzle);
            return;
        }

        _muzzle = Instantiate(SelectedSpell.spellShape.Mizzle, MuzzleSocket);
    }

    public override void ExecutePrimaryAction()
    {
        DisplayMizzle(isAbort: isCastActive);
        isCastActive = !isCastActive;
        Debug.Log("Волшебная палочка выполняет основное действие");
    }

    public override void ExecuteSecondaryAction()
    {
        DisplayMizzle(isAbort: true);
        Debug.Log("Волшебная палочка выполняет второстепенное действие");
    }
}
