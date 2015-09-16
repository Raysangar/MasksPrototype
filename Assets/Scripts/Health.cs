using UnityEngine;
using UnityEngine.UI;

public class Health : MonoBehaviour {
    [SerializeField]
    private Slider healthBar;

    [SerializeField]
    private float totalLife = 100f;

    private float currentLife;
    private bool avatarIsInvulnerable;

    void Start() {
        currentLife = totalLife;
        avatarIsInvulnerable = false;
    }

    void ReceiveDamage(int damage) {
        if (avatarIsInvulnerable)
            return;
        currentLife -= damage;
        healthBar.value = currentLife / totalLife;
        if (currentLife <= 0)
            AvatarDead();
    }

    void AvatarDead() {

    }

    public void AvatarDashing(bool isDashing) {
        avatarIsInvulnerable = isDashing;
    }
}
