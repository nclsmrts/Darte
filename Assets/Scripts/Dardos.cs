using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine.XR.Interaction.Toolkit.Interactables;

[RequireComponent(typeof(Rigidbody), typeof(XRGrabInteractable))]
public class Dardos : MonoBehaviour
{
    private Rigidbody rb;
    private bool hasCollided = false;
    private XRGrabInteractable grab;

    public float throwForce = 15f;   // força ao soltar
    public float stickDepth = 0.05f; // quão fundo o dardo entra no alvo

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        grab = GetComponent<XRGrabInteractable>();

        //comentar isso para funicionar no VR
        // conectar eventos do XR
        grab.selectEntered.AddListener(OnGrab);
        grab.selectExited.AddListener(OnRelease);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (hasCollided) return; // evita múltiplas colisões

        if (collision.gameObject.CompareTag("Alvo"))
        {
            hasCollided = true;

            // Para a física do dardo
            rb.isKinematic = true;
            rb.linearVelocity = Vector3.zero;
            rb.angularVelocity = Vector3.zero;

            // Alinha o dardo na superfície
            ContactPoint contact = collision.contacts[0];
            transform.position = contact.point - transform.forward * stickDepth;
            transform.rotation = Quaternion.LookRotation(-contact.normal, Vector3.up);

            // Faz o dardo "colar" no alvo
            transform.SetParent(collision.transform);

            Debug.Log("Dardo grudou no alvo!");
        }
    }

    void OnGrab(SelectEnterEventArgs args)
    {
        hasCollided = false; // reseta quando pegar
        rb.isKinematic = true; // trava na mão
        transform.SetParent(args.interactorObject.transform); // cola na mão
    }

    void OnRelease(SelectExitEventArgs args)
    {
        transform.SetParent(null); // solta da mão
        rb.isKinematic = false;

        rb.linearVelocity = Vector3.zero;
        rb.angularVelocity = Vector3.zero;

        // aplica força pra frente ao soltar
        rb.AddForce(transform.forward * throwForce, ForceMode.Impulse);
    }
}
