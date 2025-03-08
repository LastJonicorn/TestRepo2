using UnityEngine;
using UnityEngine.InputSystem; // K�ytet��n Unityn uutta Input Systemi�

public class InputManager : MonoBehaviour
{
    public InputSystem_Actions inputActions; // Viite pelaajan sy�tt�toimintoihin
    private PlayerMotor motor; // Viite PlayerMotor-komponenttiin, joka vastaa hahmon liikkeest�
    private PlayerLook look;

    private void Awake()
    {
        inputActions = new InputSystem_Actions(); // Luodaan uusi sy�tt�j�rjestelm�n instanssi
        motor = GetComponent<PlayerMotor>(); // Haetaan PlayerMotor-komponentti
        look = GetComponent<PlayerLook>();
        // Kuunnellaan hyppykomennon suorittamista ja kutsutaan motorin Jump()-metodia
        inputActions.Player.Jump.performed += ctx => motor.Jump();
    }

    private void OnEnable()
    {
        inputActions.Enable(); // Otetaan sy�tt�j�rjestelm� k�ytt��n

        // Lis�t��n Move- ja Attack-komentoihin vastaavat k�sittelij�t
        inputActions.Player.Move.performed += OnMove;
        inputActions.Player.Attack.performed += OnAttack;
    }

    private void OnDisable()
    {
        // Poistetaan Move- ja Attack-k�sittelij�t, kun komponentti poistetaan k�yt�st�
        inputActions.Player.Move.performed -= OnMove;
        inputActions.Player.Attack.performed -= OnAttack;
        inputActions.Disable(); // Poistetaan sy�tt�j�rjestelm� k�yt�st�
    }

    private void FixedUpdate()
    {
        // Luetaan pelaajan liikesy�te ja siirret��n se motorin Move-metodille
        motor.Move(inputActions.Player.Move.ReadValue<Vector2>());
    }

    private void LateUpdate()
    {
        // Luetaan pelaajan liikesy�te ja siirret��n se motorin Move-metodille
        look.Look(inputActions.Player.Look.ReadValue<Vector2>());
    }

    private void OnMove(InputAction.CallbackContext context)
    {
        // Luetaan liikesy�te ja tulostetaan se debug-konsoliin
        Vector2 movementInput = context.ReadValue<Vector2>();
        Debug.Log("Move: " + movementInput);
    }

    private void OnAttack(InputAction.CallbackContext context)
    {
        // Tulostetaan hy�kk�ystoiminto debug-konsoliin
        Debug.Log("Attack!");
    }
}
