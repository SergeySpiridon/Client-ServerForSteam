using Mirror;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Unit : NetworkBehaviour
{
    [SerializeField] private MovePlayer movePlayer = null;
    [SerializeField] private UnityEvent onSelected = null;
    [SerializeField] private UnityEvent offSelected = null;

    #region Server

    #endregion


    #region Client

    //�����-�� ���� �������, ���������� ���� ��� ��� ����� �����, ����� �������� � ��� ����� �����, �� ����� �����
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
