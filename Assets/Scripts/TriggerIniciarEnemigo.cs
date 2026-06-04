using UnityEngine;

public class TriggerIniciarEnemigo : MonoBehaviour
{
    [SerializeField] private Enemigo enemigo;

    private bool usado = false;

    private void OnTriggerEnter(Collider other)
    {
        if(usado) { return; }

        if(other.CompareTag("Player"))
        {
            usado = true;
            enemigo.ActivarSecuenciaInicial();
        }
    }
}
