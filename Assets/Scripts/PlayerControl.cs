using System;
using System.Collections;
using UnityEngine;

public class PlayerControl : MonoBehaviour
{
    public Rigidbody Planet { get; private set; }

    [SerializeField] private float speed;
    [SerializeField] private float jumpForce;
    
    private Rigidbody _rbody;

    private void Start()
    {
        _rbody = GetComponent<Rigidbody>();
    }

    private void Update()
    {
        HandleMove();
        HandleJump();
        HandleLook();
    }

    private void HandleMove()
    {
        var movementRaw = InputManager.Instance.GetMovementInput();
        if (movementRaw.magnitude == 0)
            return;
        OnMove(movementRaw);
    }

    private void OnMove(Vector2 movementRaw)
    {
        var movement = new Vector3(movementRaw.x, 0, movementRaw.y);
        _rbody.MovePosition(transform.TransformDirection(movement * speed * Time.deltaTime) + _rbody.position);
    }

    private void HandleJump()
    {
        var jumped = InputManager.Instance.GetJumpInputThisFrame();
        if (!jumped)
            return;
        OnJump();
    }

    private void OnJump()
    {
        _rbody.AddRelativeForce(Vector3.up * jumpForce, ForceMode.Impulse);
    }

    private void HandleLook()
    {
        var delta = InputManager.Instance.GetMouseDeltaInput();
        if (delta.magnitude == 0)
            return;
        OnLook(delta);
    }

    private void OnLook(Vector2 delta)
    {
        var mouseX = delta.x * 80 * Time.deltaTime;
        var mouseY = delta.y * 80 * Time.deltaTime;
        
        transform.Rotate(transform.up * mouseX);

    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("PlanetAtmosphere"))
        {
            Planet = other.attachedRigidbody;
            StartCoroutine(Landing());
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("PlanetAtmosphere"))
        {
            Planet = null;
        }
    }

    private IEnumerator Landing()
    {
        while (_rbody.velocity.magnitude > 5)
        {
            _rbody.velocity *= .75f;
            yield return new WaitForFixedUpdate();
        }
    }
}