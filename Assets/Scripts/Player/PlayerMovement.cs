using UnityEngine;

[RequireComponent(typeof(BoxCollider))]
public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float _moveSpeed = 7f;
    [SerializeField] private float _rotateSpeed = 10f;
    [SerializeField] private Transform _camera;
    [SerializeField] private LayerMask _collisionLayerMask;
    
    private const float _minMovementInput = 0.5f;
    
    private bool _isWalking;
    private float _playerRadius;

    private void Awake()
    {
        _playerRadius = GetComponent<BoxCollider>().size.x;
    }

    private void Update()
    {
        HandleMovement();
    }

    public bool IsWalking()
    {
        return _isWalking;
    }
    
    private void HandleMovement()
    {
        Vector2 inputVector = GameInput.Instance.GetMovementVectorNormalized();
        
        _isWalking = inputVector != Vector2.zero;
        
        if (inputVector.magnitude < _minMovementInput)
            return;
        
        float targetAngle = Mathf.Atan2(inputVector.x, inputVector.y) * Mathf.Rad2Deg + _camera.eulerAngles.y;
        Quaternion targetRotation = Quaternion.Euler(0f, targetAngle, 0f);
        Quaternion rotation = Quaternion.Lerp(transform.rotation, targetRotation, _rotateSpeed * Time.deltaTime);
        transform.rotation = rotation;

        Vector3 moveDir = Quaternion.Euler(0f, targetAngle, 0f) * Vector3.forward;
        moveDir = moveDir.normalized;

        float moveDistance = _moveSpeed * Time.deltaTime;
        bool canMove = !Physics.BoxCast(transform.position, Vector3.one * _playerRadius, moveDir, Quaternion.identity, moveDistance, _collisionLayerMask);

        if (!canMove)
        {
            Vector3 moveDirX = new Vector3(moveDir.x, 0, 0);
            canMove = (moveDir.x < -_minMovementInput || moveDir.x > _minMovementInput) && !Physics.BoxCast(transform.position, Vector3.one * _playerRadius, moveDirX, Quaternion.identity, moveDistance, _collisionLayerMask);

            if (canMove)
            {
                moveDir = moveDirX;
            }
            else
            {
                Vector3 moveDirZ = new Vector3(0, 0, moveDir.z);
                canMove = (moveDir.z < -_minMovementInput || moveDir.z > +_minMovementInput) && !Physics.BoxCast(transform.position, Vector3.one * _playerRadius, moveDirZ, Quaternion.identity, moveDistance, _collisionLayerMask);

                if (canMove)
                {
                    moveDir = moveDirZ;
                }
            }
        }

        if (canMove)
        {
            transform.position += moveDir * moveDistance;
        }
    }
}