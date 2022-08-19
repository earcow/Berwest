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
            description: "Отшвыривающее заклятие",
            category: SpellCategory.Charms,
            cast_difficulty: SpellDifficulty.Easy,
            learn_difficulty: SpellDifficulty.Easy);

        base.InitializePhysicalProperties(
            rechargeDurationBetweenCasts: 0.75f,
            spellEffectDuration: 5f,
            spellСastingCost: 5f,
            spellHoldingCost: 0f,
            spellEjectionType: SpellEjectionType.Shot);
    }

    protected override bool TryCastSpell()
    {
        throw new System.NotImplementedException();
    }
}
