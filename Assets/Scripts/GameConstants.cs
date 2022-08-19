/*
[System.Serializable]
public struct CrosshairData
{

[Tooltip("������ �����������")]
public Sprite CrosshairSprite;

[Tooltip("������ �����������")]
public int CrosshairSize;

[Tooltip("���� �����������")]
public Color CrosshairColor;
}

public enum SpellDifficultyEnum
{
[InspectorName("˸����")] Easy,
[InspectorName("�������")] Middle,
[InspectorName("������")] Heavy,
[InspectorName("�����������")] Hard,
[InspectorName("�������������")] Extreme
}

public enum SpellFrequencyEnum
{
[InspectorName("���������")] Solo,
[InspectorName("��������")] OneByOne,
[InspectorName("����������� ��������")] Indivisible
}

public enum SpellTypeEnum
{
[InspectorName("����")] Charms,
[InspectorName("�����")] Jinx,
[InspectorName("�����")] Hex,
[InspectorName("���������")] Curse,

//��������������
[InspectorName("��������������/���������")] TransfigurationConjuration,        //������� �������� �������� ��� �������.
[InspectorName("��������������/������������")] TransfigurationSwitching,       //����� ���������� ������������� ����� ����� ������.
[InspectorName("��������������/�������������")] TransfigurationTransformation, //��������� ���������� ����� �������.
[InspectorName("��������������/������������")] TransfigurationVanishment       //������������ �������, ��������.
}
*/
public static class GameConstants
{
    public const float Gravity = -9.81f;
    public const float CharacterGravity = -15.0f;
}

public enum Sex
{
    male = 1,
    female = 2,
    none = 0
}

