using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSpellManager : MonoBehaviour
{
    [SerializeField] private Character _character;

    private int _indexSpell = 0;

    private void Awake()
    {
        _character = this.GetComponent<Character>();

        UpdateCurrentSpell(_indexSpell);
    }

    private void UpdateCurrentSpell(int IndexOfSpell)
    {
        _character.SelectedSpell = _character.LearnedSpells[IndexOfSpell];
    }

    private void OnEnable()
    {
        if (this.TryGetComponent<PlayerInputHandler>(out var inputHandler))
        {
            inputHandler.OnNextSpellChanged.AddListener(ChangeNextSpell);
            inputHandler.OnPreviousSpellChanged.AddListener(ChangePreviousSpell);
        }
    }
    private void OnDisable()
    {
        if (this.TryGetComponent<PlayerInputHandler>(out var inputHandler))
        {
            inputHandler.OnNextSpellChanged.RemoveListener(ChangeNextSpell);
            inputHandler.OnNextSpellChanged.RemoveListener(ChangePreviousSpell);
        }
    }

    private void ChangePreviousSpell()
    {
        if (_character.ItemInHand != null && _character.ItemInHand.TryGetComponent<Wand>(out var item))
        {
            if (_indexSpell - 1 < 0)
                _indexSpell = _character.LearnedSpells.Count - 1;
            else
                _indexSpell--;

            UpdateCurrentSpell(_indexSpell);
            Debug.Log("Selected spell - " + _character.SelectedSpell.description.Name);
        }
    }
    private void ChangeNextSpell()
    {
        if (_character.ItemInHand != null && _character.ItemInHand.TryGetComponent<Wand>(out var item))
        {
            if (_indexSpell + 1 > _character.LearnedSpells.Count - 1)
                _indexSpell = 0;
            else
                _indexSpell++;

            UpdateCurrentSpell(_indexSpell);

            Debug.Log("Selected spell - " + _character.SelectedSpell.description.Name);
        }
    }
}