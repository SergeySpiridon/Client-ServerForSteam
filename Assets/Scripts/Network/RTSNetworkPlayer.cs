using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;
public class RTSNetworkPlayer : NetworkBehaviour
{
    [SerializeField]private List<Unit> units = new List<Unit>();

    #region Server
    public override void OnStartServer()
    {
        Unit.ServerOnUnitSpawned += ServerHandleUnitSpawned;
        Unit.ServerOnUnitDespawned += ServerHandleUnitDespawned;
    }

    public override void OnStopServer()
    {
        Unit.ServerOnUnitSpawned -= ServerHandleUnitSpawned;
        Unit.ServerOnUnitDespawned -= ServerHandleUnitDespawned;
    }

    private void ServerHandleUnitSpawned(Unit unit)
    {
        //создаем юнита на сервере и проверяем его валидность
        if (unit.connectionToClient.connectionId != connectionToClient.connectionId)
            return;

        units.Add(unit);
    }
    private void ServerHandleUnitDespawned(Unit unit)
    {
        if (unit.connectionToClient.connectionId != connectionToClient.connectionId)
            return;

        units.Remove(unit);
    }
    #endregion

    #region Client
    public override void OnStartClient()
    {
        if (!isClientOnly)
            return;
        Unit.AuthorityOnUnitSpawned += AuthorityHandleUnitSpawned;
        Unit.AuthorityOnUnitDespawned += AuthorityHandleUnitDespawned;
    }

    public override void OnStopClient()
    {
        if (!isClientOnly)
            return;
        Unit.AuthorityOnUnitSpawned -= AuthorityHandleUnitSpawned;
        Unit.AuthorityOnUnitDespawned -= AuthorityHandleUnitDespawned;
    }

    private void AuthorityHandleUnitSpawned(Unit unit)
    {
        //создаем юнита на сервере и проверяем его валидность
        if (!hasAuthority)
            return;

        units.Add(unit);
    }
    private void AuthorityHandleUnitDespawned(Unit unit)
    {
        if (!hasAuthority)
            return;

        units.Remove(unit);
    }
    #endregion
}
