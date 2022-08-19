using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterMove : MonoBehaviour
{

    public CharacterInput _characterInput;

    private CharacterController _characterController;
    private GroundChecker _characterGroundChecker;
    private Character _characterData;

    [Tooltip("Время, которое должно пройти чтобы можно было прыгнуть снова. Поставь 0f чтобы прыгать мгновенно")]
    public float JumpTimeout = 0.1f;

    //ХЗ нахуя
    [Tooltip("Время, необходимое для перехода в состояние падения. Полезно для спуска по лестнице")]
    public float FallTimeout = 0.15f;

    private float _verticalVelocity;
    private float _terminalVelocity = 53.0f;

    // timeout deltatime
    private float _jumpTimeoutDelta;
    private float _fallTimeoutDelta;

    private void Start()
    {
        _characterData = GetComponent<Character>();
        //_characterInput = GetComponent<PlayerInputHandler>();

        _characterController = GetComponent<CharacterController>();
        _characterGroundChecker = GetComponent<GroundChecker>();

        _jumpTimeoutDelta = JumpTimeout;
        _fallTimeoutDelta = FallTimeout;
    }

    private void FixedUpdate()
    {
        CalculateJumpAndGravity(ref _characterInput.JumpFlag);
        Move(ref _characterInput);
    }

    private void CalculateJumpAndGravity(ref bool inputJump, float gravity = GameConstants.Gravity)
    {
        if (_characterGroundChecker.IsGrounded)
        {
            // reset the fall timeout timer
            _fallTimeoutDelta = FallTimeout;

            // stop our velocity dropping infinitely when grounded
            if (_verticalVelocity < 0.0f)
            {
                _verticalVelocity = -2f;
            }

            // Jump
            if (inputJump && _jumpTimeoutDelta <= 0.0f)
            {
                // the square root of H * -2 * G = how much velocity needed to reach desired height
                _verticalVelocity = Mathf.Sqrt(_characterData.bodyAbilities.JumpHeight * -2f * gravity);
            }

            // jump timeout
            if (_jumpTimeoutDelta >= 0.0f)
            {
                _jumpTimeoutDelta -= Time.deltaTime;
            }
        }
        else
        {
            // reset the jump timeout timer
            _jumpTimeoutDelta = JumpTimeout;

            // fall timeout
            if (_fallTimeoutDelta >= 0.0f)
            {
                _fallTimeoutDelta -= Time.deltaTime;
            }

            // if we are not grounded, do not jump
            inputJump = false;
        }

        if (!_characterController.enabled)
            return;

        // apply gravity over time if under terminal (multiply by delta time twice to linearly speed up over time)
        if (_verticalVelocity < _terminalVelocity)
        {
            _verticalVelocity += gravity * Time.deltaTime;
        }

    }
    private void Move(ref CharacterInput characterInput)
    {
        // set target speed based on move speed, sprint speed and if sprint is pressed
        float targetSpeed = characterInput.SprintFlag ? _characterData.bodyAbilities.SprintSpeed : _characterData.bodyAbilities.MoveSpeed;
        float calculatedMoveSpeed;

        // a simplistic acceleration and deceleration designed to be easy to remove, replace, or iterate upon

        // note: Vector2's == operator uses approximation so is not floating point error prone, and is cheaper than magnitude
        // if there is no input, set the target speed to 0
        if (characterInput.Move == Vector2.zero) targetSpeed = 0.0f;

        // a reference to the players current horizontal velocity
        float currentHorizontalSpeed = new Vector3(_characterController.velocity.x, 0.0f, _characterController.velocity.z).magnitude;

        float speedOffset = 0.1f;
        float inputMagnitude = characterInput.GetInputMagnitude();

        // accelerate or decelerate to target speed
        if (currentHorizontalSpeed < targetSpeed - speedOffset || currentHorizontalSpeed > targetSpeed + speedOffset)
        {
            // creates curved result rather than a linear one giving a more organic speed change
            // note T in Lerp is clamped, so we don't need to clamp our speed
            calculatedMoveSpeed = Mathf.Lerp(currentHorizontalSpeed, targetSpeed * inputMagnitude, Time.deltaTime * _characterData.bodyAbilities.SpeedChangeRate);

            // round speed to 3 decimal places
            calculatedMoveSpeed = Mathf.Round(calculatedMoveSpeed * 1000f) / 1000f;
        }
        else
        {
            calculatedMoveSpeed = targetSpeed;
        }

        // normalise input direction
        Vector3 inputDirection = new Vector3(characterInput.Move.x, 0.0f, characterInput.Move.y).normalized;

        // note: Vector2's != operator uses approximation so is not floating point error prone, and is cheaper than magnitude
        // if there is a move input rotate player when the player is moving
        if (characterInput.Move != Vector2.zero)
        {
            // move
            inputDirection = transform.right * characterInput.Move.x + transform.forward * characterInput.Move.y;
        }

        // move the player
        _characterController.Move(inputDirection.normalized * (calculatedMoveSpeed * Time.deltaTime) + new Vector3(0.0f, _verticalVelocity, 0.0f) * Time.deltaTime);
    }
}