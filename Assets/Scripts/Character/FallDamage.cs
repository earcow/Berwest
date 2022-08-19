using UnityEngine;
using UnityEngine.Events;

public class FallDamage : MonoBehaviour
{
    private GroundChecker _groundChecker;
    private CharacterController _characterController;
    
    [Tooltip("�������� ���� ��� ������� � ������?")]
    public bool RecievesFallDamage = true;

    [Tooltip("����������� �������� ��������� �����")]
    public float MinSpeedForFallDamage = 10f;

    [Tooltip("�������� ��� ������� ����������� ������������ ����")]
    public float MaxSpeedForFallDamage = 30f;

    [Tooltip("����������� ���� �� �������")]
    public float FallDamageAtMinSpeed = 10f;

    [Tooltip("������������ ���� �� �������")]
    public float FallDamageAtMaxSpeed = 50f;

    private Vector3 _characterVelocity;
    private Vector3 _latestImpactSpeed;

    private void Awake()
    {
        _groundChecker = GetComponent<GroundChecker>();
        _characterController = GetComponent<CharacterController>();
    }

    private void FixedUpdate()
    {
        _characterVelocity = _characterController.velocity;
        if (_groundChecker.IsGrounded)
        {
            float fallSpeed = -Mathf.Min(_characterVelocity.y, _latestImpactSpeed.y);
            float fallSpeedRatio = (fallSpeed - MinSpeedForFallDamage) /
                                   (MaxSpeedForFallDamage - MinSpeedForFallDamage);
            if (RecievesFallDamage && fallSpeedRatio > 0f)
            {
                float dmgFromFall = Mathf.Lerp(FallDamageAtMinSpeed, FallDamageAtMaxSpeed, fallSpeedRatio);
                OnFallDamage?.Invoke(null, dmgFromFall);
            }
        }
        _latestImpactSpeed = _characterVelocity;
    }

    public UnityEvent<GameObject, float> OnFallDamage = new UnityEvent<GameObject, float>();
}
