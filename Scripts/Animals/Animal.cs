using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UniRx;

/// <summary>
/// 動物のPrefabにアタッチする
/// 動物に各機能をここで動かす
/// </summary>
public class Animal:MonoBehaviour
{
    private AnimalAnimation animalAnimation;
    private AnimalCollider animalCollider;
    private AnimalParam animalParam;
    private TameModel tameModel;
    private bool isTameMode;
    private void Awake()
    {
        animalAnimation = GetComponent<AnimalAnimation>();
        animalCollider = GetComponent<AnimalCollider>();
    }
    public void Start()
    {
        Bind();
    }

    public void Bind()
    {
        if (isTameMode)
        {
            animalCollider.isClicked
                        .Subscribe(flag =>
                        {
                            Debug.Log((tameModel.GetPlayerPosition() - transform.position).magnitude);
                            if (flag && (tameModel.GetPlayerPosition() - transform.position).magnitude <= 50f)
                            {
                                tameModel.SetAnimal(animalParam);
                                tameModel.SetCurrentState(TameState.開始前);
                            }
                            else
                            {
                                animalCollider.isClicked.Value = false;
                            }
                        })
                        .AddTo(this);
        }
    }

    /// <summary>
    /// インスタンス化するときに必ず渡すパラメータ
    /// </summary>
    /// <param name="animalParam"></param>
    public void Init(AnimalParam animalParam,TameModel tameModel,bool isTameMode)
    {
        this.animalParam = animalParam;
        this.tameModel = tameModel;
        this.isTameMode = isTameMode;
    }
}
