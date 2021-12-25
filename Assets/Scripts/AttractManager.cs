using System.Collections.Generic;
using UnityEngine;

public class AttractManager
{
    private static List<Rigidbody> _attractorPool;
    private const float G = 6.674f;

    public AttractManager()
    {
        _attractorPool = new List<Rigidbody>();
    }

    public static void AddAttractor(Rigidbody a)
    {
        _attractorPool.Add(a);
    }

    public static void AttractAll(Rigidbody body)
    {
        var pos = body.transform.position;
        foreach (var attractor in _attractorPool)
        {
            var force = GetForce(attractor, pos);
            body.AddForce(force, ForceMode.Acceleration);
        }
    }

    public static void AttractAll(Rigidbody body, Vector3 forcePos)
    {
        var pos = body.transform.position;
        foreach (var attractor in _attractorPool)
        {
            var force = GetForce(attractor, pos);
            body.AddForceAtPosition(force, forcePos, ForceMode.Acceleration);
        }
    }

    private static Vector3 GetForce(Rigidbody attractor, Vector3 pos)
    {
        var direction = attractor.position - pos;
        var distance = direction.magnitude;

        var forceMagnitude = G * attractor.mass / Mathf.Pow(distance, 2);
        return direction.normalized * forceMagnitude;
    }

    public static void AttractSingle(Rigidbody active, Rigidbody passive, Vector3 forcePos)
    {
        var pos = active.transform.position;
        var force = GetForce(passive, pos);
        active.AddForceAtPosition(force, forcePos, ForceMode.Acceleration);
    }
}