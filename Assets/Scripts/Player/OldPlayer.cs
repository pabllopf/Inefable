//------------------------------------------------------------------------------------------
// <author>Pablo Perdomo Falcón</author>
// <copyright file="Player.cs" company="Pabllopf">GNU General Public License v3.0</copyright>
//------------------------------------------------------------------------------------------
using Mirror;
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
public class OldPlayer : NetworkBehaviour
{
    /// <summary>The open</summary>
    private const string Open = "Open";

    /// <summary>The Wait</summary>
    private const string Wait = "Wait";

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
    private readonly int speedMove = 3;

    /// <summary>The frecuency to attack</summary>
    private readonly float frecuencyToAttack = 0.20f;

    /// <summary>The frecuency to roll</summary>
    private readonly float frecuencyToUseSkill = 0.75f;

    /// <summary>The attacking</summary>
    private bool attacking = false;

    /// <summary>The use skill</summary>
    private bool useSkill = false;

    /// <summary>The radius attack</summary>
    private readonly float radiusAttack = 0.5f;

    /// <summary>The time to close bar</summary>
    private float timeToCloseBar = 10f;

    /// <summary>The time reset</summary>
    private readonly float timeReset = 10f;

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

    /// <summary>The key pack</summary>
    private KeyPack keyPack = null;

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


    public override void OnStartLocalPlayer()
    {
        base.OnStartLocalPlayer();

        transform.position = new Vector2(255, 255);
        position = new Vector2(255, 255);
        animator = GetComponent<Animator>();
        uiAnimator = transform.Find("Interface/Bar").GetComponent<Animator>();
        rigbody2D = GetComponent<Rigidbody2D>();
        health = GetComponent<Health>();
        shield = GetComponent<Shield>();
        wallet = GetComponent<Wallet>();
        keyPack = GetComponent<KeyPack>();
        inventory = GetComponent<Inventory>();
        mobileUI = transform.Find("Interface/Mobile").gameObject;
        joystick = transform.Find("Interface/Mobile/Joystick").GetComponent<Joystick>();

        buttonA = mobileUI.transform.Find("Buttons/ButtonA").gameObject;
        buttonB = mobileUI.transform.Find("Buttons/ButtonB").gameObject;

        mobileUI.SetActive(false);
    }

    private void OnDisable()
    {
        if (isLocalPlayer)
        {
        }
    }


    /// <summary>Starts this instance.</summary>
    /*public void Start()
    {
        if (!isLocalPlayer) { return; }
        //Game.LoadSettings();
        //Game.LoadStats();
        //Language.Translate();
        //this.transform.position = new Vector2(255, 255);
        //this.position = new Vector2(255, 255);
        //Instantiate(mainCamera, new Vector2(255, 255), Quaternion.identity);

        this.animator = this.GetComponent<Animator>();
        this.walkEffect = this.transform.Find("WalkEffect").GetComponent<WalkEffect>();
        this.uiAnimator = this.transform.Find("Interface/Bar").GetComponent<Animator>();
        this.rigbody2D = this.GetComponent<Rigidbody2D>();
        this.health = this.GetComponent<Health>();
        this.shield = this.GetComponent<Shield>();
        this.wallet = this.GetComponent<Wallet>();
        this.keyPack = this.GetComponent<KeyPack>();
        this.inventory = this.GetComponent<Inventory>();
        this.mobileUI = this.transform.Find("Interface/Mobile").gameObject;
        this.joystick = this.transform.Find("Interface/Mobile/Joystick").GetComponent<Joystick>();

        this.buttonA = this.mobileUI.transform.Find("Buttons/ButtonA").gameObject;
        this.buttonB = this.mobileUI.transform.Find("Buttons/ButtonB").gameObject;

        this.mobileUI.SetActive(false);

        this.HasPet();
    }*/

    /// <summary>Updates this instance.</summary>
    public void Update()
    {
        if (!isLocalPlayer) { return; }

        ControlBar();

        if (Settings.Current.Platform == "Computer")
        {
            if (Input.GetAxisRaw("Horizontal") > 0 || Input.GetAxisRaw("Horizontal") < 0 || Input.GetAxisRaw("Vertical") > 0 || Input.GetAxisRaw("Vertical") < 0)
            {
                position = rigbody2D.position;
                direction.Set(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"), 0);
                direction.Normalize();
                animator.SetFloat(Horizontal, direction.x);
                animator.SetFloat(Vertical, direction.y);
                animator.SetBool(Run, true);
                attackVector = transform.position + (direction / 4);
            }
            else
            {
                animator.SetBool(Run, false);
            }

            if (Input.GetKeyDown(KeyCode.Q))
            {
                animator.SetTrigger(Skill);
            }

            if (Input.GetKeyDown(KeyCode.E))
            {
                if (!attacking)
                {
                    StartCoroutine(AttackNow());
                }
            }
        }

        if (Settings.Current.Platform == "Xbox")
        {
            if (Input.GetAxisRaw("LeftJoystickX") > 0 || Input.GetAxisRaw("LeftJoystickX") < 0 || Input.GetAxisRaw("LeftJoystickY") > 0 || Input.GetAxisRaw("LeftJoystickY") < 0)
            {
                position = rigbody2D.position;
                direction.Set(Input.GetAxisRaw("LeftJoystickX"), Input.GetAxisRaw("LeftJoystickY"), 0);
                direction.Normalize();
                animator.SetFloat(Horizontal, direction.x);
                animator.SetFloat(Vertical, direction.y);
                animator.SetBool(Run, true);
                attackVector = transform.position + (direction / 4);
            }
            else
            {
                animator.SetBool(Run, false);
            }

            if (Input.GetButtonDown("ButtonY"))
            {
                animator.SetTrigger(Skill);
            }

            if (Input.GetButtonDown("ButtonA"))
            {
                if (!attacking)
                {
                    StartCoroutine(AttackNow());
                }
            }
        }

        if (Settings.Current.Platform == "Mobile")
        {
            if (!mobileUI.activeSelf)
            {
                mobileUI.SetActive(true);
                buttonA.GetComponent<Button>().onClick.AddListener(() => { AttackAction(); });
                buttonB.GetComponent<Button>().onClick.AddListener(() => { SkillAction(); });
            }

            if (joystick.Horizontal > 0 || joystick.Horizontal < 0 || joystick.Vertical > 0 || joystick.Vertical < 0)
            {
                position = rigbody2D.position;
                direction.Set(joystick.Horizontal, joystick.Vertical, 0);
                direction.Normalize();
                animator.SetFloat(Horizontal, direction.x);
                animator.SetFloat(Vertical, direction.y);
                animator.SetBool(Run, true);
                attackVector = transform.position + (direction / 4);
            }
            else
            {
                animator.SetBool(Run, false);
            }
        }
    }

    /// <summary>Update every frame.</summary>
    public void FixedUpdate()
    {
        if (!isLocalPlayer) { return; }
        rigbody2D.MovePosition(position + (direction * (speedMove * Time.deltaTime)));
    }

    /// <summary>Rolls the action.</summary>
    public void SkillAction()
    {
        if (!useSkill)
        {
            StartCoroutine(SkillNow());
        }

    }

    /// <summary>Attacks the action.</summary>
    public void AttackAction()
    {
        if (!attacking)
        {
            StartCoroutine(AttackNow());
        }
    }

    /// <summary>Attacks the now.</summary>
    /// <returns>Return none</returns>
    public IEnumerator AttackNow()
    {
        Collider2D[] colliders = Physics2D.OverlapCircleAll(attackVector, radiusAttack, LayerMask.GetMask("Enemy"));

        foreach (Collider2D collider in colliders)
        {
            if (collider.CompareTag("Enemy"))
            {
                collider.gameObject.GetComponent<IEnemy>().TakeDamage(Random.Range(5, 15));
            }


        }

        animator.SetTrigger(Attack);

        attacking = true;
        yield return new WaitForSeconds(frecuencyToAttack);
        attacking = false;
    }

    private IEnumerator SkillNow()
    {
        animator.SetTrigger(Skill);
        gameObject.layer = LayerMask.NameToLayer("IgnoreAttack");

        //this.GetComponent<CapsuleCollider2D>().enabled = false;

        useSkill = true;
        yield return new WaitForSeconds(frecuencyToUseSkill);
        useSkill = false;
        //this.GetComponent<CapsuleCollider2D>().enabled = true;
        gameObject.layer = LayerMask.NameToLayer("Player");
    }

    /*
    /// <summary>Called when [trigger enter2 d].</summary>
    /// <param name="obj">The object.</param>
    public void OnTriggerEnter2D(Collider2D obj)
    {
        switch (obj.tag)
        {
            case "Coin":
                wallet.AddCoin();
                MonoBehaviour.Destroy(obj.gameObject);
                break;

            case "Heart":
                health.Treat(10);
                MonoBehaviour.Destroy(obj.gameObject);

                break;

            case "Key":
                keyPack.AddKey();
                MonoBehaviour.Destroy(obj.gameObject);
                break;

            case "Pet":
                if (pet)
                {
                    pet.LeaveOwner();
                    pet = null;

                    pet = obj.GetComponent<Pet>();
                    pet.SetOwner(gameObject);
                    Stats.Current.pet = pet.Name;
                }
                else
                {
                    pet = obj.GetComponent<Pet>();
                    pet.SetOwner(gameObject);
                    Stats.Current.pet = pet.Name;
                }

                break;
            case "PotionRed":
                if (inventory.HasSpace())
                {
                    inventory.AddItem(obj.tag, obj.GetComponent<Icon>().Sprite);
                    MonoBehaviour.Destroy(obj.gameObject);
                }

                break;
            case "PotionBlue":
                if (inventory.HasSpace())
                {
                    inventory.AddItem(obj.tag, obj.GetComponent<Icon>().Sprite);
                    MonoBehaviour.Destroy(obj.gameObject);
                }

                break;
            case "PotionPurple":
                if (inventory.HasSpace())
                {
                    inventory.AddItem(obj.tag, obj.GetComponent<Icon>().Sprite);
                    MonoBehaviour.Destroy(obj.gameObject);
                }

                break;
            case "PotionYellow":
                if (inventory.HasSpace())
                {
                    inventory.AddItem(obj.tag, obj.GetComponent<Icon>().Sprite);
                    MonoBehaviour.Destroy(obj.gameObject);
                }

                break;
        }
    }*/

    /// <summary>Determines whether this instance has pet.</summary>
    private void HasPet()
    {
        if (Stats.Current.pet != string.Empty)
        {
            GameObject obj = Resources.Load<GameObject>("Pets/" + Stats.Current.pet);
            GameObject petSpawn = Instantiate(obj, gameObject.transform.position, Quaternion.identity);
            petSpawn.GetComponent<Pet>().SetOwner(gameObject);
        }
    }

    /// <summary>Controls the bar.</summary>
    private void ControlBar()
    {
        if (Input.anyKey || Input.touches.Length > 0 || Input.GetAxisRaw("LeftJoystickX") != 0)
        {
            uiAnimator.SetBool(Open, true);
            timeToCloseBar = timeReset;
        }
        else
        {
            timeToCloseBar -= Time.deltaTime;
            if (timeToCloseBar <= 0)
            {
                timeToCloseBar = timeReset;
                uiAnimator.SetBool(Open, false);
                animator.SetTrigger(Wait);
            }
        }
    }

    /// <summary>Called when [draw gizmos selected].</summary>
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(attackVector, radiusAttack);
    }
}