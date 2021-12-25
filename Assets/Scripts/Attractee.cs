using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class Attractee : MonoBehaviour
{
    [SerializeField] protected float mass;
    protected Rigidbody _rigidbody;
    
    protected virtual void Start()
    {
        _rigidbody = GetComponent<Rigidbody>();
        _rigidbody.mass = mass;
    }

    private void FixedUpdate()
    {
        Attract();
    }

    protected virtual void Attract()
    {
        AttractManager.AttractAll(_rigidbody);
    }
}