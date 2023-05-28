using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Barack : Building
{
    [SerializeField] private Transform _spawnPoint;

    public void CreateUnit(GameObject unitPrefab)
    {
        GameObject newUnit = Instantiate(unitPrefab, _spawnPoint.position, Quaternion.identity);
        Vector3 newPosition = _spawnPoint.position + new Vector3(Random.Range(-1f, 1f), 0, Random.Range(-1f, 1f));
        newUnit.GetComponent<Unit>().WhenClickOnGround(newPosition);
    }
}
