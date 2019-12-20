//------------------------------------------------------------------------------------------
// <author>Pablo Perdomo Falcón</author>
// <copyright file="Health.cs" company="Pabllopf">GNU General Public License v3.0</copyright>
//------------------------------------------------------------------------------------------
using UnityEngine;
using UnityEngine.UI;

/// <summary>The health of the player</summary>
public class Health : MonoBehaviour
{
    /// <summary>The health</summary>
    public int health;

    /// <summary>The health UI</summary>
    public Scrollbar healthUI;

    /// <summary>The shield</summary>
    public int shield;

    /// <summary>The shield UI</summary>
    public Scrollbar shieldUI;

    /// <summary>The shield object</summary>
    private GameObject shieldOBJ;

    /// <summary>Awakes this instance.</summary>
    public void Awake()
    {
        Stats.Current = new Stats();
    }

    /// <summary>Starts this instance.</summary>
    public void Start()
    {
        this.healthUI = transform.Find("Interface/Bar/Health").GetComponent<Scrollbar>();
        this.shieldUI = transform.Find("Interface/Bar/Shield").GetComponent<Scrollbar>();
        this.shieldOBJ = transform.Find("Interface/Bar/Shield").gameObject;

        this.health = Stats.Current.Health;
        this.healthUI.size = (float)this.health / 100;

        this.shield = Stats.Current.Shield;
        this.shieldUI.size = (float)this.shield / 100;
    }

    /// <summary>Treats this instance.</summary>
    public void Treat()
    {
        int amount = Random.Range(1, 10);
        if ((this.health + amount) < 100)
        {
            this.health += amount;
            Stats.Current.Health = this.health;
            this.healthUI.size = (float)this.health / 100;
        }
    }

    /// <summary>Takes the damage.</summary>
    public void TakeDamage()
    {
        int amount = Random.Range(1, 10);
        if (this.health > 0 && this.shield > 0)
        {
            this.shield -= amount;
            Stats.Current.Shield = this.shield;
            this.shieldUI.size = (float)this.shield / 100;
        }
        else
        {
            this.shieldOBJ.SetActive(false);

            if (this.health > 0)
            {
                this.health -= amount;
                Stats.Current.Health = this.health;
                this.healthUI.size = (float)this.health / 100;
            }
        }
    }

    /// <summary>Put full the shield.</summary>
    public void FullShield()
    {
        this.shield = 100;
        Stats.Current.Shield = this.shield;
        this.shieldOBJ.SetActive(true);
        this.shieldUI.size = (float)this.shield / 100;
    }

    /// <summary>Put full the health.</summary>
    public void FullHealth()
    {
        this.health = 100;
        Stats.Current.Health = this.health;
        this.healthUI.size = (float)this.health / 100;
    }
}
