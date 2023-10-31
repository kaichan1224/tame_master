using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// プレイヤーのアニメーションの切り替えを行うためのクラス
/// </summary>
public class PlayerAnimation : MonoBehaviour
{
    [SerializeField] private Animator animator;
    public void SetMoveAnimation(bool flag)
    {
        //歩くアニメーションのオフに
        animator.SetBool("isWalking",flag);
    }
}
