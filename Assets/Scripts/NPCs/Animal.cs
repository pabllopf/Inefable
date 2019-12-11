using UnityEngine;

public class Animal : MonoBehaviour
{
    /// <summary>The Vertical</summary>
    private static readonly string Vertical = "Vertical";

    /// <summary>The Horizontal</summary>
    private static readonly string Horizontal = "Horizontal";

    /// <summary>The animator</summary>
    private Animator animator;

    private int direction;
    private int currentDirection;
    private float speed = 0.5f;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        direction = Random.Range(0, 8);
        if (direction != currentDirection)
        {
            currentDirection = direction;
        }
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        direction = Random.Range(0, 8);
        if (direction != currentDirection)
        {
            currentDirection = direction;
        }
    }

    /// <summary>Starts this instance.</summary>
    private void Start()
    {
        this.animator = this.GetComponent<Animator>();
        this.currentDirection = Random.Range(0, 8);
    }


    private void Update()
    {
        if (currentDirection == 0) 
        {
            this.transform.position = Vector3.MoveTowards(this.transform.position, this.transform.position + new Vector3(0, 0.2f, 0), speed * Time.deltaTime);
            this.animator.SetFloat(Horizontal, 0);
            this.animator.SetFloat(Vertical, 1);
        }

        if (currentDirection == 1)
        {
            this.transform.position = Vector3.MoveTowards(this.transform.position, this.transform.position + new Vector3(0, -0.2f, 0), speed * Time.deltaTime);
            this.animator.SetFloat(Horizontal, 0);
            this.animator.SetFloat(Vertical, -1);
        }

        if (currentDirection == 2)
        {
            this.transform.position = Vector3.MoveTowards(this.transform.position, this.transform.position + new Vector3(0.2f, 0, 0), speed * Time.deltaTime);
            this.animator.SetFloat(Horizontal, 1);
            this.animator.SetFloat(Vertical, 0);
        }

        if (currentDirection == 3)
        {
            this.transform.position = Vector3.MoveTowards(this.transform.position, this.transform.position + new Vector3(-0.2f, 0, 0), speed * Time.deltaTime);
            this.animator.SetFloat(Horizontal, -1);
            this.animator.SetFloat(Vertical, 0);
        }
        if (currentDirection == 4)
        {
            this.transform.position = Vector3.MoveTowards(this.transform.position, this.transform.position + new Vector3(-0.2f, 0.2f, 0), speed * Time.deltaTime);
            this.animator.SetFloat(Horizontal, -1);
            this.animator.SetFloat(Vertical, 0);
        }

        if (currentDirection == 5)
        {
            this.transform.position = Vector3.MoveTowards(this.transform.position, this.transform.position + new Vector3(0.2f, -0.2f, 0), speed * Time.deltaTime);
            this.animator.SetFloat(Horizontal, 1);
            this.animator.SetFloat(Vertical, 0);
        }

        if (currentDirection == 6)
        {
            this.transform.position = Vector3.MoveTowards(this.transform.position, this.transform.position + new Vector3(0.2f, 0.2f, 0), speed * Time.deltaTime);
            this.animator.SetFloat(Horizontal, 1);
            this.animator.SetFloat(Vertical, 0);
        }

        if (currentDirection == 7)
        {
            this.transform.position = Vector3.MoveTowards(this.transform.position, this.transform.position + new Vector3(-0.2f, -0.2f, 0), speed * Time.deltaTime);
            this.animator.SetFloat(Horizontal, -1);
            this.animator.SetFloat(Vertical, 0);
        }
        
    }
}
