using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IPlayerDataAccess
{
    StatusSaveData GetPlayerData();
    void LoadPlayerData(StatusSaveData _playerStatusData );
} // IPlayerDataAccess
