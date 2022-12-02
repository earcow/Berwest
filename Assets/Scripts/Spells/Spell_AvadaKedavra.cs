using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[SerializeField]
public class Spell_AvadaKedavra : SpellBasic
{

    protected override void InitializeSpell()
    {
        base.InitializeDescription(
            name: "Avada Kedavra", 
            icon: Resources.Load<Sprite>(path: "Icons/Spells/Avada_Kedavra"), 
            description: "”·Ë‚‡˛˘ÂÂ Á‡ÍÎˇÚËÂ", 
            category: SpellCategory.Curse,
            cast_difficulty: SpellDifficulty.Hard, 
            learn_difficulty: SpellDifficulty.Easy);

        base.InitializePhysicalProperties(
            rechargeDurationBetweenCasts: 2.5f, 
            spellEffectDuration: 5f, 
            spell—astingCost: 100f, 
            spellHoldingCost: 0f, 
            spellEjectionType: SpellEjectionType.Shot);
    }

    public override bool TryCastSpell(Transform MuzzleSocket, ref GameObject Muzzle, Transform ProjectileSocket)
    {
        if (!base.IsCanCastSpell()) return false;

        if(Muzzle == base.GetMuzzle(MuzzleSocket)) return false;

        base.HideMuzzle(ref Muzzle);
        Muzzle = base.GetMuzzle(MuzzleSocket);

        base.DoShoot(ProjectileSocket, 10f);

        return true;
    }

    public override bool TryCastSpellAlt(Transform MuzzleSocket, ref GameObject Muzzle, Transform ProjectileSocket)
    {
        throw new System.NotImplementedException();
    }
}
