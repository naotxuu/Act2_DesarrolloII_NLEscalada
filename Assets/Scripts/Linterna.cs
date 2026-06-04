using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Experimental.GlobalIllumination;

public class Linterna : MonoBehaviour, Interactuable
{
    [SerializeField] private GameObject spotLight;
    [SerializeField] private float tiempoOn;
    [SerializeField] private float tiempoOff;

    private void Start()
    {
        StartCoroutine(ParpadeoLinterna());
    }

    private IEnumerator ParpadeoLinterna()
    {
        while (true)
        {
            spotLight.SetActive(false);
            yield return new WaitForSeconds(tiempoOff);

            spotLight.SetActive(true);
            yield return new WaitForSeconds(tiempoOn);
        }
    }

    public void Interactuas(Player player)
    {
        PlayerInventario inventario = player.GetComponent<PlayerInventario>();
        inventario.EquiparLinterna();
        Destroy(gameObject);
    }
    
}
