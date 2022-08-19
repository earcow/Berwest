using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CharacterController))]
public class CharacterGravityEffector : MonoBehaviour
{
    [Tooltip("Объект на который воздействует гравитация. По-умолчанию используется this")]
    [SerializeField] private Transform _transform;

    [Tooltip("Переопределяет значение гравитации для персонажа. По-умолчанию используется значение -- минус 9.81f")]
    [SerializeField] private float _gravity;
    public float Gravity => _gravity;

    private Vector3 _gravityVector;

    private float _verticalVelocity;
    //private float _terminalVelocity = 53.0f;

    private CharacterController _characterController;

    void Awake()
    {
        if (_gravity == 0.0f)
            _gravity = GameConstants.Gravity;

        if (_transform == null)
            _transform = GetComponent<Transform>();

        _characterController = GetComponent<CharacterController>();

    }

    void FixedUpdate()
    {

        _characterController.Move(new Vector3(0, _gravity, _characterController.velocity.z)* Time.fixedDeltaTime);

    }
}
