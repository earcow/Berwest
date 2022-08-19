using UnityEngine;

[System.Serializable]
public class BodyAbilities
{
    [Tooltip("�������� �������� ����� � m/s")]
    public float MoveSpeed = 4.0f;
    [Tooltip("�������� ������� � m/s")]
    public float SprintSpeed = 6.0f;
    [Tooltip("�������� ��������� ���������")]
    public float RotationSpeed = 1.0f;
    [Tooltip("��������� � ����������")]
    public float SpeedChangeRate = 10.0f;

    [Space(10)]
    [Tooltip("������ �� ������� �������� ����� ��������")]
    public float JumpHeight = 1.2f;
}