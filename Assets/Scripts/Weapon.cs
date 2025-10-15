using UnityEngine;

public class Weapon : MonoBehaviour
{
    [SerializeField] private float damage = 20f;
    [SerializeField] private AttackController _attackController;

    private void Start()
    {
        _attackController = transform.root.GetComponent<AttackController>();   
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        EnemyHealth enemyHealth = other.GetComponent<EnemyHealth>();
        if (enemyHealth != null && _attackController.IsAttack)
        {
            enemyHealth.ReduceHealth(damage);
            Debug.Log("Enemy damage");
        }
    }
}
