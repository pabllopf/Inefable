using UnityEngine;

[RequireComponent(typeof(CircleCollider2D))]
[RequireComponent(typeof(SpriteRenderer))]
[RequireComponent(typeof(BoxCollider2D))]
[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(Occlusion))]
[RequireComponent(typeof(Animator))]
public class Witch : MonoBehaviour
{
    private int health = 100;

    public GameObject suicide;
    private float frecuencySpawn = 4f;
    private static readonly float resetSpawn = 4f;
    private static readonly int numMaxSuicides = 3;

    private Transform target;
    private Vector3 direction;

    private Animator animator;
    private Rigidbody2D rigid2D;
    private BoxCollider2D boxCollider2D;
    private CircleCollider2D circleCollider2D;

    private static readonly float speed = 1f;
    private static readonly float visionRadio = 6f;
    private static readonly float attackRadio = 5f;

    private static readonly string walk = "Walk";
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

        if (DistanceToTarget() <= attackRadio) { AttactToTarget(); return; }

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
        rigid2D.isKinematic = false;

        direction = target.position - this.transform.position;
        direction.Normalize();

        animator.SetFloat(horizontal, direction.x);
        animator.SetFloat(vertical, direction.y);

        animator.SetBool(walk, true);
        animator.SetBool(attack, false);
    }

    private void AttactToTarget()
    {
        rigid2D.isKinematic = true;

        direction = Vector3.zero;

        animator.SetBool(attack, true);
        animator.SetBool(walk, false);

        if (frecuencySpawn > 0) { frecuencySpawn -= Time.deltaTime; return; }

        frecuencySpawn = resetSpawn;
        for (int i = 0; i < Random.Range(1,numMaxSuicides);i++) 
        {
            Instantiate(suicide, transform.position + new Vector3(Random.Range(-0.6f,0.6f), Random.Range(-0.6f, 0.6f), 0), Quaternion.identity);
        }
    }

    private void FixedUpdate()
    {
        rigid2D.MovePosition(this.transform.position + direction * speed * Time.deltaTime);
    }

    public void TakeDamage(int amount)
    {
        health -= amount;
        if (health <= 0) { Die(); }
    }

    private void Die()
    {
        // add spawn of ramdom objects
        Destroy(this.gameObject);
    }
}
