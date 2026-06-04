using UnityEngine;

public class Puerta : MonoBehaviour, Interactuable
{
    [SerializeField] private string idLlaveNecesaria;
    [SerializeField] private Animator animator;

    //private bool abierta;

    public void Interactuas(Player player)
    {
        Debug.Log("Player interactua");
        PlayerInventario inventario = player.GetComponent<PlayerInventario>();
        if(inventario.TieneLlave(idLlaveNecesaria))
        {
            //abierta = true;
            animator.SetTrigger("abrir");
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Enemigo"))
        {
            animator.SetTrigger("abrir");
        }
    }
}
