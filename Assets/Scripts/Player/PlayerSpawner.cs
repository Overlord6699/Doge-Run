using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSpawner : MonoBehaviour
{
    [SerializeField]
    private Player _playerPrefab;
    [SerializeField]
    private Transform _playerSpawn;
    
    
    public Player Spawn()
    {
        return Instantiate(_playerPrefab, _playerSpawn);
    }
}
