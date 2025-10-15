using UnityEngine;
using UnityEngine.UI;

public class EnemyHealth : MonoBehaviour
{
    [SerializeField] private Slider healthSlider;
    [SerializeField] private float totalHealth = 100f;
    [SerializeField] private Animator _animator;
    private float _health;

    private void Start()
    {
        _health = totalHealth;
        InitHealth();
    }

    public void ReduceHealth(float damage)
    {
        _health -= damage;
        Debug.Log("Enemy HP: " + _health);
        InitHealth();
        _animator.SetTrigger("hit_1");
        if (_health <= 0f)
        {
            Die();
        }
    }

    private void InitHealth()
    {
        healthSlider.value = _health / totalHealth;
    }

    private void Die()
    {
        gameObject.SetActive(false);
    }
}
