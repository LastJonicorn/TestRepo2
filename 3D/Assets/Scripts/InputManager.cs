using UnityEngine;
using UnityEngine.InputSystem; // Käytetään Unityn uutta Input Systemiä

public class InputManager : MonoBehaviour
{
    public InputSystem_Actions inputActions; // Viite pelaajan syöttötoimintoihin
    private PlayerMotor motor; // Viite PlayerMotor-komponenttiin, joka vastaa hahmon liikkeestä
    private PlayerLook look;

    private void Awake()
    {
        inputActions = new InputSystem_Actions(); // Luodaan uusi syöttöjärjestelmän instanssi
        motor = GetComponent<PlayerMotor>(); // Haetaan PlayerMotor-komponentti
        look = GetComponent<PlayerLook>();
        // Kuunnellaan hyppykomennon suorittamista ja kutsutaan motorin Jump()-metodia
        inputActions.Player.Jump.performed += ctx => motor.Jump();
    }

    private void OnEnable()
    {
        inputActions.Enable(); // Otetaan syöttöjärjestelmä käyttöön

        // Lisätään Move- ja Attack-komentoihin vastaavat käsittelijät
        inputActions.Player.Move.performed += OnMove;
        inputActions.Player.Attack.performed += OnAttack;
    }

    private void OnDisable()
    {
        // Poistetaan Move- ja Attack-käsittelijät, kun komponentti poistetaan käytöstä
        inputActions.Player.Move.performed -= OnMove;
        inputActions.Player.Attack.performed -= OnAttack;
        inputActions.Disable(); // Poistetaan syöttöjärjestelmä käytöstä
    }

    private void FixedUpdate()
    {
        // Luetaan pelaajan liikesyöte ja siirretään se motorin Move-metodille
        motor.Move(inputActions.Player.Move.ReadValue<Vector2>());
    }

    private void LateUpdate()
    {
        // Luetaan pelaajan liikesyöte ja siirretään se motorin Move-metodille
        look.Look(inputActions.Player.Look.ReadValue<Vector2>());
    }

    private void OnMove(InputAction.CallbackContext context)
    {
        // Luetaan liikesyöte ja tulostetaan se debug-konsoliin
        Vector2 movementInput = context.ReadValue<Vector2>();
        Debug.Log("Move: " + movementInput);
    }

    private void OnAttack(InputAction.CallbackContext context)
    {
        // Tulostetaan hyökkäystoiminto debug-konsoliin
        Debug.Log("Attack!");
    }
}
