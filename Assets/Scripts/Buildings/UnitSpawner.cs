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
        //Спавнит перфаб, который был передан в лист в нетворменеджере
        //Если больше ничего не передать, то он не будет принадлежать ниодному из клиентов, это будет просто серверный объект
        NetworkServer.Spawn(unitInstantiate,connectionToClient);
    }

    #endregion

    #region Client
    //Событие ипоинткликхэнд. Не забыть добавить EventManager, иначе не сработает, ибо этот ивент нужен для канваса)
    public void OnPointerClick(PointerEventData eventData)
    {
        //проверяем, если нажата не левая кнопка, то возврат
        if (eventData.button != PointerEventData.InputButton.Left)
            return;
        //Крч это именно клиента я так понял, если бы мы щелкнули на чужой объект, то скрипт бы не сработал, но варнинг бы высветился, а так все ок
        if (!hasAuthority)
            return;
        CmdSpawnUnit();
    }

    #endregion
}
