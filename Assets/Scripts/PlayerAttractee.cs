using UnityEngine;

public class PlayerAttractee : Attractee
{
    [SerializeField] private Transform feetPos;
    private PlayerControl playerControl;

    protected override void Start()
    {
        base.Start();
        playerControl = GetComponent<PlayerControl>();
    }
    
    protected override void Attract()
    {
        if (playerControl.Planet == null)
        {
            AttractManager.AttractAll(_rigidbody);
        }
        else
        {
            AttractManager.AttractSingle(_rigidbody, playerControl.Planet, feetPos.position);
        }
    }
}
