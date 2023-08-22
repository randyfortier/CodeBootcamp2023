using UnityEngine;

public class Health : MonoBehaviour
{
    [SerializeField] private int maxHP = 100;
    [SerializeField] private bool isDead;
    [SerializeField] private int hp = 100;

    public void TakeDamage(int damage) {
        Animator animator = GetComponent<Animator>();
        this.hp -= damage;
        if (this.hp <= 0) {
            this.hp = 0;
            this.isDead = true;

            
            if (animator != null ) {
                animator.SetBool("Dead", true);
            }
        }
    }
}
