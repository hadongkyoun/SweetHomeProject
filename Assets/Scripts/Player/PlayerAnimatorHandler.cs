using UnityEngine;

public class PlayerAnimatorHandler : MonoBehaviour
{
    [SerializeField]
    private Animator animator;
    [SerializeField]
    private float animationBlendSpeed;

    private int xVelHash;
    private int yVelHash;


    private void Start()
    {
        xVelHash = Animator.StringToHash("X_Velocity");
        yVelHash = Animator.StringToHash("Y_Velocity");
    }

    public void SetPlayerVelocity(float velX, float velY)
    {
        animator.SetFloat(xVelHash, velX);
        animator.SetFloat(yVelHash, velY);
    }
}
