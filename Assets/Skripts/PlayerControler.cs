using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class PlayerControler : MonoBehaviour
{
    [SerializeField] private float moveSpeed;
    [SerializeField] private float rotationSpeed;
    [SerializeField] private new Transform camera;

    private Quaternion _targetRotation;
    private Rigidbody _rigidbody;

    private void Awake()
    {
        _rigidbody = gameObject.GetComponent<Rigidbody>();
    }

    private void FixedUpdate()
    {
        MovePlayer();
    }

    private void MovePlayer()
    {
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");

        float moveAmount = Mathf.Abs(horizontal) + Mathf.Abs(vertical);

        var moveInput = (new Vector3(horizontal, 0, vertical)).normalized;

        var moveDir = camera.rotation * moveInput;

        moveDir.y = 0f;   //исключаю наклон игрока

        if (moveAmount > 0)
        {
            transform.position += moveDir * moveSpeed * Time.deltaTime;
            _targetRotation = Quaternion.LookRotation(moveDir);
        }

        transform.rotation = Quaternion.RotateTowards(transform.rotation, _targetRotation, rotationSpeed * Time.deltaTime);
    }
}

