//------------------------------------------------------------------------------------------
// <author>Pablo Perdomo Falcón</author>
// <copyright file="Player.cs" company="Pabllopf">GNU General Public License v3.0</copyright>
//------------------------------------------------------------------------------------------
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

/// <summary>Manage the player of the game.</summary>
[RequireComponent(typeof(SpriteRenderer))]
[RequireComponent(typeof(AudioListener))]
[RequireComponent(typeof(CapsuleCollider2D))]
[RequireComponent(typeof(Animator))]
[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(Wallet))]
[RequireComponent(typeof(Health))]
public class Player : MonoBehaviour
{
    /// <summary>The open</summary>
    private const string Open = "Open";

    /// <summary>The run</summary>
    private const string Run = "Run";

    /// <summary>The attack</summary>
    private const string Attack = "Attack";

    /// <summary>The skill</summary>
    private const string Skill = "Roll";

    /// <summary>The vertical</summary>
    private const string Vertical = "Vertical";

    /// <summary>The horizontal</summary>
    private const string Horizontal = "Horizontal";

    /// <summary>The position</summary>
    private Vector3 position = Vector3.zero;

    /// <summary>The direction</summary>
    private Vector3 direction = Vector3.zero;

    /// <summary>The attack vector</summary>
    private Vector3 attackVector = Vector3.zero;

    /// <summary>The speed move</summary>
    private int speedMove = 3;

    /// <summary>The radius attack</summary>
    private float radiusAttack = 0.5f;

    /// <summary>The time to close bar</summary>
    private float timeToCloseBar = 10f;

    /// <summary>The time reset</summary>
    private float timeReset = 10f;

    /// <summary>The mobile UI</summary>
    private GameObject mobileUI = null;

    /// <summary>The button a</summary>
    private GameObject buttonA = null;

    /// <summary>The button b</summary>
    private GameObject buttonB = null;

    /// <summary>The joystick</summary>
    private Joystick joystick = null;

    /// <summary>The health</summary>
    private Health health = null;

    /// <summary>The health</summary>
    private Shield shield = null;

    /// <summary>The wallet</summary>
    private Wallet wallet = null;

    /// <summary>The inventory</summary>
    private Inventory inventory = null;

    /// <summary>The pet</summary>
    private Pet pet = null;

    /// <summary>The animator</summary>
    private Animator animator = null;

    /// <summary>The UI animator</summary>
    private Animator uiAnimator = null;

    /// <summary>The rigid body</summary>
    private Rigidbody2D rigbody2D = null;

    /// <summary>Starts this instance.</summary>
    public void Start()
    {
        Game.LoadSettings();
        Game.LoadStats();
        Language.Translate();

        this.animator = this.GetComponent<Animator>();
        this.uiAnimator = this.transform.Find("Interface/Bar").GetComponent<Animator>();
        this.rigbody2D = this.GetComponent<Rigidbody2D>();
        this.health = this.GetComponent<Health>();
        this.shield = this.GetComponent<Shield>();
        this.wallet = this.GetComponent<Wallet>();
        this.inventory = this.GetComponent<Inventory>();
        this.mobileUI = this.transform.Find("Interface/Mobile").gameObject;
        this.joystick = this.transform.Find("Interface/Mobile/Joystick").GetComponent<Joystick>();

        this.buttonA = this.mobileUI.transform.Find("Buttons/ButtonA").gameObject;
        this.buttonB = this.mobileUI.transform.Find("Buttons/ButtonB").gameObject;

        this.mobileUI.SetActive(false);

        this.HasPet();
    }

    /// <summary>Updates this instance.</summary>
    public void Update()
    {
        this.ControlBar();

        if (Settings.Current.Plattform == "Computer")
        {
            if (Input.GetAxisRaw("Horizontal") > 0 || Input.GetAxisRaw("Horizontal") < 0 || Input.GetAxisRaw("Vertical") > 0 || Input.GetAxisRaw("Vertical") < 0)
            {
                this.position = this.rigbody2D.position;
                this.direction.Set(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"), 0);
                this.direction.Normalize();
                this.animator.SetFloat(Horizontal, this.direction.x);
                this.animator.SetFloat(Vertical, this.direction.y);
                this.animator.SetBool(Run, true);
                this.attackVector = this.transform.position + (this.direction / 2);
            }
            else
            {
                this.animator.SetBool(Run, false);
            }

            if (Input.GetKeyDown(KeyCode.Space))
            {
                this.animator.SetTrigger(Skill);
            }

            if (Input.GetKeyDown(KeyCode.E))
            {
                this.AttackAction();
            }
        }

        if (Settings.Current.Plattform == "Xbox")
        {
            if (Input.GetAxisRaw("LeftJoystickX") > 0 || Input.GetAxisRaw("LeftJoystickX") < 0 || Input.GetAxisRaw("LeftJoystickY") > 0 || Input.GetAxisRaw("LeftJoystickY") < 0)
            {
                this.position = this.rigbody2D.position;
                this.direction.Set(Input.GetAxisRaw("LeftJoystickX"), Input.GetAxisRaw("LeftJoystickY"), 0);
                this.direction.Normalize();
                this.animator.SetFloat(Horizontal, this.direction.x);
                this.animator.SetFloat(Vertical, this.direction.y);
                this.animator.SetBool(Run, true);
                this.attackVector = this.transform.position + (this.direction / 2);
            }
            else
            {
                this.animator.SetBool(Run, false);
            }

            if (Input.GetButtonDown("ButtonY"))
            {
                this.animator.SetTrigger(Skill);
            }

            if (Input.GetButtonDown("ButtonA"))
            {
                this.AttackAction();
            }
        }

        if (Settings.Current.Plattform == "Mobile")
        {
            if (!this.mobileUI.activeSelf)
            {
                this.mobileUI.SetActive(true);
                this.buttonA.GetComponent<Button>().onClick.AddListener(() => { this.AttackAction(); });
                this.buttonB.GetComponent<Button>().onClick.AddListener(() => { this.RollAction(); });
            }

            if (this.joystick.Horizontal > 0 || this.joystick.Horizontal < 0 || this.joystick.Vertical > 0 || this.joystick.Vertical < 0)
            {
                this.position = this.rigbody2D.position;
                this.direction.Set(this.joystick.Horizontal, this.joystick.Vertical, 0);
                this.direction.Normalize();
                this.animator.SetFloat(Horizontal, this.direction.x);
                this.animator.SetFloat(Vertical, this.direction.y);
                this.animator.SetBool(Run, true);
                this.attackVector = this.transform.position + (this.direction / 2);
            }
            else
            {
                this.animator.SetBool(Run, false);
            }
        }
    }

    /// <summary>Update every frame.</summary>
    public void FixedUpdate()
    {
        this.rigbody2D.MovePosition(this.position + (this.direction * (this.speedMove * Time.deltaTime)));
    }

    /// <summary>Rolls the action.</summary>
    public void RollAction() 
    {
        this.animator.SetTrigger(Skill);
        return;
    }

    /// <summary>Attacks the action.</summary>
    public void AttackAction()
    {
        Collider2D[] colliders = Physics2D.OverlapCircleAll(this.attackVector, this.radiusAttack, LayerMask.GetMask("Enemy"));

        foreach (Collider2D collider in colliders) 
        {
            if (collider.CompareTag("Enemy")) 
            {
                collider.gameObject.GetComponent<IEnemy>().TakeDamage(50);
            }
        }

        this.animator.SetTrigger(Attack);
       
        return;
    }

    /// <summary>Called when [trigger enter2 d].</summary>
    /// <param name="obj">The object.</param>
    public void OnTriggerEnter2D(Collider2D obj)
    {
        switch (obj.tag)
        {
            case "Coin":
                this.wallet.AddCoin();
                MonoBehaviour.Destroy(obj.gameObject);
                break;

            case "Heart":
                if (this.health.CanAdd(10)) 
                {
                    this.health.Treat(10);
                    MonoBehaviour.Destroy(obj.gameObject);
                }

                break;

            case "Key":
                MonoBehaviour.Destroy(obj.gameObject);
                break;

            case "Pet":
                if (this.pet)
                {
                    this.pet.LeaveOwner();
                    this.pet = null;

                    this.pet = obj.GetComponent<Pet>();
                    this.pet.SetOwner(this.gameObject);
                    Stats.Current.pet = this.pet.GetName();
                    Game.SaveStats();
                }
                else
                {
                    this.pet = obj.GetComponent<Pet>();
                    this.pet.SetOwner(this.gameObject);
                    Stats.Current.pet = this.pet.GetName();
                    Game.SaveStats();
                }

                break;
            case "PotionRed":
                if (this.inventory.HasSpace())
                {
                    this.inventory.AddItem(obj.tag, obj.GetComponent<Icon>().Get());
                    MonoBehaviour.Destroy(obj.gameObject);
                }

                break;
            case "PotionBlue":
                if (this.inventory.HasSpace())
                {
                    this.inventory.AddItem(obj.tag, obj.GetComponent<Icon>().Get());
                    MonoBehaviour.Destroy(obj.gameObject);
                }

                break;
            case "PotionPurple":
                if (this.inventory.HasSpace())
                {
                    this.inventory.AddItem(obj.tag, obj.GetComponent<Icon>().Get());
                    MonoBehaviour.Destroy(obj.gameObject);
                }

                break;
            case "PotionYellow":
                if (this.inventory.HasSpace())
                {
                    this.inventory.AddItem(obj.tag, obj.GetComponent<Icon>().Get());
                    MonoBehaviour.Destroy(obj.gameObject);
                }

                break;
        }
    }

    /// <summary>Sets the position.</summary>
    public void SetPosition() 
    {
        this.position = new Vector3(250, 250, 0);
        this.position.Set(250, 250, 0);
    }

    /// <summary>Determines whether this instance has pet.</summary>
    private void HasPet() 
    {
        if (Stats.Current.pet != string.Empty) 
        {
            GameObject obj = Resources.Load<GameObject>("Pets/" + Stats.Current.pet);
            GameObject petSpawn = Instantiate(obj, this.gameObject.transform.position, Quaternion.identity);
            petSpawn.GetComponent<Pet>().SetOwner(this.gameObject);
        }
    }

    /// <summary>Controls the bar.</summary>
    private void ControlBar() 
    {
        if (Input.anyKey || Input.touches.Length > 0)
        {
            this.uiAnimator.SetBool(Open, true);
            this.timeToCloseBar = this.timeReset;
        }
        else
        {
            this.timeToCloseBar -= Time.deltaTime;
            if (this.timeToCloseBar <= 0)
            {
                this.timeToCloseBar = this.timeReset;
                this.uiAnimator.SetBool(Open, false);
            }
        }
    }

    /// <summary>Called when [draw gizmos selected].</summary>
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(this.attackVector, this.radiusAttack);
    }
}