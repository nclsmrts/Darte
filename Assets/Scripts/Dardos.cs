using UnityEngine;

public class Dardos : MonoBehaviour
{

    private Rigidbody rb;
    private bool hasCollided = false;

    private void Start()
    {
        rb.GetComponent<Rigidbody>();
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (hasCollided) return; // evita m�ltiplas colis�es

        if (collision.gameObject.CompareTag("Alvo"))
        {
            hasCollided = true;

            // Para a f�sica do dardo
            rb.isKinematic = true;
            rb.linearVelocity = Vector3.zero;
            rb.angularVelocity = Vector3.zero;

            // Faz o dardo seguir o alvo
            //transform.SetParent(collision.transform);

            Debug.Log("bateu");
        }
    }
}
