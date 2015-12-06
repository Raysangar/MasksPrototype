using UnityEngine;
using System.Collections;

[RequireComponent(typeof(CombosManager))]
public class AttackController : MonoBehaviour {
    private Animator characterAnimator;
    private CombosManager combosManager;
    private int animationsTriggered;


    void Start () {
        characterAnimator = GetComponent<Animator>();
        combosManager = GetComponent<CombosManager>();
        animationsTriggered = 0;
    }

    public void Attack(AttackType attackType) {
        combosManager.AddAttackToCurrentSequence(attackType);
        if (nextAttackAlreadyTriggered() || !combosManager.IsComboAvailableForCurrentSequence())
            combosManager.CancelCombo();
        else
            triggerAttack(combosManager.GetAnimationForCurrentCombo());
    }

    public void AttackTo(AttackInfo attackInfo) {
        Vector3 target = attackInfo.Target;
        target.y = transform.position.y;
        transform.LookAt(target);
        Attack(attackInfo.AttackType);
    }

    private void triggerAttack(string nextAnimationAttack) {
        ++animationsTriggered;
        characterAnimator.SetTrigger(nextAnimationAttack);
    }

    private bool nextAttackAlreadyTriggered() {
        return animationsTriggered > 1;
    }

    void OnAttackFinished() {
        if (--animationsTriggered == 0)
            combosManager.ComboFinished();
    }
}
