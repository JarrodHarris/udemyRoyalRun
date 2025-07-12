using UnityEngine;
using UnityEngine.AI;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    [SerializeField] float moveSpeed = 5f;
    [SerializeField] float xClampRange = 4f;
    [SerializeField] float zClampRange = 3f;
    Vector2 movement;
    Rigidbody rb;

    private void Awake()
    {
        rb = this.GetComponent<Rigidbody>();
    }

    private void FixedUpdate()
    {
        HandleMovement();
    }
    
    public void Move(InputAction.CallbackContext context)
    {
        movement = context.ReadValue<Vector2>();
        // Debug.Log(movement);
    }

    private void HandleMovement()
    {
        Vector3 currentPosition = rb.position;  //When using FixedUpdate for movement, instead of using transform position, using rigidbody's position is more appropriate
        Vector3 moveDirection = new Vector3(movement.x, 0f, movement.y);    //movement.y is used as it is referring to a Vector2(x, y)



        Vector3 newPosition = currentPosition + moveDirection * (moveSpeed * Time.fixedDeltaTime);

        newPosition.x = Mathf.Clamp(newPosition.x, -xClampRange, xClampRange);
        newPosition.z = Mathf.Clamp(newPosition.z, -zClampRange, zClampRange);


        rb.MovePosition(newPosition);


    }
}
