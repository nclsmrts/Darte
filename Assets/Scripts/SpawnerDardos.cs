using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine.XR.Interaction.Toolkit.Interactables;

public class SpawnerDardos : MonoBehaviour
{
    [Header("Configuração do Spawner")]
    public GameObject dartPrefab;   // Prefab do dardo
    public Transform spawnPoint;    // Ponto fixo onde nasce

    private GameObject currentDart; // Dardo que está no spawn

    void Start()
    {
        SpawnDart(); // cria o primeiro dardo
    }

    void SpawnDart()
    {
        // Se já tem um dardo no spawn, não cria outro
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
        // Desliga o evento para evitar duplicação
        XRGrabInteractable grab = currentDart.GetComponent<XRGrabInteractable>();
        if (grab != null)
        {
            grab.selectEntered.RemoveListener(OnGrabDart);
        }

        // Libera a referência (pois o jogador já pegou)
        currentDart = null;

        // Cria outro dardo no spawn
        SpawnDart();
    }
}
