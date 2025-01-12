using UnityEngine;
using UnityEngine.VFX;

public class VFXActivator : MonoBehaviour
{
    // R�f�rence au composant Visual Effect
    private VisualEffect visualEffect;

    // Touche � utiliser pour activer/d�sactiver l'effet
    [SerializeField]
    private KeyCode activationKey = KeyCode.B;

    void Start()
    {
        // R�cup�ration du composant Visual Effect
        visualEffect = GetComponent<VisualEffect>();

        if (visualEffect == null)
        {
            Debug.LogError("Aucun composant Visual Effect trouv� sur cet objet !");
        }
    }

    void Update()
    {
        // V�rifie si la touche est press�e
        if (Input.GetKeyDown(activationKey))
        {
            if (visualEffect != null)
            {
                // Active ou d�sactive l'effet
                visualEffect.enabled = !visualEffect.enabled;
            }
        }
    }
}