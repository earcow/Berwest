using UnityEngine;
using UnityEngine.InputSystem;

public class MouseScrollAxisYInteractor : IInputInteraction<Vector2>
{
    public float DelayBetweenTriggering;

    private bool _isLockInput = false;
    private Vector2 _input;

    [UnityEditor.InitializeOnLoadMethod]
    private static void Register()
    {
        InputSystem.RegisterInteraction<MouseScrollAxisYInteractor>();
    }

    public void Process(ref InputInteractionContext context)
    {
        if (_isLockInput)
            _isLockInput = !context.timerHasExpired;

        switch (context.phase)
        {
            case InputActionPhase.Waiting:
                this._input = context.ReadValue<Vector2>();

                if (_input.y >= 0.1f || _input.y <= -0.1f)
                {
                    if (_isLockInput)
                        return;

                    this._isLockInput = true;
                    context.Performed();
                }
                else
                    return;
                break;

            default:
                return;
        }
        context.SetTimeout(DelayBetweenTriggering);
    }

    public void Reset()
    {}

}
