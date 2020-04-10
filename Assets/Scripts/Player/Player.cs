//------------------------------------------------------------------------------------------
// <author>Pablo Perdomo Falcón</author>
// <copyright file="Player.cs" company="Pabllopf">GNU General Public License v3.0</copyright>
//------------------------------------------------------------------------------------------
using System.Collections;
using Mirror;
using UnityEngine;
using UnityEngine.UI;
using Utils;

/// <summary>Manage the player of the game.</summary>
public class Player : NetworkBehaviour
{
    /// <summary>The run</summary>
    private const string Run = "Run";

    /// <summary>The vertical</summary>
    private const string Vertical = "Vertical";

    /// <summary>The horizontal</summary>
    private const string Horizontal = "Horizontal";

    /// <summary>The type player</summary>
    [SerializeField]
    private PlayerType typePlayer = null;

    /// <summary>The speed of move</summary>
    private float speedOfMove = 0;

    /// <summary>The damage of attack</summary>
    private int damageOfAttack = 0;

    /// <summary>The is attacking</summary>
    private bool isAttacking = false;

    /// <summary>The press button a</summary>
    private bool pressButtonA = false;

    /// <summary>The can speak</summary>
    private bool canSpeak = false;

    /// <summary>The is using skill</summary>
    private bool isUsingSkill = false;

    /// <summary>The position</summary>
    private Vector3 position = Vector3.zero;

    /// <summary>The direction</summary>
    private Vector3 direction = Vector3.zero;

    /// <summary>The attack vector</summary>
    private Vector3 attackVector = Vector3.zero;

    /// <summary>The mobile UI</summary>
    private GameObject mobileUI = null;

    /// <summary>The walk effect</summary>
    private GameObject walkEffect = null;

    /// <summary>The joystick</summary>
    private Joystick joystick = null;

    /// <summary>The Rigid body 2D</summary>
    private Rigidbody2D rigbody2D = null;

    /// <summary>The animator</summary>
    private Animator animator = null;

    /// <summary>Gets or sets the type player.</summary>
    /// <value>The type player.</value>
    public PlayerType TypePlayer { get => typePlayer; set => typePlayer = value; }

    /// <summary>Gets or sets the animator.</summary>
    /// <value>The animator.</value>
    public Animator Animator { get => animator; set => animator = value; }

    /// <summary>Gets or sets the attack vector.</summary>
    /// <value>The attack vector.</value>
    public Vector3 AttackVector { get => attackVector; set => attackVector = value; }

    /// <summary>Gets the axis x.</summary>
    /// <value>The axis x.</value>
    private float AxisX => Input.GetAxisRaw("Horizontal") + Input.GetAxisRaw("LeftJoystickX") + joystick.Horizontal;

    /// <summary>Gets the axis y.</summary>
    /// <value>The axis y.</value>
    private float AxisY => Input.GetAxisRaw("Vertical") + Input.GetAxisRaw("LeftJoystickY") + joystick.Vertical;

    /// <summary>Gets or sets the speed of move.</summary>
    /// <value>The speed of move.</value>
    public float SpeedOfMove { get => speedOfMove; set => speedOfMove = value; }
    
    /// <summary>Gets or sets the damage of attack.</summary>
    /// <value>The damage of attack.</value>
    public int DamageOfAttack { get => damageOfAttack; set => damageOfAttack = value; }
    
    /// <summary>Gets or sets a value indicating whether this instance can speak.</summary>
    /// <value>
    /// <c>true</c> if this instance can speak; otherwise, <c>false</c>.</value>
    public bool CanSpeak { get => canSpeak; set => canSpeak = value; }

    /// <summary>Gets or sets a value indicating whether [press button a].</summary>
    /// <value>
    /// <c>true</c> if [press button a]; otherwise, <c>false</c>.</value>
    public bool PressButtonA { get => pressButtonA; set => pressButtonA = value; }

    /// <summary>Button B</summary>
    public void ButtonA()
    {
        if (canSpeak) 
        {
            pressButtonA = true;
            return;
        }

        if (!isAttacking)
        {
            StartCoroutine(AttackNow());
        }
    }

    /// <summary>Button B.</summary>
    public void ButtonB()
    {
        if (!isUsingSkill)
        {
            StartCoroutine(SkillNow());
        }
    }

    /// <summary>Stops the player.</summary>
    public void StopPlayer() 
    {
        walkEffect.SetActive(false);
        animator.SetBool(Run, false);
        this.enabled = false;
    }

    /// <summary>Awakes this instance.</summary>
    private void Awake()
    {
        Settings.Load();
        Language.Translate();
    }

    /// <summary>Starts this instance.</summary>
    private void Start()
    {
        if (isLocalPlayer)
        {
            rigbody2D = GetComponent<Rigidbody2D>();

            walkEffect = transform.Find("WalkEffect").gameObject;

            animator = GetComponent<Animator>();
            animator.runtimeAnimatorController = TypePlayer.Controller;

            joystick = transform.Find("Interface/Mobile/Joystick").GetComponent<Joystick>();

            transform.Find("Interface/Mobile/Buttons/ButtonA").GetComponent<Button>().onClick.AddListener(() => { ButtonA(); });
            transform.Find("Interface/Mobile/Buttons/ButtonB").GetComponent<Button>().onClick.AddListener(() => { ButtonB(); });

            mobileUI = transform.Find("Interface/Mobile").gameObject;
            mobileUI.SetActive(Settings.Current.Platform.Equals("Mobile") ? true : false);

            position = NetworkManager.startPositions[0].transform.position;

            speedOfMove = typePlayer.SpeedOfMovement;
            damageOfAttack = typePlayer.Damage;

            SetUpPlayerCamera();

        }
    }

    /// <summary>Sets up player camera.</summary>
    private void SetUpPlayerCamera()
    {
        GameObject gameObject = new GameObject("Camera");
        gameObject.transform.position = position;

        gameObject.AddComponent<Camera>();
        gameObject.AddComponent<Follower>();

        Follower follower = gameObject.GetComponent<Follower>();
        follower.Target = transform;
    }

    /// <summary>Updates this instance.</summary>
    private void Update()
    {
        if (isLocalPlayer)
        {
            if (Settings.Current.Platform.Equals("Mobile")) 
            {
                if (!mobileUI.activeSelf) 
                {
                    mobileUI.SetActive(true);
                }
            }

            Move(AxisX, AxisY);

            if (Input.GetKeyDown(KeyCode.E) || Input.GetButtonDown("ButtonA"))
            {
                ButtonA();
                return;
            }

            if (Input.GetKeyDown(KeyCode.Space) || Input.GetButtonDown("ButtonY"))
            {
                ButtonB();
                return;
            }

            return;
        }
    }

    /// <summary>Moves the specified horizontal.</summary>
    /// <param name="horizontal">The horizontal.</param>
    /// <param name="vertical">The vertical.</param>
    private void Move(float horizontal, float vertical)
    {
        direction.Set(horizontal, vertical, 0);
        direction.Normalize();

        if (direction != Vector3.zero)
        {
            position = rigbody2D.position;

            animator.SetFloat(Horizontal, direction.x);
            animator.SetFloat(Vertical, direction.y);
            animator.SetBool(Run, true);

            walkEffect.SetActive(true);
        }
        else
        {
            walkEffect.SetActive(false);
            animator.SetBool(Run, false);
        }
    }

    /// <summary>Fixed the update.</summary>
    private void FixedUpdate()
    {
        if (isLocalPlayer)
        {
            rigbody2D.MovePosition(position + (direction * (speedOfMove * Time.fixedDeltaTime)));
        }
    }

    /// <summary>Skills the now.</summary>
    /// <returns>Return none</returns>
    private IEnumerator SkillNow()
    {
        isUsingSkill = true;

        global::Skill.Invoke(TypePlayer.Skill).OfThis(gameObject);

        yield return new WaitForSeconds(TypePlayer.FrequencyToUseSkill);
        isUsingSkill = false;
    }

    /// <summary>Attacks the now.</summary>
    /// <returns>Return none</returns>
    private IEnumerator AttackNow()
    {
        isAttacking = true;

        attackVector = this.transform.position + (this.direction / 3);

        global::Attack.Invoke(TypePlayer.Attack).OfThis(gameObject);

        yield return new WaitForSeconds(TypePlayer.FrequencyToAttack);
        isAttacking = false;
    }

    #region Gizmos Selected

    /// <summary>Called when [draw gizmos selected].</summary>
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(this.transform.position + (this.direction / 3), TypePlayer.RadiusAttack);
    }

    #endregion
}