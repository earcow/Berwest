using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[SerializeField]
public abstract class SpellBasic : MonoBehaviour
{
    
    protected void InitializeDescription(string name, Sprite icon, string description, SpellCategory category, SpellDifficulty cast_difficulty, SpellDifficulty learn_difficulty)
    {
        this.description = new SpellDescription(name, icon, description, category, cast_difficulty, learn_difficulty);
    }
    protected void InitializePhysicalProperties(float rechargeDurationBetweenCasts, float spellEffectDuration, float spellСastingCost, float spellHoldingCost, SpellEjectionType spellEjectionType)
    {
        this.physicalProperties = new SpellPhysicalProperty(rechargeDurationBetweenCasts, spellEffectDuration, spellСastingCost, spellHoldingCost, spellEjectionType);
    }

    [SerializeField] public SpellDescription description;
    [SerializeField] public SpellPhysicalProperty physicalProperties;
    [SerializeField] public SpellShape spellShape;

    [System.Serializable]
    public class SpellDescription
    {
        public SpellDescription(string name, Sprite icon, string description, SpellCategory category, SpellDifficulty cast_difficulty, SpellDifficulty learn_difficulty)
        {
            this._name = name;
            this._icon = icon;
            this._description = description;
            this._category = category;
            this._cast_difficulty = cast_difficulty;
            this._learn_difficulty = learn_difficulty;
        }

        [Tooltip("Имя заклинания")] [SerializeField] protected string _name;
        public string Name => this._name;

        [Tooltip("Иконка заклинания")] [SerializeField] private Sprite _icon;
        public Sprite Icon => this._icon;

        [Tooltip("Описание заклятия")] [SerializeField] private string _description;
        public string Description => this._description;

        [Tooltip("Тип заклятия")] [SerializeField] private SpellCategory _category;
        public SpellCategory Category => this._category;

        [Tooltip("Сложность сотворения заклятия (расход маны)")] [SerializeField] private SpellDifficulty _cast_difficulty;
        public SpellDifficulty CastDifficulty => this._cast_difficulty;

        [Tooltip("Сложность изучения заклятия")] [SerializeField] private SpellDifficulty _learn_difficulty;
        public SpellDifficulty LearnDifficulty => this._learn_difficulty;
    }

    [System.Serializable]
    public class SpellPhysicalProperty
    {
        public SpellPhysicalProperty(float rechargeDurationBetweenCasts, float spellEffectDuration, float spellСastingCost, float spellHoldingCost, SpellEjectionType spellEjectionType)
        {
            this._rechargeDurationBetweenCasts = rechargeDurationBetweenCasts;
            this._spellEffectDuration = spellEffectDuration;
            this._spellСastingCost = spellСastingCost;
            this._spellHoldingCost = spellHoldingCost;
            this._spellEjectionType = spellEjectionType;
        }

        [Tooltip("Время перезарядки между кастами, сек.")] [SerializeField] private float _rechargeDurationBetweenCasts;
        public float RechargeDuration => this._rechargeDurationBetweenCasts;

        [Tooltip("Продолжительность эффекта, сек.")] [SerializeField] private float _spellEffectDuration;
        public float SpellEffectDuration => this._spellEffectDuration;

        [Tooltip("Затраты на сотворение заклятия, единицы маны.")] [SerializeField] [Range(0,1000)] private float _spellСastingCost;
        public float SpellСastingCost => this._spellСastingCost;

        [Tooltip("Затраты на поддержание заклятия, единиц маны в сек.")] [SerializeField] [Range(0, 100)] private float _spellHoldingCost;
        public float SpellHoldingCost => this._spellHoldingCost;

        [Tooltip("Тип каста")] [SerializeField] private SpellEjectionType _spellEjectionType;
        public SpellEjectionType EjectionType => this._spellEjectionType;
    }

    [System.Serializable]
    public class SpellShape
    {
        [Tooltip("Префаб снаряда")] [SerializeField] private GameObject _projectile;
        public GameObject Projectile => this._projectile;

        [Tooltip("Вспышка при касте")] [SerializeField] private GameObject _mizzle;
        public GameObject Mizzle => this._mizzle;
    }


    protected abstract bool TryCastSpell();
    protected abstract void InitializeSpell();

    //пересисления для класса
    public enum SpellDifficulty
    {
        [InspectorName("Лёгкий")] Easy,
        [InspectorName("Средний")] Middle,
        [InspectorName("Тяжёлый")] Heavy,
        [InspectorName("Невыносимый")] Hard,
        [InspectorName("Экстримальный")] Extreme
    }
    public enum SpellCategory
    {
        [InspectorName("Чары")] Charms,
        [InspectorName("Сглаз")] Jinx,
        [InspectorName("Порча")] Hex,
        [InspectorName("Проклятия")] Curse,

        //Изучает создание предмета «из воздуха».
        [InspectorName("Трансфигурация/Созидание")] TransfigurationConjuration,
        //Обмен физических характеристик между двумя целями.
        [InspectorName("Трансфигурация/Переключение")] TransfigurationSwitching,
        //Изменение физической формы объекта.
        [InspectorName("Трансфигурация/Трансформация")] TransfigurationTransformation,
        //Исчезновение объекта, очищение.
        [InspectorName("Трансфигурация/Исчезновение")] TransfigurationVanishment
    }

    public enum SpellEjectionType
    {
        [InspectorName("Выстрел")] Shot,
        [InspectorName("Касание")] Сontact,
        [InspectorName("На себя")] Self,
        [InspectorName("Луч")] Beam,
        [InspectorName("Уникальный каст")] Unique,
    }
}