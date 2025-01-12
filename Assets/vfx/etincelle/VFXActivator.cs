using UnityEngine;
using UnityEngine.VFX;

public class VFXActivator : MonoBehaviour
{
    // Référence au composant Visual Effect
    private VisualEffect visualEffect;

    // Touche à utiliser pour activer/désactiver l'effet
    [SerializeField]
    private KeyCode activationKey = KeyCode.B;

    void Start()
    {
        // Récupération du composant Visual Effect
        visualEffect = GetComponent<VisualEffect>();

        if (visualEffect == null)
        {
            Debug.LogError("Aucun composant Visual Effect trouvé sur cet objet !");
        }
    }

    void Update()
    {
        // Vérifie si la touche est pressée
        if (Input.GetKeyDown(activationKey))
        {
            if (visualEffect != null)
            {
                // Active ou désactive l'effet
                visualEffect.enabled = !visualEffect.enabled;
            }
        }
    }
}