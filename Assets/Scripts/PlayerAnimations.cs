using UnityEngine;

public class PlayerAnimations : MonoBehaviour
{
    [SerializeField] private Animator animator;

    private void Start()
    {
        int animNumber = Random.Range(1, 5); // ��������� ���������� ����� ��� �������� �� 1 �� 4
        animator.SetInteger("Anim_Number", animNumber);
    }
}
