using Mirror;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class UnitSpawner : NetworkBehaviour, IPointerClickHandler
{
    [SerializeField] private GameObject unitPrefab = null;
    [SerializeField] private Transform unitSpawnPoint = null;


    #region Server
    [Command]
    private void CmdSpawnUnit()
    {
        GameObject unitInstantiate = Instantiate
            (unitPrefab, unitSpawnPoint.position, unitSpawnPoint.rotation);
        //������� ������, ������� ��� ������� � ���� � ���������������
        //���� ������ ������ �� ��������, �� �� �� ����� ������������ �������� �� ��������, ��� ����� ������ ��������� ������
        NetworkServer.Spawn(unitInstantiate,connectionToClient);
    }

    #endregion

    #region Client
    //������� ��������������. �� ������ �������� EventManager, ����� �� ���������, ��� ���� ����� ����� ��� �������)
    public void OnPointerClick(PointerEventData eventData)
    {
        //���������, ���� ������ �� ����� ������, �� �������
        if (eventData.button != PointerEventData.InputButton.Left)
            return;
        //��� ��� ������ ������� � ��� �����, ���� �� �� �������� �� ����� ������, �� ������ �� �� ��������, �� ������� �� ����������, � ��� ��� ��
        if (!hasAuthority)
            return;
        CmdSpawnUnit();
    }

    #endregion
}
