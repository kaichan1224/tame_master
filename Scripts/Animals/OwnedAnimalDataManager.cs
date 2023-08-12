using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

/// <summary>
/// 所有済みの動物のデータを管理するクラス
/// </summary>
public class OwnedAnimalDataManager : MonoBehaviour
{
    private string prefkey = "OWNED_ANIMAL_DATA";
    public OwnedAnimalData ownedAnimalData;
    [SerializeField] private List<GameObject> animalDatas;
    /// <summary>
    /// 初期化処理
    /// </summary>
    void Awake()
    {
        Load();
    }

    /// <summary>
    /// ロード処理
    /// </summary>
    public void Load()
    {
        //データを読み込む時に、Spriteはデータ化されないため、対応するスプライトを読み込む必要がある.
        if (PlayerPrefs.HasKey(prefkey))
        {
            string loadjson = PlayerPrefs.GetString(prefkey);
            ownedAnimalData = JsonUtility.FromJson<OwnedAnimalData>(loadjson);
            for (int i = 0; i < ownedAnimalData.dataList.Count; i++)
            {
                foreach (var data in animalDatas)
                {
                    var par = data.GetComponent<Animal>().parameter;
                    if (ownedAnimalData.dataList[i].id == par.id)
                    {
                        ownedAnimalData.dataList[i].sprite = par.sprite;
                        break;
                    }
                }
            }
        }
        else
        {
            ownedAnimalData= new OwnedAnimalData();
            ownedAnimalData.dataList = new List<AnimalData>();
        }
    }

    /// <summary>
    /// セーブ処理
    /// </summary>
    private void Save()
    {
        string savejson = JsonUtility.ToJson(ownedAnimalData);
        PlayerPrefs.SetString(prefkey,savejson);
        PlayerPrefs.Save();
    }

    /// <summary>
    /// 動物の追加処理
    /// </summary>
    /// <param name="animalData"></param>
    public void AddData(AnimalData animalData)
    {
        ownedAnimalData.dataList.Add(animalData);
    }

    /// <summary>
    /// アプリ終了時の処理
    /// </summary>
    private void OnDestroy()
    {
        Save();
    }
}
