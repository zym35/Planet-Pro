using UnityEngine;

public class PlayerStand : MonoBehaviour
{
    private Rigidbody _rigidbody;
    private PlayerControl parentControl;

    private void Start()
    {
        _rigidbody = transform.parent.GetComponent<Rigidbody>();
        parentControl = transform.parent.GetComponent<PlayerControl>();
    }

    private void Update()
    {
        if (parentControl.Planet == null)
        {
            return;
        }

        var gravityUp = (transform.position - parentControl.Planet.position).normalized;
        var targetRotation = Quaternion.FromToRotation(transform.up, gravityUp);
        _rigidbody.MoveRotation(targetRotation * transform.rotation);
    }
    
}