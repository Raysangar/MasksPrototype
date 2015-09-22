using UnityEngine;
using System.Collections;

public class AvatarMovementController : MonoBehaviour {
    [SerializeField]
    private float avatarSpeed;

    private Animator avatarAnimator;
    private bool movementBlocked;
    private Rigidbody avatarRigidbody;
    private Vector3 currentMovementDirection;

	void Start () {
        avatarAnimator = GetComponent<Animator>();
        movementBlocked = false;
        avatarRigidbody = GetComponent<Rigidbody>();
	}

    void FixedUpdate() {
        if (currentMovementDirection != Vector3.zero && !movementBlocked)
            MoveAvatar();
        else
            Stop();
    }

    public void SetMovement(Vector3 movement) {
        currentMovementDirection = movement.normalized;
    }

    public void Stop() {
        avatarAnimator.SetBool("Run", false);
        avatarRigidbody.velocity = Vector3.zero;
    }

    private void MoveAvatar() {
        avatarAnimator.SetBool("Run", true);
        transform.rotation = Quaternion.LookRotation(currentMovementDirection);
        avatarRigidbody.velocity = currentMovementDirection * avatarSpeed;
    }

    public void AvatarAttacking(bool isAttacking) {
        movementBlocked = isAttacking;
    }

    public void AvatarDashing(bool isDashing) {
        movementBlocked = isDashing;
    }

    public void AvatarDash() {
        AvatarDashToward(transform.forward);
    }

    public void AvatarDashToward(Vector3 dashDirection) {
        if (!movementBlocked) {
            movementBlocked = true;
            transform.rotation = Quaternion.LookRotation(dashDirection);
            avatarAnimator.SetTrigger("Headspring");
        }
    }
}
