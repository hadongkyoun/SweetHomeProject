using UnityEngine;

public class EnemyAnimationHandler : MonoBehaviour
{
    private Animator animator;

    private void Awake()
    {
        animator = GetComponent<Animator>();
    }

    public void SetCurrentSpeed(float speed)
    {
        animator.SetFloat("Speed", speed);
    }
}
