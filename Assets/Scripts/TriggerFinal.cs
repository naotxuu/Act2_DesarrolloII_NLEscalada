using UnityEngine;

public class TriggerFinal : MonoBehaviour
{
    [SerializeField] private Player player;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            player.Ganar();
        }
    }
}
