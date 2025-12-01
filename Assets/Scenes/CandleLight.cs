using UnityEngine;

public class CandleLight : MonoBehaviour
{
    public Light candleLight; // a luz da vela
    public bool isLit = false; // começa apagada

    void Start()
    {
        candleLight.enabled = isLit;
    }

    void Update()
    {
        // Tecla E liga ou desliga a vela
        if (Input.GetKeyDown(KeyCode.E))
        {
            isLit = !isLit;
            candleLight.enabled = isLit;
        }
    }
}
