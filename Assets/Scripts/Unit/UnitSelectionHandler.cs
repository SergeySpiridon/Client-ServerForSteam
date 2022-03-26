using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;
using UnityEngine.InputSystem;
using System;

public class UnitSelectionHandler : NetworkBehaviour
{
    //������ ����� �����, ����� ����� ������� ���������� ������ �� ������� ����������� � ������ ����
    [SerializeField] private LayerMask layerMask = new LayerMask();
    private Camera mainCamera;
    //����� ������� ����� ���� ���������� ������
    public List<Unit> SelectedUnits { get; } = new List<Unit>();

    private void Start()
    {
        //������ �� ������
        mainCamera = Camera.main;
    }
    private void Update()
    {
        //�������� ������� ����� ������ ����, ��� ����� ������� �������, ����������� �������
        if (Mouse.current.leftButton.wasPressedThisFrame)
        {
            foreach (var selectedUnit in SelectedUnits)
            {
                selectedUnit.Deselected();
            }
            SelectedUnits.Clear();
        }
            else if (Mouse.current.leftButton.wasReleasedThisFrame)
        {
            ClearSelectionArea();
        }
    }

    private void ClearSelectionArea()
    {
        //���������� ���, ������� ���� �� ������ ����� �����, � ����� � ��� ������� ����
        Ray ray = mainCamera.ScreenPointToRay(Mouse.current.position.ReadValue());
        //���� ������ �� ���������
        //��� ���, �� ��� �������, ��������� ���� 
        if (!Physics.Raycast(ray, out RaycastHit hit, Mathf.Infinity, layerMask))
            return;
        //���� � ������� ����� �� � �����, �� ������
        if (!hit.collider.TryGetComponent<Unit>(out Unit unit))
            return;
        //���� ���� �� ����������� ������ �������, �� ������
        if (!unit.hasAuthority)
            return;
        SelectedUnits.Add(unit);

        foreach(var selectedUnit in SelectedUnits)
        {
            selectedUnit.Selected();
        }
    }
}
