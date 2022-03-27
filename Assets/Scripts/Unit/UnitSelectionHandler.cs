using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;
using UnityEngine.InputSystem;
using System;

public class UnitSelectionHandler : NetworkBehaviour
{
    //Скорее всего нужно, чтобы потом рейкаст реагировал только на объекты относящиеся к одному слою
    [SerializeField] private LayerMask layerMask = new LayerMask();
    private Camera mainCamera;
    //Будем хранить здесь всех выделенных юнитов
    public List<Unit> SelectedUnits { get; } = new List<Unit>();

    [SerializeField] private RectTransform unitSelectionArea = null;
    private RTSNetworkPlayer player;
    private Vector2 startPosition;

    private void Start()
    {
        //ссылка на камеру
        mainCamera = Camera.main;
        //берем наше соединение, берем компонент со скриптом
        player = NetworkClient.connection.identity.GetComponent<RTSNetworkPlayer>();
    }
    private void Update()
    {
        //Проверка нажатия левой кнопки мыши, это новая система инпутов, добавленная вначале
        if (Mouse.current.leftButton.wasPressedThisFrame)
        {
            StartSelectionArea();
        }
        else if (Mouse.current.leftButton.wasReleasedThisFrame)
        {
            ClearSelectionArea();
        }
        else if (Mouse.current.leftButton.IsPressed())
        {
            UpdateSelectionArea();
        }
    }

    private void UpdateSelectionArea()
    {
        Vector2 mousePosition = Mouse.current.position.ReadValue();
        float areaWidth = mousePosition.x - startPosition.x;
        float areaHeight = mousePosition.y - startPosition.y;

        //размер между двумя значениями
        unitSelectionArea.sizeDelta = new Vector2(Mathf.Abs(areaWidth), Mathf.Abs(areaHeight));
        unitSelectionArea.anchoredPosition = startPosition + new Vector2(areaWidth / 2, areaHeight / 2);
    }
    private void StartSelectionArea()
    {
        foreach (var selectedUnit in SelectedUnits)
        {
            selectedUnit.Deselected();
        }
        SelectedUnits.Clear();
        unitSelectionArea.gameObject.SetActive(true);
        startPosition = Mouse.current.position.ReadValue();
        
    }
    private void ClearSelectionArea()
    {
        //Возвращает луч, который идет от камеры через точку, а точка у нас позиция мыши
        Ray ray = mainCamera.ScreenPointToRay(Mouse.current.position.ReadValue());
        //Если ничего не случилось
        //Сам луч, то что пересек, дальность луча 
        if (!Physics.Raycast(ray, out RaycastHit hit, Mathf.Infinity, layerMask))
            return;
        //Если в рэйкаст попал не в юнита, то ретурн
        if (!hit.collider.TryGetComponent<Unit>(out Unit unit))
            return;
        //если юнит не принадлежит нашему клиенту, то ретурн
        if (!unit.hasAuthority)
            return;
        SelectedUnits.Add(unit);

        foreach (var selectedUnit in SelectedUnits)
        {
            selectedUnit.Selected();
        }
    }
}
