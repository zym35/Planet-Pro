using System.Collections.Generic;
using UnityEngine;

public class PrefabSpawner : MonoBehaviour
{
    [Header("Planet Generation")]
    [SerializeField] private GameObject planetPrefab;
    [SerializeField] private Transform planetsParent;

    [SerializeField] private float spreadRadius;
    [SerializeField] private int minDistance;
    [SerializeField] private int tryNumber;
    [SerializeField][Range(0, .5f)] private float sizeVariation;

    private void Start()
    {
        GeneratePlanets();
    }

    private void GeneratePlanets()
    {
        var pointList = GeneratePositions(transform.position, spreadRadius, spreadRadius, spreadRadius, minDistance,
            tryNumber);
        foreach (var pos in pointList)
        {
            var planet = InstantiatePlanet(planetPrefab, pos, planetsParent);
            AttractManager.AddAttractor(planet.GetComponent<Rigidbody>());
        }
    }

    private GameObject InstantiatePlanet(GameObject prefab, Vector3 pos, Transform parent)
    {
        var planet = Instantiate(prefab, pos, Quaternion.identity, parent);
        
        var sizeV = Random.Range(1 - sizeVariation, 1 + sizeVariation);
        planet.transform.localScale = new Vector3(sizeV, sizeV, sizeV);
        planet.GetComponent<Rigidbody>().mass *= sizeV;
        
        return planet;
    }

    private static List<Vector3> GeneratePositions(Vector3 center, float spreadX, float spreadY, float spreadZ,
        float minD, int tryTimes)
    {
        var pointList = new List<Vector3>();

        for (var i = 0; i < tryTimes; i++)
        {
            var x = Random.Range(center.x - spreadX, center.x + spreadX);
            var y = Random.Range(center.y - spreadY, center.y + spreadY);
            var z = Random.Range(center.z - spreadZ, center.z + spreadZ);
            var pos = new Vector3(x, y, z);

            var isValid = true;
            foreach (var p in pointList)
            {
                if ((Vector3.Distance(pos, p) > minD))
                    continue;
                isValid = false;
                break;
            }

            if (!isValid)
                continue;

            pointList.Add(pos);
        }

        return pointList;
    }
}