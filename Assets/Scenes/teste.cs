using UnityEngine;

public class teste : MonoBehaviour
{
    public float moveSpeed = 5f;     // Velocidade horizontal
    public float jumpForce = 12f;    // Força do pulo

    private Rigidbody2D rb;
    private bool canJump;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        // Movimento horizontal
        float move = Input.GetAxis("Horizontal");
        rb.linearVelocity = new Vector2(move * moveSpeed, rb.linearVelocity.y);

        // Permite pular somente se a velocidade vertical for 0 (tocando o chão)
        canJump = Mathf.Abs(rb.linearVelocity.y) < 0.01f;

        if (Input.GetButtonDown("Jump") && canJump)
        {
            rb.linearVelocity = new Vector2(rb.linearVelocity.x, jumpForce);
        }
    }
}
