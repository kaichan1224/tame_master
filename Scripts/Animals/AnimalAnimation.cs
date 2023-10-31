using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 動物のPrefabにアタッチする
/// </summary>
public class AnimalAnimation : MonoBehaviour
{
    private Animator animator;
    private void Awake()
    {
        animator = GetComponent<Animator>();
    }

    public void SetMoveAnimation(bool flag)
    {
        //歩くアニメーションのオフに
        animator.SetBool("isWalk", flag);
    }
}
