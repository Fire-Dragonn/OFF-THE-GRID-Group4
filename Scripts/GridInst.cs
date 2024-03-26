using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridInst: MonoBehaviour
{
    [SerializeField] GameObject gridPrefab;
    [SerializeField] float number;
    
    void Start()
    {
        for (int i = 0; i < number; i++)
        {
            for (int j = 0; j < number; j++)
            {
                //GameObject grid = Instantiate(gridPrefab) as GameObject;
                //grid.transform.position = new Vector3( i, j, 0f);
            
                gridPrefab.transform.position = new Vector3(i, j, 0f);
                var spawnedTile = Instantiate(gridPrefab);
                //spawnedTile.name = $"Tile {number}";
            }
        }
    }


    
}
