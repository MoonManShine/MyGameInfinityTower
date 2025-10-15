using UnityEngine;

public class AttackController : MonoBehaviour
{
    [SerializeField] private Animator animator;
    [SerializeField] private GameObject swordHitBox;
    // [SerializeField] private AudioSource attackSound;
    private bool _isAttack;

    public bool IsAttack { get => _isAttack; }

    public void FinishAttack()
    {
        _isAttack = false;
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            _isAttack = true;
            animator.SetTrigger("Attack1");
            // attackSound.Play();
        }
    }
}
