using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridManager : MonoBehaviour {
    [SerializeField] private int _width, _height;

    [SerializeField] private Tile _tilePrefab;

    [SerializeField] private Transform _cam;

    [SerializeField] private  Color defaultColor;
    [SerializeField] private Color gateColor;
    [SerializeField] private int startingTokens;



    private Dictionary<Vector2, Tile> _tiles;
    private int currentTokens;

    void Start() {
        GenerateGrid();
        currentTokens = startingTokens;
    }

    void GenerateGrid() {
        _tiles = new Dictionary<Vector2, Tile>();
        for (int x = 0; x < _width; x++) {
            for (int y = 0; y < _height; y++) {
                bool isGateBlock = (x == 0 || x == _width - 1);

                var spawnedTile = Instantiate(_tilePrefab, new Vector3(x, y), Quaternion.identity);
                spawnedTile.name = $"Tile {x} {y}";
                spawnedTile.Init(isGateBlock);

                if(isGateBlock)
                {
                    spawnedTile.GetComponent<SpriteRenderer>().color = gateColor;
                }
                else
                {
                    spawnedTile.GetComponent<SpriteRenderer>().color = defaultColor;          }
              
                var isOffset = (x % 2 == 0 && y % 2 != 0) || (x % 2 != 0 && y % 2 == 0);
                spawnedTile.Init(isOffset);

                
                _tiles[new Vector2(x, y)] = spawnedTile;
            }
        }

        _cam.transform.position = new Vector3((float)_width/2 -0.5f, (float)_height / 2 - 0.5f,-10);
    }

    public Tile GetTileAtPosition(Vector2 pos)
    {
        if (_tiles.TryGetValue(pos, out var tile)) return tile;
        return null;
    }
    public void HandleTokenCollision(Vector2 pos, GameObject tokenObject)
     {
        if (_tiles.TryGetValue(pos, out var tile) && tile.isGateBlock())
         {
             currentTokens--;
            Destroy(tokenObject);
         }
     }
}