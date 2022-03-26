using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;
using UnityEngine.InputSystem;
using System;

public class UnitCommand : NetworkBehaviour
{
    //ссылка на выбранных юнитов
    [SerializeField] private UnitSelectionHandler unitSelectionHandler = null;
  //  [SerializeField] private LayerMask layerMask = new LayerMask();
   // private MovePlayer movePlayer;
    private Camera mainCamera; 
    private void Start()
    {
      //  mainCamera = Camera.main;
  //      movePlayer = GetComponent<MovePlayer>();
    }
    private void Update()
    {
        TryMove();
        
    }

    private void TryMove()
    {
        foreach (var unit in unitSelectionHandler.SelectedUnits)
        {
  //          Debug.Log(unit);
            unit.GetUnitMovement().Move();
        }
    }
}
