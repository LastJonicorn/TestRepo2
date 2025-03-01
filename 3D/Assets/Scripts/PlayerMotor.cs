using UnityEngine;

public class PlayerMotor : MonoBehaviour
{
    private CharacterController controller; // Viite CharacterController-komponenttiin
    private Vector3 playerVelocity; // Pelaajan nykyinen nopeus
    public float speed = 5f; // Pelaajan liikkumisnopeus
    private bool isGrounded; // Onko pelaaja maassa
    public float gravity = -9.8f; // Painovoiman voimakkuus
    public float jumpHeight = 3f; // Hyppykorkeus

    // Start-metodi kutsutaan kerran ennen ensimm‰ist‰ Update-kutsua
    void Start()
    {
        controller = GetComponent<CharacterController>(); // Haetaan CharacterController-komponentti
    }

    // Update-metodi kutsutaan kerran per frame
    void Update()
    {
        isGrounded = controller.isGrounded; // Tarkistetaan, onko pelaaja maassa
    }

    public void Move(Vector2 input)
    {
        Vector3 moveDirection = Vector3.zero; // Alustetaan liikesuunta
        moveDirection.x = input.x; // Asetetaan X-akselin liike
        moveDirection.z = input.y; // Asetetaan Z-akselin liike

        // Liikutetaan pelaajaa suhteessa t‰m‰n nykyiseen suuntaan
        controller.Move(transform.TransformDirection(moveDirection) * speed * Time.deltaTime);

        // Lis‰t‰‰n painovoiman vaikutus pelaajan nopeuteen
        playerVelocity.y += gravity * Time.deltaTime;

        // Jos pelaaja on maassa ja putoaa alasp‰in, asetetaan pieni negatiivinen nopeus, jotta pysyy alustassa
        if (isGrounded && playerVelocity.y < 0)
            playerVelocity.y = -2f;

        // Sovelletaan laskettu nopeus pelaajaan
        controller.Move(playerVelocity * Time.deltaTime);
    }

    public void Jump()
    {
        if (isGrounded) // Tarkistetaan, onko pelaaja maassa ennen hypp‰‰mist‰
        {
            // Lasketaan hyppynopeus fysiikan kaavalla ja asetetaan Y-akselin nopeus
            playerVelocity.y = Mathf.Sqrt(jumpHeight * -3.0f * gravity);
        }
    }
}
