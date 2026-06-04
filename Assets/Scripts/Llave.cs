using UnityEngine;

public class Llave : MonoBehaviour, Interactuable
{
    [SerializeField] private string idLlave;

    public void Interactuas(Player player)
    {
        player.GetComponent<PlayerInventario>().AgregarLlave(idLlave);
        Destroy(gameObject);
    }
}
