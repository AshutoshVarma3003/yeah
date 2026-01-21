using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float speed = 6f;
    public float gravity = -9.81f;
    public float jumpHeight = 1.5f;
    public bool isLocked = false;

    Vector3 velocity;

    CharacterController controller;

    void Start()
    {
        controller = GetComponent<CharacterController>();
        isLocked = true;
        #if UNITY_EDITOR
                isLocked = false;
        #endif
    }

    void Update()
    {
        if (isLocked)
            return;
        float moveSpeed = Input.GetKey(KeyCode.LeftShift) ? speed * 2f : speed;
        if (velocity.y < 0)
            velocity.y = -2f;

        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");

        Vector3 move = transform.right * x + transform.forward * z;
        move = Vector3.ClampMagnitude(move, 1f);

        controller.Move(move * moveSpeed * Time.deltaTime);

        velocity.y += gravity * Time.deltaTime;
        controller.Move(velocity * Time.deltaTime);
    }

    public void Unlock()
    {
        isLocked = false;
    }
}
