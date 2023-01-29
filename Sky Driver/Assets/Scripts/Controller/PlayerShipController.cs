using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;

[RequireComponent(typeof(Rigidbody))]
public class PlayerShipController : MonoBehaviour
{
    [SerializeField]
    private LayerMask _platformMask;
    [SerializeField]
    private GameObject _playerModel;
    [SerializeField]
    private GameObject _explosion;
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

    private Collider[] _shipColliders;
    private Vector3 _startPosition;
    private bool _isAccelerating;
    private bool _isDecelerating;
    private bool _isJumpQueued;
    private bool _isExploded;

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

    public void CheckCollision()
    {
        RaycastHit hit;
        bool isCrashing = Physics.Raycast(transform.position, Vector3.forward, out hit, .6f, _platformMask);
        if (!isCrashing) { return; }
        if (_speed > _maxSpeed * 0.5f)
        {
            Explode();
        }
        if (hit.distance <= 0)
        {
            Vector3 toUpdate = transform.position;
            toUpdate.z -= .25f;
            transform.position = toUpdate;
        }
        _speed = 0;
    }

    public void Reset()
    {
        transform.position = _startPosition;
        _rigidBody.velocity = Vector3.zero;
        IsMotionLocked = false;
        _speed = 0;
        _isJumpQueued = false;
        _isAccelerating = false;
        _isDecelerating = false;
        _isExploded = false;
        _playerModel.SetActive(true);
        _explosion.SetActive(false);
        GetComponent<Rigidbody>().useGravity = true;
    }

    public void ExitLevel()
    {
        _playerModel.SetActive(false);
        GetComponent<Rigidbody>().useGravity = false;
    }

    protected void Awake()
    {
        _startPosition = transform.position;
        _rigidBody = GetComponent<Rigidbody>();
        _shipColliders = GetComponentsInChildren<Collider>().Where(c => !c.isTrigger).ToArray();
        Debug.Assert(_rigidBody != null, "Could not find RigidBody");
    }

    protected void Update()
    {
        Accelerate();
        Decelerate();
        OutOfBoundsCheck();
        CheckForFall();
        CheckCollision();
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
        _steerDirection = IsMotionLocked ? 0 : context.ReadValue<float>();
    }

    public void HandleJumpInput(InputAction.CallbackContext context)
    {
        if (context.started && !IsMotionLocked)
        {
            _isJumpQueued = true;
        }
    }

    public void Explode()
    {
        OnExplode.Invoke();
        _playerModel.SetActive(false);
        _explosion.SetActive(true);
        _isExploded = true;
    }

    public void Decelerate(float value) => _speed = Mathf.Clamp(_speed - value, 0, _maxSpeed);
    public void Accelerate(float value) => _speed = Mathf.Clamp(_speed + value, 0, _maxSpeed);

    private void CheckForFall()
    {
        bool isGroundBelow = Physics.Raycast(transform.position, Vector3.down, 10f, _platformMask);
        foreach (Collider collider in _shipColliders)
        {
            collider.enabled = isGroundBelow;
        }
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
        if (!_isJumpQueued || IsMotionLocked) { return; }
        if (IsOnGround)
        {
            Vector3 velocity = _rigidBody.velocity;
            velocity.y = 0;
            _rigidBody.velocity = velocity;
            _rigidBody.AddForce(Vector3.up * _jumpPower, ForceMode.Impulse);
        }
        _isJumpQueued = false;
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
            Decelerate(_accelerationSpeed * Time.fixedDeltaTime);
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
        if (_isExploded) 
        {
            Decelerate(_accelerationSpeed * 6 * Time.deltaTime);
        }
        if (!_isDecelerating) { return; }
        Decelerate(_accelerationSpeed * Time.deltaTime);
    }

}