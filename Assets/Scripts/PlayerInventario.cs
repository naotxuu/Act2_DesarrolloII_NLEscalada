using System.Collections.Generic;
using UnityEngine;

public class PlayerInventario : MonoBehaviour
{
    [SerializeField] private Player player;

    [SerializeField] private GameObject linternaPlayer;
    //[SerializeField] private bool tieneLinterna; //Podríamos hacer que se ponga y quite la linterna a antojo, pero ya veremos


    private HashSet<string> llaves = new HashSet<string>();

    private void Awake()
    {
        player = GetComponent<Player>();
    }

    public void EquiparLinterna()
    { 
        //tieneLinterna = true;
        linternaPlayer.SetActive(true);
        player.ActivarLinterna();
    }

    public void AgregarLlave(string idLlave)
    { 
        llaves.Add(idLlave);
    }

    public bool TieneLlave(string idLlave)
    {
        return llaves.Contains(idLlave);
    }
}
