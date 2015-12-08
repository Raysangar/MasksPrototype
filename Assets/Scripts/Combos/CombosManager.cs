using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

[RequireComponent(typeof(Animator))]
public class CombosManager : MonoBehaviour {
    [SerializeField]
    private TextAsset comboSheet;

    private Combo currentSequence;
    private string currentComboName;
    private float lastAttackTime;
    private ComboInfo comboInfo;

    public String ComboSheet {
        get {
            if (comboSheet != null)
                return comboSheet.text;
            return "";
        }
    }

    public void AddAttackToCurrentSequence(AttackType attackType) {
        currentSequence.Add(attackType);
        currentComboName = comboOf(currentSequence);
    }

    public void CancelCombo() {
        ComboFinished();
    }

    public bool IsComboAvailableForCurrentSequence() {
        return (currentComboName != "" && comboCorrectlyExecuted());
    }

    public string GetAnimationForCurrentCombo()
    {
        if (currentComboName != "")
            return comboInfo.CombosAnimation[currentComboName];
        return "";
    }

    void Start() {
        comboInfo = new ComboInfo(comboSheet.text);
        currentSequence = new Combo();
        currentComboName = "";
    }

    private bool comboCorrectlyExecuted() {
        float timeSinceLastAttack = Time.time - lastAttackTime;
        TimeRange comboTimeRequirement = comboInfo.CombosTimeRequirement[currentComboName];
        return (comboTimeRequirement.isFalsey() || comboTimeRequirement.valueFitsInRange(timeSinceLastAttack));
    }

    private string comboOf(Combo combo)
    {
        foreach(string comboName in comboInfo.Combos.Keys)
            if (combo.Equals(comboInfo.Combos[comboName]))
                return comboName;
        return "";
    }

    public void ComboFinished() {
        currentSequence.Clear();
        currentComboName = "";
    }

    void OnAttackStarted() {
        lastAttackTime = Time.time;
    }
}
