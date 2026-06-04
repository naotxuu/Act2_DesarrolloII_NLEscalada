using System.Collections;
using UnityEngine;
using UnityEngine.AI;

public class Enemigo : MonoBehaviour
{
    private NavMeshAgent agent;

    private Player target;

    private Animator anim;


    private enum EstadoEnemigo //Máquina de estados
    {
        Sentado,
        CaminandoAPunto,
        Persiguiendo
    }

    private EstadoEnemigo estadoActual = EstadoEnemigo.Sentado;


    [Header("Secuencia inicial")]
    [SerializeField] private Camera camaraEnemigo;
    [SerializeField] private Transform puntoInicialDestino;
    [SerializeField] private float distanciaLlegada;



    [Header("Movimiento")]
    [SerializeField] private float velocidadCaminar = 1.0f;
    [SerializeField] private float velocidadCorrer = 2.0f;


    //No es necesario
    [Header("Sistema de combate")]

    //private float vidas = 100;

    [SerializeField] private Transform puntoAtaque;

    [SerializeField] private float radioAtaque;

    [SerializeField] private float danhoAtaque;


    void Start()
    {
        agent = GetComponent<NavMeshAgent>();

        anim = GetComponent<Animator>();

        target = FindObjectOfType<Player>();

        agent.isStopped = true;
        agent.speed = velocidadCaminar;

        if (camaraEnemigo != null)
        { camaraEnemigo.gameObject.SetActive(false); }

        anim.SetBool("sitted", true);
        anim.SetBool("walking", false);
        anim.SetBool("fast", false);
    }


    void Update()
    {
        if (estadoActual == EstadoEnemigo.Persiguiendo && target != null)
        { 
            agent.SetDestination(target.transform.position);
            EnfocarObjetivo();
        }

        //Distancia de ataque 

        //if (agent.remainingDistance <= agent.stoppingDistance)

        //{
        //    EnfocarObjetivo();

        //    //LanzarAtaque(); //Vamos a cambiarlo
        //}
    }

    private void EnfocarObjetivo()
    {
        Vector3 direccionAObjetivo = (target.transform.position - transform.position).normalized; //Para asegurarnos de que el enemigo siempre nos mira 

        direccionAObjetivo.y = 0;

        Quaternion rotacionAObjetivo = Quaternion.LookRotation(direccionAObjetivo);

        transform.rotation = rotacionAObjetivo;
    }


    public void ActivarSecuenciaInicial()
    {
        StartCoroutine(SecuenciaInicial());
    }


    private IEnumerator SecuenciaInicial()
    {
        estadoActual = EstadoEnemigo.CaminandoAPunto;

        if(camaraEnemigo != null)
        { camaraEnemigo.gameObject.SetActive(true); }

        anim.SetBool("sitted", false);

        yield return new WaitForSeconds(2);

        anim.SetBool("walking", true);

        agent.speed = velocidadCaminar;
        agent.isStopped = false;
        agent.SetDestination(puntoInicialDestino.position);

        while (agent.pathPending || agent.remainingDistance > distanciaLlegada)
        {
            yield return null;
        }

        ActivarPersecucion();
    }


    public void ActivarPersecucion()
    {
        estadoActual = EstadoEnemigo.Persiguiendo;

        if (camaraEnemigo != null)
        { camaraEnemigo.gameObject.SetActive(false); }

        anim.SetBool("walking", false);
        anim.SetBool("fast", true);

        agent.speed = velocidadCorrer;
    }


    private void OnTriggerEnter(Collider other)
    {
        Player player =  other.GetComponent<Player>();

        if (player != null)
        {
            agent.isStopped = true;
            player.Morir();
        }
    }



    private void OnDrawGizmos()

    { Gizmos.DrawSphere(puntoAtaque.position, radioAtaque); }





    //No ataca, solo nos mata, no usa

    //private void LanzarAtaque()

    //{
    //    agent.isStopped = true;

    //    anim.SetTrigger("attack");
    //}

    private void Atacar() //Evento en la animacion
    {
        Collider[] colliderTocados = Physics.OverlapSphere(puntoAtaque.position, radioAtaque);

        foreach (Collider coll in colliderTocados)

        {
            if (coll.TryGetComponent(out Danhable danhable))

            { danhable.RecibirDanho(danhoAtaque); }
        }
    }

    private void FinDeAtaque() //En la animacion
    {
        agent.isStopped = false;

        //anim.SetBool("attacking", false); //no es un booleano
    }

    //public void RecibirDanho(float danho)

    //{
    //    vidas -= danho;

    //    if (vidas <= 0)

    //    { Destroy(this.gameObject); }
    //}

}
