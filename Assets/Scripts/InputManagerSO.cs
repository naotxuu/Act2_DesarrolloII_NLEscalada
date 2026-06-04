using System;
using UnityEngine;
using UnityEngine.InputSystem;


[CreateAssetMenu(menuName = "InputManager")]

public class InputManagerSO : ScriptableObject
{
    Controls misControles;

    //public event Action OnSaltar; //Creamos nuestros propios eventos 

    //public event Action OnDisparar;

    //public event Action OnRecargar;

    public event Action<Vector2> OnMover;
    
    public event Action OnInteractuar;
    
    public event Action<bool> OnRun;



    private void OnEnable()

    {
        misControles = new Controls();

        misControles.Gameplay.Enable();

        //misControles.Gameplay.Disparar.started += Disparar;

        //misControles.Gameplay.Recargar.started += Recargar;

        misControles.Gameplay.Interactuar.started += Interactuar;


        misControles.Gameplay.Run.performed += Run;

        misControles.Gameplay.Run.canceled += Run;


        misControles.Gameplay.Mover.performed += Mover;

        misControles.Gameplay.Mover.canceled += Mover;

        //misControles.Gameplay.Saltar.started += Saltar;
    }

    private void OnDisable()
    {
        if(misControles == null) return;


        misControles.Gameplay.Interactuar.started -= Interactuar;


        misControles.Gameplay.Run.performed -= Run;

        misControles.Gameplay.Run.canceled -= Run;


        misControles.Gameplay.Mover.performed -= Mover;

        misControles.Gameplay.Mover.canceled -= Mover;

        //misControles.Gameplay.Saltar.started -= Saltar;

        misControles.Gameplay.Disable();

        misControles.Dispose();
        misControles = null;
    }

    //private void Saltar(InputAction.CallbackContext ctx)

    //{ OnSaltar?.Invoke(); }


    //private void Disparar(InputAction.CallbackContext ctx)

    //{ OnDisparar?.Invoke(); }


    //private void Recargar(InputAction.CallbackContext ctx)

    //{ OnRecargar?.Invoke(); } 


    private void Mover(InputAction.CallbackContext ctx)

    { 
        OnMover?.Invoke(ctx.ReadValue<Vector2>());
    }


    private void Run(InputAction.CallbackContext ctx)
    {
        OnRun?.Invoke(ctx.performed);
    }

    private void Interactuar(InputAction.CallbackContext ctx)
    {
        OnInteractuar?.Invoke();
    }


}
