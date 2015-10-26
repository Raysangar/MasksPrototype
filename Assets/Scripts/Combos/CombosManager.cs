using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class CombosManager : MonoBehaviour {
    private Combo currentSequence;
    private float lastAttackTime;
    private ComboInfo comboInfo;
    private int animationsTriggered;


    public TextAsset comboSheet;
    public Animator characterAnimator;

	// Use this for initialization
	void Start () {
        comboInfo = new ComboInfo(comboSheet.text);
        currentSequence = new Combo();
        animationsTriggered = 0;
	}
	
    void Attack(AttackType attackType)
    {
        string animationTriggerName = selectAnimationAttack(attackType);
        if (!string.IsNullOrEmpty(animationTriggerName)) {
            ++animationsTriggered;
            characterAnimator.SetTrigger(animationTriggerName);
        }
    }

    void AttackTo(AttackInfo attackInfo) {
        Vector3 target = attackInfo.Target;
        target.y = transform.position.y;
        transform.LookAt(target);
        Attack(attackInfo.AttackType);
    }

    private string selectAnimationAttack(AttackType attack)
    {
        currentSequence.Add(attack);
        string comboName = getComboName(currentSequence);
        if (!string.IsNullOrEmpty(comboName)) {
            float timeSinceLastAttack = Time.time - lastAttackTime;
            TimeRange comboTimeRequirement = comboInfo.CombosTimeRequirement[comboName];
            if (comboTimeRequirement.isFalsey() || comboTimeRequirement.valueFitsInRange(timeSinceLastAttack))
                return comboInfo.CombosAnimation[comboName];
        }
        currentSequence.Clear();
        return "";
    }

    private string getComboName(Combo combo)
    {
        foreach(string comboName in comboInfo.Combos.Keys)
            if (combo.Equals(comboInfo.Combos[comboName]))
                return comboName;
        return "";
    }

    void AttackStarted() {
        lastAttackTime = Time.time;
    }

    void AttackFinished() {
        if (--animationsTriggered == 0) {
            lastAttackTime = 0;
            currentSequence.Clear();
        }
    }
}
