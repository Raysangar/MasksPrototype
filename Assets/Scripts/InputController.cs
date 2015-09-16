using UnityEngine;
using System.Collections;
using System;

public class InputController : MonoBehaviour {
    private Vector3 lastMovementInput;
    private Vector3 movementInput;
    private Vector3 dashDirection;

    void Start () {
        movementInput       = Vector3.zero;
        dashDirection       = Vector3.zero;
        lastMovementInput   = Vector3.zero;
    }
	
	void Update () {
        CheckDashInput();
        CheckAttackInput();
        CheckMovementInput();
    }

    private void CheckAttackInput() {
        if (Input.GetButtonDown("WeakAttack"))
            SendMessage("Attack", AttackType.WeakAttack);
        else if (Input.GetButtonDown("MouseWeakAttack")) {
            SendMessage("Attack", AttackType.WeakAttack);
        }

        if (Input.GetButtonDown("StrongAttack"))
            SendMessage("Attack", AttackType.StrongAttack);
        else if (Input.GetButtonDown("MouseStrongAttack")) {
            SendMessage("Attack", AttackType.StrongAttack);
        }
    }

    private void CheckDashInput() {
        if (Input.GetAxis("DashW") > 0 || Input.GetButtonDown("Dash"))
            SendMessage("AvatarDash");
        else {
            dashDirection.x = Input.GetAxis("DashHorizontalWL");
            dashDirection.z = Input.GetAxis("DashVerticalWL");
            if (dashDirection.sqrMagnitude > 0)
                SendMessage("AvatarDashToward", dashDirection);
        }
    }

    private void CheckMovementInput() {
        movementInput.x = Input.GetAxis("Horizontal");
        movementInput.z = Input.GetAxis("Vertical");
        if (!movementInput.Equals(lastMovementInput)) {
            SendMessage("SetMovement", movementInput);
            lastMovementInput = movementInput;
        }
    }
}
