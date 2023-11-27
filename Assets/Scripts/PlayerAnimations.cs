using UnityEngine;

public class PlayerAnimations : MonoBehaviour
{
    [SerializeField] private Animator animator;

    private void Start()
    {
        int animNumber = Random.Range(1, 5); // Генерация случайного числа для анимации от 1 до 4
        animator.SetInteger("Anim_Number", animNumber);
    }
}
