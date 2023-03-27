using UnityEngine;

public class scrt : MonoBehaviour
{
    public float speed = 5f; // Velocidad de movimiento
    public float jumpForce = 10f; // Fuerza de salto
    public float groundCheckDistance = 0.1f; // Distancia a la que se comprueba si el personaje está en el suelo
    public LayerMask groundMask; // Capa del suelo

    private Rigidbody2D rb;
    private Animator animator;
    private bool isGrounded;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
    }

    private void Update()
    {
        // Comprobar si el personaje está en el suelo
        isGrounded = Physics2D.Raycast(transform.position, Vector2.down, groundCheckDistance, groundMask);

        // Mover el personaje
        float move = Input.GetAxis("Horizontal");
        rb.velocity = new Vector2(move * speed, rb.velocity.y);

        // Girar el personaje en la dirección del movimiento
        if (move > 0)
        {
            transform.localScale = new Vector3(1, 1, 1);
        }
        else if (move < 0)
        {
            transform.localScale = new Vector3(-1, 1, 1);
        }

        // Animar el personaje
        animator.SetFloat("Speed", Mathf.Abs(move));

        // Saltar si el personaje está en el suelo y se pulsa la tecla de salto
        if (isGrounded && Input.GetKeyDown(KeyCode.Space))
        {
            rb.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        // Detener el movimiento si el personaje colisiona con una pared
        if (collision.gameObject.tag == "Wall")
        {
            rb.velocity = Vector2.zero;
        }
    }
}