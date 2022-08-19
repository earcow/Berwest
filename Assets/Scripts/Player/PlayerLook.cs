using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerLook : MonoBehaviour
{
    private PlayerInputHandler _playerInputHandler;
    private Character _player;

    [Header("Настройки Cinemachine")]
    [Tooltip("Цель следования, установленная в виртуальной камере Cinemachine, за которой будет следовать камера")]
    public GameObject CinemachineCameraTarget;

    [Tooltip("Максимальный угол верчения ебальника")]
    public float TopClamp = 90.0f;
    [Tooltip("Минимальный угол")]
    public float BottomClamp = -90.0f;

    // cinemachine
    private float _cinemachineTargetPitch;
    private float _rotationVelocity;

    private const float _threshold = 0.01f;

    private bool IsCurrentDeviceMouse => true;


    private void Awake()
    {
        _playerInputHandler = GetComponent<PlayerInputHandler>();
        _player = GetComponent<Character>();

        if (CinemachineCameraTarget == null)
        {
            GameObject findedCameraTarget;
            CinemachineCameraTarget = (findedCameraTarget = this.GetComponentInChildren<TagLookRoot>().gameObject) != null ? findedCameraTarget : null;
        }
    }

    private void LateUpdate()
    {
        CameraRotation();
    }

    private void CameraRotation()
    {
        // if there is an input
        if (_playerInputHandler.Look.sqrMagnitude >= _threshold)
        {
            //Don't multiply mouse input by Time.deltaTime
            float deltaTimeMultiplier = IsCurrentDeviceMouse ? 1.0f : Time.deltaTime;

            _cinemachineTargetPitch += _playerInputHandler.Look.y * _player.bodyAbilities.RotationSpeed * deltaTimeMultiplier;
            _rotationVelocity = _playerInputHandler.Look.x * _player.bodyAbilities.RotationSpeed * deltaTimeMultiplier;

            // clamp our pitch rotation
            _cinemachineTargetPitch = ClampAngle(_cinemachineTargetPitch, BottomClamp, TopClamp);

            // Update Cinemachine camera target pitch
            CinemachineCameraTarget.transform.localRotation = Quaternion.Euler(_cinemachineTargetPitch, 0.0f, 0.0f);

            // rotate the player left and right
            transform.Rotate(Vector3.up * _rotationVelocity);
        }
    }

    private static float ClampAngle(float lfAngle, float lfMin, float lfMax)
    {
        if (lfAngle < -360f) lfAngle += 360f;
        if (lfAngle > 360f) lfAngle -= 360f;
        return Mathf.Clamp(lfAngle, lfMin, lfMax);
    }
}