using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PlayerWeaponManager : MonoBehaviour
{
    [SerializeField] private Character _character;
    [SerializeField] private Transform _weaponSocket;

    private int _indexWeaponSlot = 1;

    [Tooltip("Слоты быстрого доступа")]
    public QuickAccessSlot[] QuickAccessPanel =
    {
        new QuickAccessSlot(null),  //0 
        new QuickAccessSlot(null),  //1
        new QuickAccessSlot(null),  //2
        new QuickAccessSlot(null),  //3
        new QuickAccessSlot(null),  //4
        new QuickAccessSlot(null),  //5
        new QuickAccessSlot(null),  //6
        new QuickAccessSlot(null),  //7
        new QuickAccessSlot(null),  //8
        new QuickAccessSlot(null)   //9
        };

    private void Awake()
    {
        _character = this.GetComponent<Character>();
        _weaponSocket = GetComponentInChildren<TagWeaponSocket>().GetComponent<Transform>();

        UpdateDisplayWeapon();
    }

    private void OnEnable()
    {
        if (this.TryGetComponent<PlayerInputHandler>(out var inputHandler))
        {
            inputHandler.OnNextWeaponChanged.AddListener(ChangeNextWeapon);
            inputHandler.OnPreviousWeaponChanged.AddListener(ChangePreviousWeapon);

            inputHandler.OnWeaponSelected.AddListener(SelectWeapon);

            inputHandler.OnFireCalled.AddListener(DoFire);
            inputHandler.OnAltFireCalled.AddListener(DoAltFire);

            //inputHandler.OnLongFireCalled.AddListener(Blya);
            //inputHandler.OnLongAltFireCalled.AddListener(Suka);

        }
    }
    /*
    private void Suka()
    {
        Debug.Log("OnLongAltFireCalled");
    }

    private void Blya()
    {
        Debug.Log("OnLongFireCalled");
    }
    */
    private void OnDisable()
    {
        if (this.TryGetComponent<PlayerInputHandler>(out var inputHandler))
        {
            inputHandler.OnNextWeaponChanged.RemoveListener(ChangeNextWeapon);
            inputHandler.OnPreviousWeaponChanged.RemoveListener(ChangePreviousWeapon);

            inputHandler.OnWeaponSelected.RemoveListener(SelectWeapon);

            inputHandler.OnFireCalled.RemoveListener(DoFire);
            inputHandler.OnAltFireCalled.RemoveListener(DoAltFire);
        }
    }

    private void SelectWeapon(int index)
    {
        if (_indexWeaponSlot != index)
        {
            _indexWeaponSlot = index;
            UpdateDisplayWeapon();
        }
    }

    private void ChangeNextWeapon()
    {
        if (_indexWeaponSlot + 1 > QuickAccessPanel.Length - 1)
            _indexWeaponSlot = 0;
        else
            _indexWeaponSlot++;

        UpdateDisplayWeapon();
        Debug.Log("Weapon from slot " + _indexWeaponSlot);// _character._quickSlots
    }

    private void ChangePreviousWeapon()
    {
        if (_indexWeaponSlot - 1 < 0)
            _indexWeaponSlot = QuickAccessPanel.Length-1;
        else
            _indexWeaponSlot--;

        UpdateDisplayWeapon();
        Debug.Log("Weapon from slot " + _indexWeaponSlot);// _character._quickSlots
    }

    private void UpdateDisplayWeapon()
    {
        DestroyImmediate(_character.ItemInHand);

        if (QuickAccessPanel[_indexWeaponSlot].Content != null)
            _character.ItemInHand = Instantiate(QuickAccessPanel[_indexWeaponSlot].Content, _weaponSocket);
    }


    private void DoFire()
    {
        if(_character.ItemInHand != null && _character.ItemInHand.TryGetComponent<Weapon>(out var item))
        {
            item.ExecutePrimaryAction();
        }
    }

    private void DoAltFire()
    {
        if (_character.ItemInHand != null && _character.ItemInHand.TryGetComponent<Weapon>(out var item))
        {
            item.ExecuteSecondaryAction();
        }
    }

}
