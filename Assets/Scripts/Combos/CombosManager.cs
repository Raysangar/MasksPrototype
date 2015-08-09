using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class CombosManager : MonoBehaviour {
    private Combo currentSequence;
    private float lastAttackTime;
    private ComboInfo comboInfo;


    public TextAsset comboSheet;
    public Animator characterAnimator;

	// Use this for initialization
	void Start () {
        comboInfo = new ComboInfo(comboSheet.text);
        currentSequence = new Combo();
	}
	
    void Attack(AttackType attackType)
    {
        string animationTriggerName = selectAnimationAttack(attackType);
        lastAttackTime = Time.time;
        characterAnimator.SetTrigger(animationTriggerName);
    }

    private string selectAnimationAttack(AttackType attack)
    {
        currentSequence.Add(attack);
        if (comboInfo.Combos.ContainsValue(currentSequence))
        {
            string comboName = getComboName(currentSequence);
            float timeSinceLastAttack = Time.time - lastAttackTime;
            KeyValuePair<float, float> comboTimeRequirement = comboInfo.CombosTimeRequirement[comboName];
            if (timeSinceLastAttack >= comboTimeRequirement.Key && timeSinceLastAttack <= comboTimeRequirement.Value)
                return comboInfo.CombosAnimation[comboName];
        }
        currentSequence.Clear();
        currentSequence.Add(attack);
        return comboInfo.CombosAnimation[getComboName(currentSequence)];
    }

    private string getComboName(Combo combo)
    {
        foreach(string comboName in comboInfo.Combos.Keys)
            if (combo.Equals(comboInfo.Combos[comboName]))
                return comboName;
        return "";
    }
}
