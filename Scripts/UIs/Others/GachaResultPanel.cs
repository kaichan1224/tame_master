using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class GachaResultPanel : MonoBehaviour
{
    [SerializeField] TMP_Text rareText;
    [SerializeField] TMP_Text itemText;
    [SerializeField] Image itemImage;
    [SerializeField] Button backButton;
    public void Init(ItemParam itemParam,Sprite sprite)
    {
        rareText.text = itemParam.rarity.ToString();
        itemText.text = itemParam.name.ToString();
        itemImage.sprite = sprite;
    }

    public Button GetBackButton()
    {
        return backButton;
    }

    public void Show()
    {
        gameObject.SetActive(true);
    }

    public void Hide()
    {
        gameObject.SetActive(false);
    }
}
