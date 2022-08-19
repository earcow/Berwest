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

    protected override bool TryCastSpell()
    {
        throw new System.NotImplementedException();
    }
}
