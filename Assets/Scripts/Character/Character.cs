using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character : MonoBehaviour
{[Header("Описание персонажа")]
    [SerializeField] private string _name;
    [SerializeField] private Sex _sex;

    [SerializeField] public Health _health;
    [SerializeField] public BodyAbilities bodyAbilities = new BodyAbilities();

    [Tooltip("Изученные заклинания")]
    [SerializeField] private List <SpellBasic>_listLearnedSpells;
    public List<SpellBasic> LearnedSpells => _listLearnedSpells;

    [Tooltip("Заклятие на уме")]
    [SerializeField] private SpellBasic _selectedSpell;
    public SpellBasic SelectedSpell
    {
        get { return _selectedSpell; }
        set { _selectedSpell = value; }
    }


    [Tooltip("Предмет в руках (оружие)")]
    [SerializeField] private GameObject _itemInHand;
    public GameObject ItemInHand
    {
        get
        {
            return _itemInHand;
        }
        set
        {
            _itemInHand = value;
        }
    }

    [Tooltip("Инвентарь")]
    [SerializeField] private List<Weapon> _inventory;
    public List<Weapon> Inventory => _inventory;

    private void Awake()
    {
        _health.GetComponent<Health>();

        LearnedSpells.RemoveAll(spell => spell == null);

    }



}


