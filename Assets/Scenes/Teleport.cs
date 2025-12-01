using UnityEngine;

public class Teleport : MonoBehaviour
{
    public Transform destino;

    private void OnTriggerEnter(Collider other) // 3D
    {
        if (other.CompareTag("Player"))
        {
            other.transform.position = destino.position;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision) // 2D
    {
        if (collision.CompareTag("Player"))
        {
            collision.transform.position = destino.position;
        }
    }
}
