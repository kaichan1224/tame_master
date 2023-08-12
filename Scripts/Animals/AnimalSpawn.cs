using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 動物を生成する
/// </summary>
public class AnimalSpawn : MonoBehaviour
{
    //ガリガリランクの動物を格納するリスト
    public List<GameObject> animalsGarigari;
    //ノーマルランクの動物を格納するリスト
    public List<GameObject> animalsNormal;
    //ムキムキランクの動物を格納するリスト
    public List<GameObject> animalsMukimuki;
    //追加キャラのリスト
    public List<GameObject> animalsAdd;
    //ガリガリランクが出現する重み確率
    [SerializeField] private float weightGarigari;
    //ノーマルランクが出現する重み確率
    [SerializeField] private float weightNormal;
    //ムキムキランクが出現する重み確率
    [SerializeField] private float weightMukimuki;
    //追加キャラ
    [SerializeField] private float weightAdd;
    //累計の確率
    private float totalWeight;
    //現在の動物の出現数
    public int currentSpwanCnt = 0;
    //出現数の最大値
    [SerializeField] private int maxSpwanCnt;
    //プレイヤーの現在位置
    [SerializeField] private Transform player;
    //生成間隔
    [SerializeField] private float regulerTime = 10f;
    //出現する半径外側部分
    [SerializeField] private float outside;
    //生成する位置のy座標
    [SerializeField] private float generatePlaceY;
    //出現しない半径内側部分
    [SerializeField] private float inside;
    [SerializeField] private TamingManager tamingManager;

    /// <summary>
    /// 動物生成処理を開始するメソッド
    /// </summary>
    void Start()
    {
        totalWeight = weightGarigari + weightNormal + weightMukimuki + weightAdd;
        StartCoroutine(Generate());
    }

    /// <summary>
    /// ランダムな場所にキャラクターを生成する
    /// </summary>
    void RandomGenerate()
    {
        float x, y, z;
        Vector3 playerPosition = player.position;
        var randomX = Random.Range(inside,outside);
        var randomY = Random.Range(inside, outside);
        if(Random.value < 0.5f)
            randomX *= -1;
        if (Random.value < 0.5f)
            randomY *= -1;
        x = playerPosition.x + randomX;
        y = generatePlaceY;
        z = playerPosition.z + randomY;
        Vector3 generatePlace = new Vector3(x, y, z);
        //ここから重みづけ確率によって作成するキャラクターの比率が変わるようにする
        var randomPoint = Random.Range(0, totalWeight);
        if (randomPoint < weightGarigari)
        {
            GameObject animal = Instantiate(animalsGarigari[Random.Range(0, animalsGarigari.Count)], generatePlace, Quaternion.Euler(0, Random.Range(0f, 360f), 0));
            animal.GetComponent<Animal>().tamingManager = tamingManager;
        }
        else if (randomPoint < weightNormal + weightGarigari)
        {
            GameObject animal = Instantiate(animalsNormal[Random.Range(0, animalsNormal.Count)], generatePlace, Quaternion.Euler(0, Random.Range(0f, 360f), 0));
            animal.GetComponent<Animal>().tamingManager = tamingManager;
        }
        else if (randomPoint < weightNormal + weightGarigari + weightMukimuki)
        {
            GameObject animal = Instantiate(animalsMukimuki[Random.Range(0, animalsMukimuki.Count)], generatePlace, Quaternion.Euler(0, Random.Range(0f, 360f), 0));
            animal.GetComponent<Animal>().tamingManager = tamingManager;
        }
        else
        {
            GameObject animal = Instantiate(animalsAdd[Random.Range(0, animalsAdd.Count)], generatePlace, Quaternion.Euler(0, Random.Range(0f, 360f), 0));
            animal.GetComponent<Animal>().tamingManager = tamingManager;
        }
        currentSpwanCnt++;
    }

    /// <summary>
    /// 定期的に動物を生成するための非同期メソッド
    /// TODO 現在のスポーン量の制御
    /// </summary>
    /// <returns>生成間隔</returns>
    private IEnumerator Generate()
    {
        while (true)
        {
            yield return new WaitForSeconds(regulerTime);
            if(currentSpwanCnt < maxSpwanCnt)
                RandomGenerate();
        }
    }
}
