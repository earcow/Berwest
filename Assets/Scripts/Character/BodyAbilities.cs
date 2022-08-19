using UnityEngine;

[System.Serializable]
public class BodyAbilities
{
    [Tooltip("Скорость движения перса в m/s")]
    public float MoveSpeed = 4.0f;
    [Tooltip("Скорость спринта в m/s")]
    public float SprintSpeed = 6.0f;
    [Tooltip("Скорость разворота персонажа")]
    public float RotationSpeed = 1.0f;
    [Tooltip("Ускорение и торможение")]
    public float SpeedChangeRate = 10.0f;

    [Space(10)]
    [Tooltip("Высота на которую персонаж может прыгнуть")]
    public float JumpHeight = 1.2f;
}