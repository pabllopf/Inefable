using UnityEngine;
using System.Collections;

[RequireComponent(typeof(SpriteRenderer))]
[RequireComponent(typeof(BoxCollider2D))]
[RequireComponent(typeof(CircleCollider2D))]
[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(Animator))]
[RequireComponent(typeof(Occlusion))]
public class Suicide : MonoBehaviour
{
    private Transform target;
    private Vector3 direction;

    private Animator animator;
    private Rigidbody2D rigid2D;
    private BoxCollider2D boxCollider2D;
    private CircleCollider2D circleCollider2D;

    private static readonly float speed = 2f;
    private static readonly float visionRadio = 6f;
    private static readonly float attackRadio = 0.8f;
    private static readonly float boomRadio = 0.5f;

    private static readonly string attack = "Attack";
    private static readonly string vertical = "Vertical";
    private static readonly string horizontal = "Horizontal";

    private void Start()
    {
        animator = GetComponent<Animator>();
        rigid2D = GetComponent<Rigidbody2D>();
        boxCollider2D = GetComponent<BoxCollider2D>();
        circleCollider2D = GetComponent<CircleCollider2D>();

        rigid2D.isKinematic = false;
        rigid2D.simulated = true;
        boxCollider2D.isTrigger = false;
        circleCollider2D.isTrigger = true;
        circleCollider2D.radius = visionRadio;
    }

    private void OnTriggerStay2D(Collider2D collider2D)
    {
        if (collider2D.CompareTag("Player"))
        {
            target = collider2D.transform;
        }
    }

    private void Update()
    {
        if (!target) { return; }

        if (DistanceToTarget() > visionRadio) { HasNotTarget(); return; }

        if (DistanceToTarget() <= attackRadio){ StartCoroutine(AttactToTarget()); return; }

        FollowTarget();
    }

    private float DistanceToTarget()
    {
        return Vector2.Distance(this.transform.position, target.position);
    }

    private void HasNotTarget()
    {
        target = null;
        direction = Vector3.zero;
    }

    private void FollowTarget()
    {
        direction = target.position - this.transform.position;
        direction.Normalize();

        animator.SetFloat(horizontal, direction.x);
        animator.SetFloat(vertical, direction.y);
    }

    private IEnumerator AttactToTarget()
    {
        animator.SetBool(attack, true);
        direction = Vector3.zero;
        boxCollider2D.enabled = false;
        circleCollider2D.enabled = false;
        
        yield return new WaitForSeconds((animator.GetCurrentAnimatorStateInfo(0).length));
        
        if (DistanceToTarget() <= attackRadio + boomRadio) { target.GetComponent<Health>().Take(5); }
        Destroy(this.gameObject);
    }

    private void FixedUpdate()
    {
        rigid2D.MovePosition(this.transform.position + direction * speed * Time.deltaTime);
    }

    public void TakeDamage() 
    {
        Destroy(this.gameObject);
    }

}
