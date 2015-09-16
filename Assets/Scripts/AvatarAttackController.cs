using UnityEngine;
using System.Collections;

public class AvatarAttackController : MonoBehaviour {    
    private bool avatarAttacking;

    void Start() {
        avatarAttacking = false;
    }

    public void AvatarAttacking(bool isAttacking) {
        avatarAttacking = isAttacking;
    }

    void ColliderHit(Collider other) {
        if (avatarAttacking) {
            other.SendMessageUpwards("ReceiveDamage", 5);
        }
    }
}
