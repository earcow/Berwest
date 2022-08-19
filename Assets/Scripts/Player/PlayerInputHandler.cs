using System;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Interactions;

public class PlayerInputHandler : CharacterInput
{
    public PlayerInput PlayerInput;

    [Header("Настройки передвижения")]
    public bool analogMovement;

    [Header("Настройки курсора мыши")]
    public bool cursorLocked = true;
    public bool cursorInputForLook = true;

    public bool LockControl = false;

    void Awake()
    {
        PlayerInput = new PlayerInput();
        PlayerInput.Player.Enable();
    }

    private void Start()
    {
        PlayerInput.Player.Jump.performed += context => Jump(activateJump: true);
        PlayerInput.Player.Jump.canceled += context => Jump(activateJump: false);

        PlayerInput.Player.Sprint.started += context => Sprint(activateSprint: true);
        PlayerInput.Player.Sprint.canceled += context => Sprint(activateSprint: false);

        PlayerInput.Player.WeaponChange.performed += context => WeaponChange(context.ReadValue<float>() > 0.1f ? true : false);

        PlayerInput.Player.QA1.performed += context => WeaponSelect(1);
        PlayerInput.Player.QA2.performed += context => WeaponSelect(2);
        PlayerInput.Player.QA3.performed += context => WeaponSelect(3);
        PlayerInput.Player.QA4.performed += context => WeaponSelect(4);
        PlayerInput.Player.QA5.performed += context => WeaponSelect(5);
        PlayerInput.Player.QA6.performed += context => WeaponSelect(6);
        PlayerInput.Player.QA7.performed += context => WeaponSelect(7);
        PlayerInput.Player.QA8.performed += context => WeaponSelect(8);
        PlayerInput.Player.QA9.performed += context => WeaponSelect(9);
        PlayerInput.Player.QA0.performed += context => WeaponSelect(0);

        PlayerInput.Player.SpellChange.performed += context => SpellChange(context.ReadValue<Vector2>().y > 0.1f ? true : false);
        PlayerInput.Player.OpenSpellList.performed += context => TryOpenSpellMenu();

        PlayerInput.Player.Fire.canceled += context =>
        {
            if (context.interaction is PressInteraction)
                TryFire();
        };
            PlayerInput.Player.Fire.performed += context =>
        {
            if (context.interaction is HoldInteraction)
                TryLongFire();
        };

        PlayerInput.Player.AltFire.canceled += context =>
        {
            if (context.interaction is PressInteraction)
                TryAltFire();
        };

        PlayerInput.Player.AltFire.performed += context =>
        {
            if (context.interaction is HoldInteraction)
                TryLongAltFire();
        };


    }

    private void Update()
    {
        Look = PlayerInput.Player.Look.ReadValue<Vector2>();
        Move = PlayerInput.Player.Move.ReadValue<Vector2>();
    }

    public void Jump(bool activateJump)
    {
        OnJump?.Invoke();
        JumpFlag = activateJump;
    }
    public UnityEvent OnJump = new UnityEvent();

    public void Sprint(bool activateSprint)
    {
        OnSprint?.Invoke();
        SprintFlag = activateSprint;
    }
    public UnityEvent OnSprint = new UnityEvent();

    private void WeaponChange(bool isForwardDirection)
    {
        if (isForwardDirection)
        {
            OnNextWeaponChanged?.Invoke();
        }
        else
        {
            OnPreviousWeaponChanged?.Invoke();
        }
    }
    public UnityEvent OnNextWeaponChanged = new UnityEvent();
    public UnityEvent OnPreviousWeaponChanged = new UnityEvent();

    private void WeaponSelect(int indexSlot)
    {
        OnWeaponSelected?.Invoke(indexSlot);
    }
    public UnityEvent<int> OnWeaponSelected = new UnityEvent<int>();

    private void SpellChange(bool isForwardDirection)
    {
        if (isForwardDirection)
        {
            OnNextSpellChanged?.Invoke();
        }
        else
        {
            OnPreviousSpellChanged?.Invoke();
        }
    }
    public UnityEvent OnNextSpellChanged = new UnityEvent();
    public UnityEvent OnPreviousSpellChanged = new UnityEvent();

    private void TryOpenSpellMenu()
    {
        OnSpellMenuCalled?.Invoke();
    }
    public UnityEvent OnSpellMenuCalled = new UnityEvent();

    private void TryFire()
    {
        OnFireCalled?.Invoke();

    }
    public UnityEvent OnFireCalled = new UnityEvent();

    private void TryLongFire()
    {
        OnLongFireCalled?.Invoke();
    }
    public UnityEvent OnLongFireCalled = new UnityEvent();

    private void TryAltFire()
    {
        OnAltFireCalled?.Invoke();
    }
    public UnityEvent OnAltFireCalled = new UnityEvent();

    private void TryLongAltFire()
    {
        OnLongAltFireCalled?.Invoke();
    }
    public UnityEvent OnLongAltFireCalled = new UnityEvent();







    public override float GetInputMagnitude()
    {
        if (analogMovement)
            return this.Move.magnitude;
        else
            return 1.0f;
    }

    private void OnApplicationFocus(bool hasFocus) => SetCursorState(cursorLocked);
    private void SetCursorState(bool newState) => Cursor.lockState = newState ? CursorLockMode.Locked : CursorLockMode.None;

    private void OnValidate()
    {
        if (PlayerInput != null)
        {
            if (LockControl)
                DisablePlayerInput(PlayerInput);
            else
                EnablePlayerInput(PlayerInput);
        }
    }

    public void DisablePlayerInput(PlayerInput input)
    {
        input.Disable();
    }
    public void EnablePlayerInput(PlayerInput input)
    {
        input.Enable();
    }

}
