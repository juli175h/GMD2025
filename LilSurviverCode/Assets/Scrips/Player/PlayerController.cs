using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    public InputAction MoveAction;
    private Vector2 moveInput;
    public Animator animator;

    [HideInInspector]
    public bool isFacingRight = true;
    [HideInInspector]
    public bool hasImmunity = false;

    public float movementSpeed = 6.0f;
    private Rigidbody2D rb;

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
        MoveAction.Enable();
    }

    // Update is called once per frame
    void Update()
    {
        moveInput = MoveAction.ReadValue<Vector2>();
        animator.SetFloat("Speed", Mathf.Abs(moveInput.x) + Mathf.Abs(moveInput.y));

        if (moveInput.x > 0 && !isFacingRight)
        {
            FlipPlayer();
        }
        else if (moveInput.x < 0 && isFacingRight)
        {
            FlipPlayer();
        }
    }

    void FixedUpdate()
    {
        rb.MovePosition(rb.position + moveInput * movementSpeed * Time.fixedDeltaTime);
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        Debug.Log("trigger exp"+ other.name);
        if (other.CompareTag("Exp"))
        {
            Debug.Log("trigger exp");
            gameObject.GetComponent<PlayerXp>().GainXp(other.gameObject.GetComponent<ExpScript>().ExpValue);
            Destroy(other.gameObject);
        }
    }
    void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Exp"))
        {
            Debug.Log("Collision exp");

            gameObject.GetComponent<PlayerXp>().GainXp(other.gameObject.GetComponent<ExpScript>().ExpValue);
            Destroy(other.gameObject);
        }
        if (other.gameObject.CompareTag("Enemy"))
        {
            if (!hasImmunity)
                gameObject.GetComponent<PlayerHealth>().TakeDamage(other.gameObject.GetComponent<EnemyController>().Damage);
        }
    }

    void FlipPlayer()
    {
        isFacingRight = !isFacingRight;
        Vector3 scale = transform.localScale;
        scale.x *= -1; 
        transform.localScale = scale;
    }


}
