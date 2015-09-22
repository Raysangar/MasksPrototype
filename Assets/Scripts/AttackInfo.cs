using UnityEngine;
public class AttackInfo {
    public AttackType AttackType { get; private set; }
    public Vector3 Target { get; private set; }

    public AttackInfo (AttackType attackType, Vector3 target) {
        AttackType = attackType;
        Target = target;
    }
}
