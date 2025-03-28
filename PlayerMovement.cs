using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float speed = 5f;      // Velocidad de movimiento
    public float jumpForce = 7f;  // Fuerza del salto
    private Rigidbody2D rb;
    private bool isGrounded;
    private Animator animatorPlayer;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animatorPlayer = GetComponent<Animator>();
    }

    void Update()
    {
        // Movimiento lateral
        float moveX = Input.GetAxisRaw("Horizontal");
        rb.linearVelocity = new Vector2(moveX * speed, rb.linearVelocity.y);
        animatorPlayer.SetFloat("movement", Mathf.Abs(Input.GetAxisRaw("Horizontal")));
        if (Input.GetAxisRaw("Horizontal") != 0)
        {
            transform.localScale = new Vector3(Input.GetAxisRaw("Horizontal"), 1, 1);
        }
        // Salto
        if (Input.GetKeyDown(KeyCode.Space) && isGrounded)
        {
            rb.linearVelocity = new Vector2(rb.linearVelocity.x, jumpForce);
            isGrounded = false; // Evita saltos dobles
        }
        animatorPlayer.SetBool("isGrounded", isGrounded);
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        // Si tocamos el suelo, permitimos volver a saltar
        if (collision.gameObject.name == "Plataforma")
        {
            isGrounded = true;
        }
    }
}
