using UnityEngine;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour, Danhable
{
    [Header("Velocidad")]

    [SerializeField] private float velocidadMovimiento;

    [SerializeField] private float velocidadCorriendo;

    private float velocidad;


    [SerializeField] private float factorGravedad; //Tiene que ser negativo 

    //[SerializeField] private float alturaDeSalto;


    [SerializeField] private Transform camara;

    [SerializeField] private InputManagerSO inputManager;
    
    [SerializeField] private Animator anim;


    //[SerializeField] private ParticleSystem particles;



    [Header("Detección Interactuable")]

    [SerializeField] private Transform manos;

    [SerializeField] private float radioDeteccion;

    [SerializeField] private LayerMask queEsInteractuable;



    //[Header("Detección Suelo")]

    //[SerializeField] private Transform pies;

    //[SerializeField] private LayerMask queEsSuelo;



    //[Header("Sistema de combate")]

    [SerializeField] private float vida; //No uso

    //[SerializeField] private float distanciaDisparo;

    //[SerializeField] private float danhoDisparo;



    private CharacterController controller;

    private Vector3 direccionMovimiento;

    private Vector3 direccionInput;

    private Vector3 velocidadVertical;


    public bool moviendome;

    public bool corriendo;




    private void OnEnable()
    {
        //inputManager.OnSaltar += Saltar;

        //inputManager.OnDisparar += Disparar;

        //inputManager.OnRecargar += Recargar;

        inputManager.OnMover += Mover;

        inputManager.OnRun += Run;

        inputManager.OnInteractuar += Interactuar;
    }


    private void OnDisable()
    {
        //inputManager.OnSaltar -= Saltar;

        inputManager.OnMover -= Mover;

        inputManager.OnRun -= Run;

        inputManager.OnInteractuar -= Interactuar;
    }


    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked; //Para que no se vea el ratón  

        controller = GetComponent<CharacterController>();
    }



    void Update()
    {
        RotarHaciaCamara();

        AplicarMovimiento();

        ActualizarMovimiento();


        //ManejarVelocidadVertical();
        AplicarGravedad();
    }


    private void Interactuar()
    {
        anim.SetTrigger("interact");

        if (Physics.SphereCast(camara.transform.position, radioDeteccion, camara.forward, out RaycastHit hitInfo, 2.0f, queEsInteractuable, QueryTriggerInteraction.Collide)) //Mira a ver si impactas en algo
        {
            Debug.Log("Interactuo, me choco con " + hitInfo.collider.name);
            Interactuable interactuable = hitInfo.collider.GetComponent<Interactuable>();

            if (interactuable != null) //Mira a ver si es dañable 
            {
                Debug.Log("Objeto interactuable encontrado: " + hitInfo.collider.name);
                interactuable.Interactuas(this);
            }
        }
    }


    private void Run(bool pulsando)
    {
        corriendo = pulsando;

        if (pulsando)//estaCorriendo)
        {
            anim.SetBool("running", true);

            velocidad = velocidadMovimiento;

            velocidadMovimiento = velocidadCorriendo;
        }

        else
        {
            anim.SetBool("running", false);

            velocidadMovimiento = velocidad;
        }
    }


    private void Mover(Vector2 ctx)
    { 
        direccionInput = new Vector3(ctx.x, 0, ctx.y);
    }


    private void AplicarGravedad()
    {
        velocidadVertical.y += factorGravedad * Time.deltaTime;

        controller.Move(velocidadVertical * Time.deltaTime);
    }


    private void RotarHaciaCamara()
    {
        Vector3 direccionCamara = camara.forward;

        direccionCamara.y = 0f;

        if (direccionCamara.sqrMagnitude > 0.1f)
        {
            Quaternion rotacionObjetivo = Quaternion.LookRotation(direccionCamara);
            transform.rotation = rotacionObjetivo;
        }
    }

    private void ActualizarMovimiento()
    {
        if (direccionMovimiento.sqrMagnitude > 0)
        {
            anim.SetBool("walking", true);

            //RotarHaciaDestino();
        }

        else
        {
            anim.SetBool("walking", false);
        }
    }


    private void AplicarMovimiento()
    {
        direccionMovimiento = camara.forward * direccionInput.z + camara.right * direccionInput.x;

        direccionMovimiento.y = 0; //para no subir xd 

        controller.Move(direccionMovimiento * velocidadMovimiento * Time.deltaTime);

        moviendome = direccionMovimiento.magnitude > 0.1f;
    }


    public void ActivarLinterna()
    {
        anim.SetBool("hasflash", true);
    }


    public void Morir()
    {
        SceneManager.LoadScene("GameOver");
    }

    public void Ganar()
    {
        SceneManager.LoadScene("GameWin");
    }


    private void OnDrawGizmos()

    { Gizmos.DrawSphere(camara.position, radioDeteccion); }




    //Diferente, no se usa:


    //private void Saltar()

    //{
    //    if (EstoyEnSuelo())

    //    { velocidadVertical.y = Mathf.Sqrt(-2 * factorGravedad * alturaDeSalto); }
    //}


    //private void Recargar()

    //{ anim.SetTrigger("reload"); }



    //private void Disparar()

    //{
    //    anim.SetTrigger("shoot");

    //    audio.Play();

    //    particles.Play();



    //    if (Physics.Raycast(camara.position, camara.forward, out RaycastHit hitInfo, distanciaDisparo)) //Mira a ver si impactas en algo 

    //    {
    //        if (hitInfo.transform.TryGetComponent(out Danhable sistemaDanho)) //Mira a ver si es dañable 

    //        {
    //            if (!hitInfo.transform.CompareTag("Player"))

    //            { sistemaDanho.RecibirDanho(danhoDisparo); }
    //        }
    //    }
    //}

    public void RecibirDanho(float danho)

    {
        vida -= danho;

        if (vida <= 0)

        { Debug.Log("Aqui mueres"); }
    }

    //private bool EstoyEnSuelo()

    //{ return Physics.CheckSphere(pies.position, radioDeteccion, queEsSuelo); }

    private void RotarHaciaDestino()
    {
        Quaternion rotacionObjetivo = Quaternion.LookRotation(direccionMovimiento);

        transform.rotation = rotacionObjetivo;
    }

    //private void ManejarVelocidadVertical()

    //{
    //    if (EstoyEnSuelo() && velocidadVertical.y < 0)

    //    { velocidadVertical.y = 0; }

    //    AplicarGravedad();
    //}
}
