using UnityEngine;

[RequireComponent(typeof(CharacterController))]
public class PlayerController : MonoBehaviour
{
    [SerializeField]
    private float playerSpeed = 2.0f;
    [SerializeField]
    private float jumpHeight = 1.0f;
    [SerializeField]
    private float gravityValue = -9.81f;
    
    private CharacterController controller;
    private Vector3 playerVelocity;
    private bool groundedPlayer;
    private InputManager inputManager;
    private Transform cameraTransform;
    

    private void Start() {
        controller = GetComponent<CharacterController>();
        inputManager = InputManager.Instance;
        if (Camera.main != null) cameraTransform = Camera.main.transform;
    }

    void Update() {
        groundedPlayer = controller.isGrounded;
        
        if (groundedPlayer && playerVelocity.y < 0) {
            playerVelocity.y = 0f;
        }

        Vector2 movement = inputManager.GetPlayerMovement();
        Vector3 move = new Vector3(movement.x, 0, movement.y);
        move = cameraTransform.forward.normalized * move.z + cameraTransform.right.normalized * move.x;
        move.y = 0f;
        controller.Move(move * Time.deltaTime * playerSpeed);

        // Changes the characters forward position when not moving
        //if (move != Vector3.zero) {
        //    gameObject.transform.forward = move;
        //}

        // Changes the height position of the player..
        if (inputManager.PlayerJumpedThisFrame() && groundedPlayer) {
            playerVelocity.y += Mathf.Sqrt(jumpHeight * -3.0f * gravityValue);
        }

        playerVelocity.y += gravityValue * Time.deltaTime;
        controller.Move(playerVelocity * Time.deltaTime);
    }

}
