using Mirror;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Unit : NetworkBehaviour
{
    [SerializeField] private MovePlayer movePlayer = null;
    [SerializeField] private UnityEvent onSelected = null;
    [SerializeField] private UnityEvent offSelected = null;

    //Server
    public static event Action<Unit> ServerOnUnitSpawned;
    public static event Action<Unit> ServerOnUnitDespawned;

    //Client
    public static event Action<Unit> AuthorityOnUnitSpawned;
    public static event Action<Unit> AuthorityOnUnitDespawned;

    #region Server

    #endregion

    public override void OnStartServer()
    {
        ServerOnUnitSpawned?.Invoke(this);
    }
    public override void OnStopServer()
    {
        ServerOnUnitDespawned?.Invoke(this);
    }

    #region Client

    [Client]
    public override void OnStartClient()
    {
        //если не сервер и если не принадлежит нам
        if (!hasAuthority || !isClientOnly)
            return;
        AuthorityOnUnitSpawned?.Invoke(this);
    }
    [Client]
    public override void OnStopClient()
    {
        if (!hasAuthority || !isClientOnly)
            return;
        AuthorityOnUnitDespawned?.Invoke(this);
    }


    //Какой-то явно паттерн, скалдываем сюда все что юниты могут, можно обойтись и без этого конеч, но пусть будет
    [Client]
    public MovePlayer GetUnitMovement()
    {
        return movePlayer;
    }


    [Client]
    public void Selected()
    {
        if (!hasAuthority)
            return;
        onSelected?.Invoke();
    }
    [Client]
    public void Deselected()
    {
        if (!hasAuthority)
            return;
        offSelected?.Invoke();
    }
    #endregion
}
