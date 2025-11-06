using UnityEngine;

public class PlayerAnimation : MonoBehaviour
{
    [Header("Sprites de Anima��o")]
    public Sprite[] walkSprites;
    public Sprite[] jumpSprites;
    public Sprite idleSprite;

    [Header("Velocidade da anima��o")]
    public float frameRate = 0.1f; // quanto menor, mais r�pida a anima��o

    private SpriteRenderer sr;
    private Rigidbody2D rb;
    private float timer;
    private int frameIndex;

    void Start()
    {
        sr = GetComponent<SpriteRenderer>();
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        bool isJumping = rb.linearVelocity.y > 0.1f || rb.linearVelocity.y < -0.1f;
        bool isWalking = Mathf.Abs(rb.linearVelocity.x) > 0.1f && !isJumping;

        timer += Time.deltaTime;

        if (isWalking)
        {
            if (timer >= frameRate)
            {
                timer = 0f;
                frameIndex = (frameIndex + 1) % walkSprites.Length;
                sr.sprite = walkSprites[frameIndex];
            }
        }
        else if (isJumping)
        {
            if (jumpSprites.Length > 0)
                sr.sprite = jumpSprites[0]; // s� 1 sprite de pulo, ou o primeiro da lista
        }
        else
        {
            sr.sprite = idleSprite;
        }

        // vira o sprite pro lado que est� andando
        if (rb.linearVelocity.x > 0.1f)
            sr.flipX = false;
        else if (rb.linearVelocity.x < -0.1f)
            sr.flipX = true;
    }
}
