using System.Buffers;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// �O���b�h�̃}�X�ڂ̌����ڂ��Ǘ�����N���X
/// </summary>
public class GridSystemVisual : MonoBehaviour
{
    public static GridSystemVisual Instance { get; private set; }

    [Serializable]
    public struct GridVisualTypeMaterial
    {
        public GridVisualType gridVisualType;
        public Material material;
    }

    public enum GridVisualType
    {
        White,
        Blue,
        Red,
        RedSoft,
        Yellow
    }

    [SerializeField]
    private Transform singleGridSystemVisualPrefab;

    [SerializeField]
    private List<GridVisualTypeMaterial> gridVisualTypeMaterialList;

    private SingleGridSystemVisual[,] singleGridSystemVisualArray;

    private void Awake()
    {
        if (Instance != null)
        {
            Debug.LogError("There's more than one GridSystemVisual! " + transform + " - " + Instance);
            Destroy(gameObject);
            return;
        }

        Instance = this;
    }

    private void Start()
    {
        singleGridSystemVisualArray = new SingleGridSystemVisual[
            LevelGrid.Instance.GetWidth(),
            LevelGrid.Instance.GetHeight()
            ];
        for (int x = 0; x < LevelGrid.Instance.GetWidth(); x++)
        {
            for (int z = 0; z < LevelGrid.Instance.GetHeight(); z++)
            {
                GridPosition gridPosition = new GridPosition(x, z);
                Transform gridSystemVisualSingleTransform =
                    Instantiate(singleGridSystemVisualPrefab, LevelGrid.Instance.GetWorldPosition(gridPosition), Quaternion.identity);
                singleGridSystemVisualArray[x, z] = gridSystemVisualSingleTransform.GetComponent<SingleGridSystemVisual>();
            }
        }

        UnitActionSystem.Instance.OnSelectedActionChanged += UnitActionSystem_OnSelectedActionChanged;
        LevelGrid.Instance.OnAnyUnitMovedGridPosiiton += LevelGrid_OnAnyUnitMovedGridPosiiton;
    }

    /// <summary>
    /// ���ׂẴO���b�h�̃}�X���\���ɂ���
    /// </summary>
    public void HideAllGridPosition()
    {
        for (int x = 0; x < LevelGrid.Instance.GetWidth(); x++)
        {
            for (int z = 0; z < LevelGrid.Instance.GetHeight(); z++)
            {
                singleGridSystemVisualArray[x, z].Hide();
            }
        }
    }

    /// <summary>
    /// �͈͓��̃O���b�h�̃}�X��\������
    /// </summary>
    /// <param name="gridPosition">�O���b�h�ɂ�������W</param>
    /// <param name="range">�͈�</param>
    /// <param name="gridVisualType">�O���b�h�̃}�X�ڂ̌�����</param>
    private void ShowGridPositionRange(GridPosition gridPosition, int range, GridVisualType gridVisualType)
    {
        List<GridPosition> gridPositionList = new List<GridPosition>();

        for (int x = -range; x <= range; x++)
        {
            for (int z = -range; z <= range; z++)
            {
                GridPosition testGridPosition = gridPosition + new GridPosition(x, z);

                if (!LevelGrid.Instance.IsValidGridPosition(testGridPosition))
                {
                    continue;
                }

                int testDistance = Mathf.Abs(x) + Mathf.Abs(z);
                if (testDistance > range)
                {
                    continue;
                }

                gridPositionList.Add(testGridPosition);
            }
        }

        ShowGridPositionList(gridPositionList, gridVisualType);
    }

    /// <summary>
    /// �Ή������ʒu�̃}�X�ڂ�\������
    /// </summary>
    /// <param name="gridPositionList">���W���Ǘ����郊�X�g</param>
    /// <param name="gridVisualType">�O���b�h�̃}�X�ڂ̌�����</param>
    public void ShowGridPositionList(List<GridPosition> gridPositionList, GridVisualType gridVisualType)
    {
        foreach (GridPosition gridPosition in gridPositionList)
        {
            singleGridSystemVisualArray[gridPosition.x, gridPosition.z].Show(GetGridVisualTypeMaterial(gridVisualType));
        }
    }

    /// <summary>
    /// �O���b�h�̃}�X�ڂ̌����ڂ��X�V����
    /// </summary>
    private void UpdateGridVisual()
    {
        HideAllGridPosition();

        Unit selectedUnit = UnitActionSystem.Instance.GetSelectedUnit();
        BaseAction selectedAction = UnitActionSystem.Instance.GetSelectedAction();

        GridVisualType gridVisualType = GridVisualType.White;

        switch (selectedAction)
        {
            default:
            case MoveAction moveAction:
                gridVisualType = GridVisualType.White;
                break;
            case ShootAction shootAction:
                gridVisualType = GridVisualType.Red;
                ShowGridPositionRange(selectedUnit.GetGridPosition(), shootAction.GetMaxShootDistance(), GridVisualType.RedSoft);
                break;
        }

        ShowGridPositionList(selectedAction.GetValidActionGridPositionList(), gridVisualType);
    }

    private void UnitActionSystem_OnSelectedActionChanged(object sender, EventArgs e)
    {
        UpdateGridVisual();
    }

    private void LevelGrid_OnAnyUnitMovedGridPosiiton(object sender, EventArgs e)
    {
        UpdateGridVisual();
    }

    /// <summary>
    /// �I�������A�N�V�����ɉ������F�̃}�e���A�����擾����
    /// </summary>
    /// <param name="gridVisualType">�O���b�h�̃}�X�ڂ̌�����</param>
    /// <returns>�}�e���A��</returns>
    private Material GetGridVisualTypeMaterial(GridVisualType gridVisualType)
    {
        foreach (GridVisualTypeMaterial gridVisualTypeMaterial in gridVisualTypeMaterialList)
        {
            if (gridVisualTypeMaterial.gridVisualType == gridVisualType)
            {
                return gridVisualTypeMaterial.material;
            }
        }

        Debug.LogError("Could not find GridVisualTypeMaterial for GridVisualType" + gridVisualType);
        return null;
    }
}
