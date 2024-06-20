using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.EventSystems;

/// <summary>
/// ユニットのアクションを管理するクラス
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
    /// 選択したアクションを実行する
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
    /// ユニットの状態をアクションを実行中に設定する
    /// </summary>
    private void SetBusy()
    {
        isBusy = true;
        OnBusyChanged?.Invoke(this, isBusy);
    }

    /// <summary>
    /// ユニットのアクションが終了した状態に設定する
    /// </summary>
    private void ClearBusy()
    {
        isBusy = false;
        OnBusyChanged?.Invoke(this, isBusy);
    }

    /// <summary>
    /// ユニットが選択可能かどうかを判定する
    /// </summary>
    /// <returns>選択可能な場合True、不可な場合Falseを返す</returns>
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
    /// ユニットを選択した際の処理を行う
    /// </summary>
    /// <param name="unit">対象のユニット</param>
    private void SetSelectedUnit(Unit unit)
    {
        selectedUnit = unit;
        SetSelectedAction(unit.GetAction<MoveAction>());
        OnSelectedUnitChanged?.Invoke(this, EventArgs.Empty);
    }

    /// <summary>
    /// アクションを選択した際の処理を行う
    /// </summary>
    /// <param name="baseAction">対象のアクション</param>
    public void SetSelectedAction(BaseAction baseAction)
    {
        selectedAction = baseAction;
        OnSelectedActionChanged?.Invoke(this, EventArgs.Empty);
    }

    /// <summary>
    /// 選択したユニットの情報を返す
    /// </summary>
    /// <returns>選択したユニット</returns>
    public Unit GetSelectedUnit()
    {
        return selectedUnit;
    }

    /// <summary>
    /// 選択したアクションの情報を返す
    /// </summary>
    /// <returns>選択したアクション</returns>
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
