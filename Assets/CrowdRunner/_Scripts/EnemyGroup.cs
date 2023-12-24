using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyGroup : MonoBehaviour
{
    [Header(" Elements")]
    [SerializeField] private Enemy enemyPrefab;
    [SerializeField] private Transform enemiesParent;

    [Header(" Settings")]
    [SerializeField] private int amount;
    [SerializeField] private float radius;
    [SerializeField] private float angle;


    private void Start()
    {
        GenerateEnemies();
    }
    private void GenerateEnemies()
    {
        for (int i = 0; i < amount; i++)
        {
            
            Vector3 enemyLocalPosition = GetRunnerLocalPosition(i);

            //convierte la posicion local a world position
            Vector3 enemyWorldPosition = enemiesParent.TransformPoint(enemyLocalPosition);

            Instantiate(enemyPrefab, enemyWorldPosition, Quaternion.identity, enemiesParent);
        }
    }

    private Vector3 GetRunnerLocalPosition(int index)
    {
        //The golden ratio and the golden angle https://en.wikipedia.org/wiki/Fermat%27s_spiral
        //angle = 137.508º;
        //radius = radius * Mathf.Sqrt(index)

        float x = radius * Mathf.Sqrt(index) * Mathf.Cos(Mathf.Deg2Rad * angle * index);
        float z = radius * Mathf.Sqrt(index) * Mathf.Sin(Mathf.Deg2Rad * angle * index);

        return new Vector3(x, 0, z);
    }
}
