using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UniRx;
using System;

public class ItemButton : MonoBehaviour
{
    //表示しているアイテムのパラメタ
    public ItemParam itemParam;
    //UI
    [SerializeField] private Button button;
    [SerializeField] private TMP_Text itemName;
    [SerializeField] private Image image;
    public void Init(ItemParam itemParam,Sprite itemSprite)
    {
        this.itemParam = itemParam;
        itemName.text = itemParam.name.ToString();
        image.sprite = itemSprite;
    }

    public Button GetButton()
    {
        return button;
    }
}
