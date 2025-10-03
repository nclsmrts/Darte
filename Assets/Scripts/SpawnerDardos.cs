using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine.XR.Interaction.Toolkit.Interactables;

public class SpawnerDardos : MonoBehaviour
{
    [Header("Configura��o do Spawner")]
    public GameObject dartPrefab;   // Prefab do dardo
    public Transform spawnPoint;    // Ponto fixo onde nasce

    private GameObject currentDart; // Dardo que est� no spawn

    void Start()
    {
        SpawnDart(); // cria o primeiro dardo
    }

    void SpawnDart()
    {
        // Se j� tem um dardo no spawn, n�o cria outro
        if (currentDart != null) return;

        // Cria dardo no ponto
        currentDart = Instantiate(dartPrefab, spawnPoint.position, spawnPoint.rotation);

        // Liga evento de "quando for pego"
        XRGrabInteractable grab = currentDart.GetComponent<XRGrabInteractable>();
        if (grab != null)
        {
            grab.selectEntered.AddListener(OnGrabDart);
        }
    }

    void OnGrabDart(SelectEnterEventArgs args)
    {
        // Desliga o evento para evitar duplica��o
        XRGrabInteractable grab = currentDart.GetComponent<XRGrabInteractable>();
        if (grab != null)
        {
            grab.selectEntered.RemoveListener(OnGrabDart);
        }

        // Libera a refer�ncia (pois o jogador j� pegou)
        currentDart = null;

        // Cria outro dardo no spawn
        SpawnDart();
    }
}
