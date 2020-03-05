//------------------------------------------------------------------------------------------
// <author>Pablo Perdomo Falcón</author>
// <copyright file="Player.cs" company="Pabllopf">GNU General Public License v3.0</copyright>
//------------------------------------------------------------------------------------------
using System.Collections;
using Mirror;
using UnityEngine;
using UnityEngine.UI;

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

    /// <summary>The is attacking</summary>
    private bool isAttacking = false;

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

    /// <summary>Button B</summary>
    public void ButtonA()
    {
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
            
            animator = GetComponent<Animator>();
            animator.runtimeAnimatorController = TypePlayer.Controller;

            joystick = transform.Find("Interface/Mobile/Joystick").GetComponent<Joystick>();

            transform.Find("Interface/Mobile/Buttons/ButtonA").GetComponent<Button>().onClick.AddListener(() => { ButtonA(); });
            transform.Find("Interface/Mobile/Buttons/ButtonB").GetComponent<Button>().onClick.AddListener(() => { ButtonB(); });

            mobileUI = transform.Find("Interface/Mobile").gameObject;
            mobileUI.SetActive(Settings.Current.Platform.Equals("Mobile") ? true : false);

            position = NetworkManager.startPositions[0].transform.position;

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
        }
        else
        {
            animator.SetBool(Run, false);
        }
    }

    /// <summary>Fixed the update.</summary>
    private void FixedUpdate()
    {
        if (isLocalPlayer)
        {
            rigbody2D.MovePosition(position + (direction * (TypePlayer.SpeedOfMovement * Time.fixedDeltaTime)));
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

        global::Attack.Invoke(TypePlayer.Attack).OfThis(gameObject);

        yield return new WaitForSeconds(TypePlayer.FrequencyToAttack);
        isAttacking = false;
    }
}