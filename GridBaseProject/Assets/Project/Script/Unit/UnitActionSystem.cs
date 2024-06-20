using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.EventSystems;

/// <summary>
/// ���j�b�g�̃A�N�V�������Ǘ�����N���X
/// </summary>
public class UnitActionSystem : MonoBehaviour
{
    public static UnitActionSystem Instance {  get; private set; }

    public event EventHandler OnSelectedUnitChanged;
    public event EventHandler OnSelectedActionChanged;
    public event EventHandler<bool> OnBusyChanged;
    public event EventHandler OnActionStarted;

    [SerializeField]
    private Unit selectedUnit = null;

    [SerializeField]
    private LayerMask unitLayerMask;

    private BaseAction selectedAction;
    private bool isBusy;
    private bool isLateSetUp;

    private void Awake()
    {
        if(Instance != null)
        {
            Debug.LogError("There's more than one UnitActionSystem! " + transform + " - " + Instance);
            Destroy(gameObject);
            return;
        }

        Instance = this;
    }

    private void Start()
    {
        isLateSetUp = false;
        Unit.OnAnyUnitDead += Unit_OnAnyUnitDead;
    }

    private void Update()
    {
        if (isBusy)
        {
            return;
        }

        if (!TurnSystem.Instance.IsPlayerTurn())
        {
            return;
        }

        if (EventSystem.current.IsPointerOverGameObject())
        {
            return;
        }

        if (TryHandleUnitSelection())
        {
            return;
        }
        
        HandleSelectedAction();
    }

    private void LateUpdate()
    {
        if (isLateSetUp)
        {
            return;
        }

        SetSelectedUnit(selectedUnit);
        isLateSetUp = true;
    }

    /// <summary>
    /// �I�������A�N�V���������s����
    /// </summary>
    private void HandleSelectedAction()
    {
        if (Mouse.current.leftButton.wasPressedThisFrame)
        {
            GridPosition mouseGridPosition = LevelGrid.Instance.GetGridPosition(MouseWorld.GetPosition());

            if (!selectedAction.IsValidActionGridPosition(mouseGridPosition))
            {
                return;
            }
            if (!selectedUnit.TrySpendActionPointsToTakeAction(selectedAction))
            {
                return;
            }

            SetBusy();
            selectedAction.TakeAction(mouseGridPosition, ClearBusy);

            OnActionStarted?.Invoke(this, EventArgs.Empty);
        }
    }

    /// <summary>
    /// ���j�b�g�̏�Ԃ��A�N�V���������s���ɐݒ肷��
    /// </summary>
    private void SetBusy()
    {
        isBusy = true;
        OnBusyChanged?.Invoke(this, isBusy);
    }

    /// <summary>
    /// ���j�b�g�̃A�N�V�������I��������Ԃɐݒ肷��
    /// </summary>
    private void ClearBusy()
    {
        isBusy = false;
        OnBusyChanged?.Invoke(this, isBusy);
    }

    /// <summary>
    /// ���j�b�g���I���\���ǂ����𔻒肷��
    /// </summary>
    /// <returns>�I���\�ȏꍇTrue�A�s�ȏꍇFalse��Ԃ�</returns>
    private bool TryHandleUnitSelection()
    {
        if (Mouse.current.leftButton.wasPressedThisFrame)
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out RaycastHit raycastHit, float.MaxValue, unitLayerMask))
            {
                if (raycastHit.transform.TryGetComponent<Unit>(out Unit unit))
                {
                    if(unit == selectedUnit)
                    {
                        return false;
                    }

                    if (unit.IsEnemy())
                    {

                        return false;
                    }

                    SetSelectedUnit(unit);
                    return true;
                }
            }
        }
            
        return false;
    }

    /// <summary>
    /// ���j�b�g��I�������ۂ̏������s��
    /// </summary>
    /// <param name="unit">�Ώۂ̃��j�b�g</param>
    private void SetSelectedUnit(Unit unit)
    {
        selectedUnit = unit;
        SetSelectedAction(unit.GetAction<MoveAction>());
        OnSelectedUnitChanged?.Invoke(this, EventArgs.Empty);
    }

    /// <summary>
    /// �A�N�V������I�������ۂ̏������s��
    /// </summary>
    /// <param name="baseAction">�Ώۂ̃A�N�V����</param>
    public void SetSelectedAction(BaseAction baseAction)
    {
        selectedAction = baseAction;
        OnSelectedActionChanged?.Invoke(this, EventArgs.Empty);
    }

    /// <summary>
    /// �I���������j�b�g�̏���Ԃ�
    /// </summary>
    /// <returns>�I���������j�b�g</returns>
    public Unit GetSelectedUnit()
    {
        return selectedUnit;
    }

    /// <summary>
    /// �I�������A�N�V�����̏���Ԃ�
    /// </summary>
    /// <returns>�I�������A�N�V����</returns>
    public BaseAction GetSelectedAction()
    {
        return selectedAction;
    }

    private void Unit_OnAnyUnitDead(object sender, EventArgs e)
    {
        Unit unit = sender as Unit;

        if (unit == selectedUnit)
        {
            selectedUnit = null;
        }
    }
}
