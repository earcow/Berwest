using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spell_Flipendo : SpellBasic
{

    protected override void InitializeSpell()
    {
        base.InitializeDescription(
            name: "Flipendo",
            icon: Resources.Load<Sprite>(path: "Icons/Spells/Flipendo"),
            description: "Îòøâûðèâàþùåå çàêëÿòèå",
            category: SpellCategory.Charms,
            cast_difficulty: SpellDifficulty.Easy,
            learn_difficulty: SpellDifficulty.Easy);

        base.InitializePhysicalProperties(
            rechargeDurationBetweenCasts: 0.75f,
            spellEffectDuration: 5f,
            spellÑastingCost: 5f,
            spellHoldingCost: 0f,
            spellEjectionType: SpellEjectionType.Shot);

        base.InitializeProjectileShape(
            projectile: Resources.Load<GameObject>(path: "Prefabs/SpellProjectiles/FlipendoProjectile"),
            muzzle:     Resources.Load<GameObject>(path: "Prefabs/SpellMuzzles/FlipendoMuzzle"));
    }

    public override bool TryCastSpell(Transform MuzzleSocket, ref GameObject Muzzle, Transform ProjectileSocket)
    {
        if (!base.IsCanCastSpell()) return false;

        base.HideMuzzle(ref Muzzle);
        Muzzle = base.GetMuzzle(MuzzleSocket);

        base.DoShoot(ProjectileSocket);

        return true;
    }

    public override bool TryCastSpellAlt(Transform MuzzleSocket, ref GameObject Muzzle, Transform ProjectileSocket)
    {
        throw new System.NotImplementedException();
    }
}
