/*
[System.Serializable]
public struct CrosshairData
{

[Tooltip("Спрайт перекрестия")]
public Sprite CrosshairSprite;

[Tooltip("Размер перекрестия")]
public int CrosshairSize;

[Tooltip("Цвет перекрестия")]
public Color CrosshairColor;
}

public enum SpellDifficultyEnum
{
[InspectorName("Лёгкий")] Easy,
[InspectorName("Средний")] Middle,
[InspectorName("Тяжёлый")] Heavy,
[InspectorName("Невыносимый")] Hard,
[InspectorName("Экстремальный")] Extreme
}

public enum SpellFrequencyEnum
{
[InspectorName("Одиночный")] Solo,
[InspectorName("Очередью")] OneByOne,
[InspectorName("Непрерывное действие")] Indivisible
}

public enum SpellTypeEnum
{
[InspectorName("Чары")] Charms,
[InspectorName("Сглаз")] Jinx,
[InspectorName("Порча")] Hex,
[InspectorName("Проклятия")] Curse,

//Трансфигурация
[InspectorName("Трансфигурация/Созидание")] TransfigurationConjuration,        //Изучает создание предмета «из воздуха».
[InspectorName("Трансфигурация/Переключение")] TransfigurationSwitching,       //Обмен физических характеристик между двумя целями.
[InspectorName("Трансфигурация/Трансформация")] TransfigurationTransformation, //Изменение физической формы объекта.
[InspectorName("Трансфигурация/Исчезновение")] TransfigurationVanishment       //Исчезновение объекта, очищение.
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

