using System.Collections;
using System.Collections.Generic;
using System;
using System.Threading.Tasks;
using UnityEngine;

[SerializeField]
public abstract class SpellBasic : MonoBehaviour
{

    protected void InitializeDescription(string name, Sprite icon, string description, SpellCategory category, SpellDifficulty cast_difficulty, SpellDifficulty learn_difficulty)
    {
        this.description = new SpellDescription(name, icon, description, category, cast_difficulty, learn_difficulty);
    }
    protected void InitializePhysicalProperties(float rechargeDurationBetweenCasts, float spellEffectDuration, float spell�astingCost, float spellHoldingCost, SpellEjectionType spellEjectionType)
    {
        this.physicalProperties = new SpellPhysicalProperty(rechargeDurationBetweenCasts, spellEffectDuration, spell�astingCost, spellHoldingCost, spellEjectionType);
    }
    protected void InitializeProjectileShape(GameObject projectile, GameObject muzzle)
    {
        this.spellShape = new SpellShape(projectile, muzzle);
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

        [Tooltip("��� ����������")] [SerializeField] protected string _name;
        public string Name => this._name;

        [Tooltip("������ ����������")] [SerializeField] private Sprite _icon;
        public Sprite Icon => this._icon;

        [Tooltip("�������� ��������")] [SerializeField] private string _description;
        public string Description => this._description;

        [Tooltip("��� ��������")] [SerializeField] private SpellCategory _category;
        public SpellCategory Category => this._category;

        [Tooltip("��������� ���������� �������� (������ ����)")] [SerializeField] private SpellDifficulty _cast_difficulty;
        public SpellDifficulty CastDifficulty => this._cast_difficulty;

        [Tooltip("��������� �������� ��������")] [SerializeField] private SpellDifficulty _learn_difficulty;
        public SpellDifficulty LearnDifficulty => this._learn_difficulty;
    }

    [System.Serializable]
    public class SpellPhysicalProperty
    {
        public SpellPhysicalProperty(float rechargeDurationBetweenCasts, float spellEffectDuration, float spell�astingCost, float spellHoldingCost, SpellEjectionType spellEjectionType)
        {
            this._rechargeDurationBetweenCasts = rechargeDurationBetweenCasts;
            this._spellEffectDuration = spellEffectDuration;
            this._spell�astingCost = spell�astingCost;
            this._spellHoldingCost = spellHoldingCost;
            this._spellEjectionType = spellEjectionType;
        }

        [Tooltip("����� ����������� ����� �������, ���.")] [SerializeField] private float _rechargeDurationBetweenCasts;
        public float RechargeDuration => this._rechargeDurationBetweenCasts;

        [Tooltip("����������������� �������, ���.")] [SerializeField] private float _spellEffectDuration;
        public float SpellEffectDuration => this._spellEffectDuration;

        [Tooltip("������� �� ���������� ��������, ������� ����.")] [SerializeField] [Range(0,1000)] private float _spell�astingCost;
        public float Spell�astingCost => this._spell�astingCost;

        [Tooltip("������� �� ����������� ��������, ������ ���� � ���.")] [SerializeField] [Range(0, 100)] private float _spellHoldingCost;
        public float SpellHoldingCost => this._spellHoldingCost;

        [Tooltip("��� �����")] [SerializeField] private SpellEjectionType _spellEjectionType;
        public SpellEjectionType EjectionType => this._spellEjectionType;
    }

    [System.Serializable]
    public class SpellShape
    {
        public SpellShape(GameObject projectile, GameObject muzzle)
        {
            this._projectile = projectile;
            this._muzzle = muzzle;
        }

        [Tooltip("������ �������")] [SerializeField] private GameObject _projectile;
        public GameObject Projectile => this._projectile;

        [Tooltip("������� ��� �����")] [SerializeField] private GameObject _muzzle;
        public GameObject Muzzle => this._muzzle;
    }

    public abstract bool TryCastSpell(Transform MuzzleSocket, ref GameObject Muzzle, Transform ProjectileSocket);
    public abstract bool TryCastSpellAlt(Transform MuzzleSocket, ref GameObject Muzzle, Transform ProjectileSocket);

    protected abstract void InitializeSpell();

    
    //�������� ����������� ���������
    private bool isCanCast = true;

    protected async Task CastRecharge()
    {
        isCanCast = false;
        await Task.Delay(TimeSpan.FromSeconds(physicalProperties.RechargeDuration));
        isCanCast = true;
    }

    protected bool IsCanCastSpell()
    {
        if (!isCanCast) return false;
        return true;
    }


    //Muzzle ������
    protected GameObject GetMuzzle(Transform muzzleSocket)
    {
        return Instantiate(spellShape.Muzzle, muzzleSocket);
    }
    protected void HideMuzzle(ref GameObject muzzle)
    {
        DestroyImmediate(muzzle);
    }

    //���������� ��������
    private GameObject _projectile;
    protected void DoShoot(Transform projectileSocket)
    {
        var projectileSpeed = 10;
        _projectile = Instantiate(spellShape.Projectile, projectileSocket.position, projectileSocket.rotation);
        _projectile.GetComponent<Rigidbody>().AddForce(projectileSocket.forward * projectileSpeed, ForceMode.Force);

        CastRecharge();
    }

    //������������ ��� ������
    public enum SpellDifficulty
    {
        [InspectorName("˸����")] Easy,
        [InspectorName("�������")] Middle,
        [InspectorName("������")] Heavy,
        [InspectorName("�����������")] Hard,
        [InspectorName("�������������")] Extreme
    }
    public enum SpellCategory
    {
        [InspectorName("����")] Charms,
        [InspectorName("�����")] Jinx,
        [InspectorName("�����")] Hex,
        [InspectorName("���������")] Curse,

        //������� �������� �������� ��� �������.
        [InspectorName("��������������/���������")] TransfigurationConjuration,
        //����� ���������� ������������� ����� ����� ������.
        [InspectorName("��������������/������������")] TransfigurationSwitching,
        //��������� ���������� ����� �������.
        [InspectorName("��������������/�������������")] TransfigurationTransformation,
        //������������ �������, ��������.
        [InspectorName("��������������/������������")] TransfigurationVanishment
    }

    public enum SpellEjectionType
    {
        [InspectorName("�������")] Shot,
        [InspectorName("�������")] �ontact,
        [InspectorName("�� ����")] Self,
        [InspectorName("���")] Beam,
        [InspectorName("���������� ����")] Unique,
    }

}