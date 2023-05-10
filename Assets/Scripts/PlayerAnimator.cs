using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public class PlayerAnimator : MonoBehaviour
{
    private const string IS_WALKING = "IsWalking";
    private Animator m_Animator;
    [SerializeField] private Player m_Player;
    

    private void Awake()
    {
        m_Animator = GetComponent<Animator>();
        m_Animator.SetBool(IS_WALKING, false);
    }

    private void Update()
    {
        if (m_Animator != null) {
            m_Animator.SetBool(IS_WALKING, m_Player.IsWalking());
        }
    }
}
