//------------------------------------------------------------------------------------------
// <author>Pablo Perdomo Falcón</author>
// <copyright file="PlayerType.cs" company="Pabllopf">GNU General Public License v3.0</copyright>
//------------------------------------------------------------------------------------------
using UnityEditor;
using UnityEngine;

/// <summary>Define a style of a dungeon.</summary>
[System.Serializable]
[CreateAssetMenu(fileName = "New Player", menuName = "Player/New")]
public class PlayerType : ScriptableObject 
{
    [Header("Name:")]

    /// <summary>The name player</summary>
    [SerializeField]
    private string namePlayer = string.Empty;

    [Header("Stats:")]

    /// <summary>The speed of movement</summary>
    [SerializeField]
    [Range(1f, 10f)]
    private float speedOfMovement = 3f;

    [Space(10)]

    /// <summary>The radius attack</summary>
    [SerializeField]
    [Range(0.1f, 1f)]
    private float radiusAttack = 0.5f;

    /// <summary>The frequency to attack</summary>
    [SerializeField]
    [Range(0.1f, 1f)]
    private float frequencyToAttack = 0.20f;

    [Space(10)]

    /// <summary>The frequency to use skill</summary>
    [SerializeField]
    [Range(0.1f, 1f)]
    private float frequencyToUseSkill = 0.20f;
 
    [Header("Attack:")]

    /// <summary>The attack</summary>
    [SerializeField]
    private AttackType attack = AttackType.Melee;

    [Header("Skill:")]

    /// <summary>The skill</summary>
    [SerializeField]
    private SkillType skill = SkillType.Roll;

    [Header("Animator Controller:")]
   
    /// <summary>The controller</summary>
    [SerializeField]
    private RuntimeAnimatorController controller = null;

    #region Encapsulate Fields

    /// <summary>Gets or sets the name player.</summary>
    /// <value>The name player.</value>
    public string NamePlayer { get => namePlayer; set => namePlayer = value; }
    
    /// <summary>Gets or sets the speed of movement.</summary>
    /// <value>The speed of movement.</value>
    public float SpeedOfMovement { get => speedOfMovement; set => speedOfMovement = value; }

    /// <summary>Gets or sets the radius attack.</summary>
    /// <value>The radius attack.</value>
    public float RadiusAttack { get => radiusAttack; set => radiusAttack = value; }
    
    /// <summary>Gets or sets the frequency to attack.</summary>
    /// <value>The frequency to attack.</value>
    public float FrequencyToAttack { get => frequencyToAttack; set => frequencyToAttack = value; }

    /// <summary>Gets or sets the frequency to use skill.</summary>
    /// <value>The frequency to use skill.</value>
    public float FrequencyToUseSkill { get => frequencyToUseSkill; set => frequencyToUseSkill = value; }

    /// <summary>Gets or sets the attack.</summary>
    /// <value>The attack.</value>
    public AttackType Attack { get => attack; set => attack = value; }

    /// <summary>Gets or sets the skill.</summary>
    /// <value>The skill.</value>
    public SkillType Skill { get => skill; set => skill = value; }

    /// <summary>Gets or sets the controller.</summary>
    /// <value>The controller.</value>
    public RuntimeAnimatorController Controller { get => controller; set => controller = value; }
   
    #endregion

    #region Validate Name
    /// <summary>Called when [validate].</summary>
    public void OnValidate()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.delayCall += () =>
        {
            string assetPath = AssetDatabase.GetAssetPath(GetInstanceID());
            AssetDatabase.RenameAsset(assetPath, NamePlayer);
            AssetDatabase.SaveAssets();
        };
#endif
    }
    #endregion
}