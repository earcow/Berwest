using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character : MonoBehaviour
{[Header("�������� ���������")]
    [SerializeField] private string _name;
    [SerializeField] private Sex _sex;

    [SerializeField] public Health _health;
    [SerializeField] public BodyAbilities bodyAbilities = new BodyAbilities();

    [Tooltip("��������� ����������")]
    [SerializeField] private List <SpellBasic>_listLearnedSpells;
    public List<SpellBasic> LearnedSpells => _listLearnedSpells;

    [Tooltip("�������� �� ���")]
    [SerializeField] private SpellBasic _selectedSpell;
    public SpellBasic SelectedSpell
    {
        get { return _selectedSpell; }
        set { _selectedSpell = value; }
    }


    [Tooltip("������� � ����� (������)")]
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

    [Tooltip("���������")]
    [SerializeField] private List<Weapon> _inventory;
    public List<Weapon> Inventory => _inventory;

    private void Awake()
    {
        _health.GetComponent<Health>();

        LearnedSpells.RemoveAll(spell => spell == null);

    }



}


