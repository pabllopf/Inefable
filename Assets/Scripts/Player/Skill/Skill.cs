//------------------------------------------------------------------------------------------
// <author>Pablo Perdomo Falcón</author>
// <copyright file="Skill.cs" company="Pabllopf">GNU General Public License v3.0</copyright>
//------------------------------------------------------------------------------------------
using UnityEngine;

/// <summary>Skills of the players</summary>
public class Skill
{
    /// <summary>The action type</summary>
    private readonly SkillType skillType;

    /// <summary>Initializes a new instance of the <see cref="Skill"/> class.</summary>
    /// <param name="skillType">Type of the skill.</param>
    private Skill(SkillType skillType)
    {
        this.skillType = skillType;
    }

    /// <summary>Invokes the specified skill type.</summary>
    /// <param name="skillType">Type of the skill.</param>
    public static Skill Invoke(SkillType skillType)
    {
        return new Skill(skillType);
    }

    /// <summary>Ofs the this.</summary>
    /// <param name="obj">The object.</param>
    public void OfThis(GameObject obj)
    {
        switch (skillType)
        {
            case SkillType.Roll:
                RollNow(obj);
                break;
        }
    }

    /// <summary>Rolls the now.</summary>
    private void RollNow(GameObject obj)
    {
        obj.GetComponent<Animator>().SetBool("Skill", true);
    }
}