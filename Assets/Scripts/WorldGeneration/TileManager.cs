using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;
using Random = UnityEngine.Random;

public class TileManager : MonoBehaviour
{
    [SerializeField]
    private Tile[] _tilePrefabs;

    [FormerlySerializedAs("_currentZ")] [SerializeField]
    private float _spawnZ = 0f;
    [SerializeField]
    private int _tilesOnScreen = 7;
[SerializeField]
    private float _safeZone = 10f;
    
    [SerializeField]
    private Transform _playerTransform;

    private List<Tile> _activeTiles;
    private int _lastPrefabId = -1;

    private float _currentZ;
    
    public void Init(Transform playerTransform)
    {
        _currentZ = _spawnZ;
        _activeTiles = new List<Tile>();

        _playerTransform = playerTransform;
        
        SpawnTiles();
    }

    private void SpawnTiles()
    {
        for (int i = 0; i < _tilesOnScreen; i++)
        {
            _lastPrefabId = GetRandomPrefabId();
            SpawnTile(_lastPrefabId);
        }
    }

    private void Update()
    {
        if (_playerTransform.position.z -_safeZone> (_currentZ - _tilesOnScreen * 10))
        {
            _lastPrefabId = GetRandomPrefabId();
            SpawnTile(_lastPrefabId);
            DeleteTile();
        }
    }

    private void DeleteTile()
    {
        Destroy(_activeTiles[0].gameObject);
        _activeTiles.RemoveAt(0);
    }
    
    
    
    private void SpawnTile(int id)
    {
        var tile = Instantiate(_tilePrefabs[id], Vector3.forward * _currentZ,Quaternion.identity, transform);
        _activeTiles.Add(tile);
        
        _currentZ += tile.Length;
    }

    private int GetRandomPrefabId()
    {
        if (_tilePrefabs.Length < 2)
            return 0;

        var rand = Random.Range(0, _tilePrefabs.Length);
        while (_lastPrefabId == rand)
        {
            rand = Random.Range(0, _tilePrefabs.Length);
        }

        return rand;
    }

    public void Clear()
    {
        for (int i = _activeTiles.Count - 1; i > -1; i -= 1)
        {
            Destroy(_activeTiles[i].gameObject);
            _activeTiles.RemoveAt(i);
        }
        
        _activeTiles.Clear();
    }
}


