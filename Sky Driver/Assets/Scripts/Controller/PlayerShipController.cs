using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;

[RequireComponent(typeof(Rigidbody))]
public class PlayerShipController : MonoBehaviour
{
    [SerializeField]
    private LayerMask _platformMask;
    [SerializeField]
    private float _findGroundDistance = 2;
    [SerializeField]
    private float _findObstacleDistance = .2f;
    private Rigidbody _rigidBody;
    [SerializeField]
    private float _speed;
    [SerializeField]
    private float _steerDirection = 0f;
    [SerializeField]
    private float _maxSpeed = 25f;
    [SerializeField]
    private float _accelerationSpeed = 5f;
    [SerializeField]
    private float _steeringSpeed = 10f;
    [SerializeField]
    private float _jumpPower = 5f;

    private Vector3 _startPosition;

    private bool _isAccelerating;
    private bool _isDecelerating;
    private bool _isJumping;

    [field: SerializeField]
    public UnityEvent OnExplode { get; private set; }

    public bool IsMotionLocked { get; set; } = false;

    public bool IsOnGround 
    { 
        get
        {
            RaycastHit hit;
            bool isGroundBelow = Physics.Raycast(transform.position, Vector3.down, out hit, _findGroundDistance, _platformMask);
            
            return isGroundBelow;
        }
    }

    public void OnNoseCollision(Collision other)
    {
        Debug.Log("On Nose Collision.");
        if (_speed > _maxSpeed * 0.5f)
        {
            Explode();
        }
        _speed = 0;
    }

    public void Reset()
    {
        transform.position = _startPosition;
        _rigidBody.velocity = Vector3.zero;
        _speed = 0;
        gameObject.SetActive(true);
    }

    protected void Awake()
    {
        _startPosition = transform.position;
        _rigidBody = GetComponent<Rigidbody>();
        Debug.Assert(_rigidBody != null, "Could not find RigidBody");
    }

    protected void Update()
    {
        Accelerate();
        Decelerate();
        OutOfBoundsCheck();
    }

    protected void FixedUpdate()
    {
        UpdateVelocity();
        Jump();
    }

    public void HandleAccelerateInput(InputAction.CallbackContext context)
    {
        if (context.started)
        {
            _isAccelerating = true;
        }
        else if (context.canceled)
        {
            _isAccelerating = false;
        }
    }

    public void HandleDecelerateInput(InputAction.CallbackContext context)
    {
        if (context.started)
        {
            _isDecelerating = true;
        }
        else if (context.canceled)
        {
            _isDecelerating = false;
        }
    }

    public void HandleSteeringInput(InputAction.CallbackContext context)
    {
        if (IsMotionLocked) { return; }        
        _steerDirection = context.ReadValue<float>();
    }

    public void HandleJumpInput(InputAction.CallbackContext context)
    {
        if (context.started && !IsMotionLocked)
        {
            _isJumping = true;
        }
    }

    private void Explode()
    {
        Debug.Log("Boom");
        OnExplode.Invoke();
        gameObject.SetActive(false);
    }

    private void OutOfBoundsCheck()
    {
        if (transform.position.y < -10)
        {
            Explode();
        }
    }

    private void Jump()
    {
        if (!_isJumping || IsMotionLocked) { return; }
        if (IsOnGround)
        {
            _rigidBody.AddForce(Vector3.up * _jumpPower, ForceMode.Impulse);
        }
        _isJumping = false;
    }

    private void UpdateVelocity()
    {
        float steeringVelocity = GetSteeringVelocity();
        float gravityVelocity = _rigidBody.velocity.y;
        float forwardVelocity = _speed;
        _rigidBody.velocity = new(steeringVelocity, gravityVelocity, forwardVelocity);
    }

    private float GetSteeringVelocity()
    {
        float steeringVelocity = _steeringSpeed * _steerDirection;
        bool isObstacleToSide = Physics.Raycast(transform.position, Vector3.right * _steerDirection,  _findObstacleDistance, _platformMask);
        if (isObstacleToSide)
        {
            _speed = Mathf.Clamp(_speed - (_accelerationSpeed * Time.fixedDeltaTime), 0, _maxSpeed);
            steeringVelocity = 0;
        }
        return steeringVelocity;
    }

    private void Accelerate()
    {
        if (!_isAccelerating) { return; }
        _speed = Mathf.Clamp(_speed + (_accelerationSpeed * Time.deltaTime), 0, _maxSpeed);
    }

    private void Decelerate()
    {
        if (!_isDecelerating) { return; }
        _speed = Mathf.Clamp(_speed - (_accelerationSpeed * Time.deltaTime), 0, _maxSpeed);
    }

    private void OnCollisionStay(Collision other)
    {
        if (other.gameObject.CompareTag("NoCollision")) { return; }
        ContactPoint contact = other.GetContact(0);
        if (contact.thisCollider.gameObject.CompareTag("ShipNose"))
        {
            OnNoseCollision(other);
        }
    }
}