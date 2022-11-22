using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spell_Lumos : SpellBasic
{
    protected override void InitializeSpell() {}


    public override bool TryCastSpell(Transform MuzzleSocket, ref GameObject Muzzle, Transform ProjectileSocket)
    {
        base.HideMuzzle(ref Muzzle);
        Muzzle = base.GetMuzzle(MuzzleSocket);

        return true;
    }

    public override bool TryCastSpellAlt(Transform MuzzleSocket, ref GameObject Muzzle, Transform ProjectileSocket)
    {
        base.HideMuzzle(ref Muzzle);
        return true;
    }
}
