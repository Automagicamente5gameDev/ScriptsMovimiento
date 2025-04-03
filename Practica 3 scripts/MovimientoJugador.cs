using UnityEngine;

public class MovimientoJugador : MonoBehaviour
{
    Rigidbody2D rb;
    bool isGrounded;
    Animator animationPlayer;
    private bool bajoAtaque = false;
    private int vidas = 3;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animationPlayer = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (!bajoAtaque) {
            if (Input.GetKeyDown(KeyCode.Space) && isGrounded) {
                rb.linearVelocityY = 5f;
                isGrounded = false;
            } else if (Input.GetAxis("Horizontal") != 0 && Input.GetAxisRaw("Horizontal") != 0) {
                rb.linearVelocityX = 5f * Input.GetAxis("Horizontal");

                transform.localScale = new Vector3(Input.GetAxisRaw("Horizontal"), 1, 1);
            }
        }
        animationPlayer.SetFloat("movement", Mathf.Abs(Input.GetAxis("Horizontal")));
        animationPlayer.SetBool("isGrounded", isGrounded);
    }

    // Detecta si el jugador est� tocando el suelo
    void OnCollisionEnter2D(Collision2D collision) {
        if (collision.collider.name == "Plataforma") // Aseg�rate de que el suelo tenga el tag "Ground"
        {
            isGrounded = true;
            bajoAtaque = false;
        }
    }

    public void serAtacado(Vector2 empuje) {
        bajoAtaque = true;
        rb.linearVelocity = empuje;
        vidas--;
        if (vidas <=0) {
            Destroy(gameObject);
        }
    }
}
