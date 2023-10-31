using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using VContainer;
using UniRx;
using System;

public class AnimalPanel : MonoBehaviour
{
    [SerializeField] TMP_Text charaNameText;
    [SerializeField] TMP_Text expText;
    [SerializeField] TMP_Text hpText;
    [SerializeField] TMP_Text attackText;
    [SerializeField] TMP_Text defenseText;
    [SerializeField] TMP_Text speedText;
    [SerializeField] TMP_Text weapon1NameText;
    [SerializeField] TMP_Text weapon2NameText;
    [SerializeField] TMP_Text weapon1StatusText;
    [SerializeField] TMP_Text weapon2StatusText;
    [SerializeField] private Button exitButton;
    [SerializeField] private Camera uiCamera;
    public IObservable<Unit> onExit => exitButton.OnClickAsObservable();
    private ActionMasterData actionMasterData;
    private AnimalMasterData animalMasterData;
    private GameObject cloneAnimal;
    private bool isTurnPush;
    private bool isUpPush;
    private bool isDownPush;
    private float currentViewPoint;

    [Inject]
    public void Inject(ActionMasterData actionMasterData,AnimalMasterData animalMasterData)
    {
        this.actionMasterData = actionMasterData;
        this.animalMasterData = animalMasterData;
    }
    public void Init(AnimalParam animalParam)
    {
        currentViewPoint = uiCamera.fieldOfView;
        var obj = animalMasterData.GetAnimal(animalParam.name).gameObject;
        cloneAnimal = Instantiate(obj, new Vector3(0, -12, -9), Quaternion.identity);
        ChangeLayerRecursively(cloneAnimal.transform, "UIObj");
        charaNameText.text = animalParam.name.ToString();
        expText.text = animalParam.explain;
        hpText.text = $"HP {animalParam.status.Hp.ToString()}";
        attackText.text = $"攻撃 {animalParam.status.Attack.ToString()}";
        defenseText.text = $"防御 {animalParam.status.Defense.ToString()}";
        speedText.text = $"スピード {animalParam.status.Speed.ToString()}";
        weapon1NameText.text = animalParam.actionType1.ToString();
        weapon2NameText.text = animalParam.actionType2.ToString();
        var weapon1 = actionMasterData.GetAction(animalParam.actionType1);
        var weapon2 = actionMasterData.GetAction(animalParam.actionType2);
        weapon1StatusText.text = $"攻撃力{weapon1.skillAttack}　命中率{weapon1.hitRate}　\n会心率{weapon1.criticalRate}　回復量{weapon1.healValue}";
        weapon2StatusText.text = $"攻撃力{weapon2.skillAttack}　命中率{weapon2.hitRate}　\n会心率{weapon2.criticalRate}　回復量{weapon2.healValue}";
    }

    // 子オブジェクトのレイヤーを再帰的に変更する関数
    void ChangeLayerRecursively(Transform currentTransform, string layerName)
    {
        currentTransform.gameObject.layer = LayerMask.NameToLayer(layerName);

        // 子オブジェクトがある場合、再帰的に処理を続ける
        for (int i = 0; i < currentTransform.childCount; i++)
        {
            Transform child = currentTransform.GetChild(i);
            ChangeLayerRecursively(child, layerName);
        }
    }

    public void TurnPushDown()
    {
        isTurnPush = true;
    }

    public void TurnPushUp()
    {
        isTurnPush = false;
    }

    public void UpPushDown()
    {
        isUpPush = true;
    }

    public void UpPushUp()
    {
        isUpPush = false;
    }

    public void DownPushDown()
    {
        isDownPush = true;
    }

    public void DownPushUp()
    {
        isDownPush = false;
    }

    private void Update()
    {
        if (isTurnPush)
        {
            cloneAnimal.transform.Rotate(new Vector3(0, 90 * Time.deltaTime, 0));
        }
        if (isDownPush)
        {
            currentViewPoint = Mathf.Clamp(currentViewPoint + 60 * Time.deltaTime, 5, 150);
            uiCamera.fieldOfView = currentViewPoint;
        }
        if (isUpPush)
        {
            currentViewPoint = Mathf.Clamp(currentViewPoint - 60 * Time.deltaTime, 5, 150);
            uiCamera.fieldOfView = currentViewPoint;
        }
    }

    public void Show()
    {
        this.gameObject.SetActive(true);
    }

    public void Hide()
    {
        this.gameObject.SetActive(false);
        Destroy(cloneAnimal);
    }
}
