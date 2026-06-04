using System.Collections;
using UnityEngine;

public class SonidoPasos : MonoBehaviour
{
    [Header("Referencias")]
    [SerializeField] private Player player;
    [SerializeField] private AudioSource audioSource;

    [Header("Sonidos de pasos")]
    [SerializeField] private AudioClip[] pasos;

    [Header("Tiempos")]
    [SerializeField] private float tiempoEntrePasosCaminando = 0.5f;
    [SerializeField] private float tiempoEntrePasosCorriendo = 0.25f;

    [Header("Volumen")]
    [SerializeField] private float volumen = 0.7f;

    private Coroutine rutinaPasos;

    private void Awake()
    {
        player = GetComponent<Player>();
        audioSource = GetComponent<AudioSource>();
    }

    private void Update()
    {
        if (player.moviendome)
        {
            if (rutinaPasos == null)
            {
                rutinaPasos = StartCoroutine(RutinaPasos());
            }
        }
        else
        {
            if (rutinaPasos != null)
            {
                StopCoroutine(rutinaPasos);
                rutinaPasos = null;
            }
        }
    }

    private IEnumerator RutinaPasos()
    {
        while (player.moviendome)
        {
            ReproducirPaso();

            if (player.corriendo)
            {
                yield return new WaitForSeconds(tiempoEntrePasosCorriendo);
            }
            else
            {
                yield return new WaitForSeconds(tiempoEntrePasosCaminando);
            }
        }

        rutinaPasos = null;
    }

    private void ReproducirPaso()
    {
        if (pasos.Length == 0)
            return;

        int indice = Random.Range(0, pasos.Length);
        AudioClip paso = pasos[indice];

        audioSource.PlayOneShot(paso, volumen);
    }
}