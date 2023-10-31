/**************************************
 *** Designer:AL21115
 *** Date:2023.7.1
 *** 動物がスポーンするための処理モジュール
 **************************************/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using VContainer;

/// <summary>
/// 動物を生成する
/// </summary>
public class AnimalSpawn : MonoBehaviour
{
    private AnimalParamMasterData animalParamMasterData;
    private AnimalMasterData animalMasterData;
    private TameModel tameModel;
    [SerializeField] private Transform player;
    private float regulerTime = 180f;
    private float outside = 25;
    private float generatePlaceY;
    private float inside = 15;
    private List<Animal> generatedAnimal = new List<Animal>();

    [Inject]
    public void Init(AnimalParamMasterData animalParamMasterData,AnimalMasterData animalMasterData,TameModel tameModel)
    {
        this.animalParamMasterData = animalParamMasterData;
        this.animalMasterData = animalMasterData;
        this.tameModel = tameModel;
    }

    public void CreateAnimal(Animal animalPrefab, AnimalParam animalParam,Vector3 position,bool isTouch)
    {
        var animal = Instantiate(animalPrefab,position,Quaternion.identity);
        animal.Init(animalParam,tameModel,isTouch);
        generatedAnimal.Add(animal);
    }

    void RandomGenerateEnemy()
    {
        AnimalParam animalParam = animalParamMasterData.datas[Random.Range(1, animalParamMasterData.datas.Count)];
        Animal animal = animalMasterData.GetAnimal(animalParam.name);
        CreateAnimal(animal, animalParam,GetRandomPlace(),true);
    }

    /// <summary>
    /// 動物生成処理を開始するメソッド
    /// </summary>
    void Start()
    {
        StartCoroutine(Spwan());
    }

    void ClearAnimals()
    {
        foreach (var animal in generatedAnimal)
        {
            //TODOほんとはanimal側のメソッドを呼び起こすべし
            Destroy(animal.gameObject);
        }
        generatedAnimal.Clear();
    }

    /// <summary>
    /// ランダムな場所を出力
    /// </summary>
    Vector3 GetRandomPlace()
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
        return generatePlace;
    }

    /// <summary>
    /// 定期的に動物を生成するための非同期メソッド
    /// TODO 現在のスポーン量の制御
    /// </summary>
    /// <returns>生成間隔</returns>
    private IEnumerator Spwan()
    {
        yield return new WaitForSeconds(2f);
        while (true)
        {
            for (int i = 0; i < 2; i++)
            {
                RandomGenerateEnemy();
            }
            yield return new WaitForSeconds(regulerTime);
            ClearAnimals();
            yield return new WaitForSeconds(5f);
        }
    }
}
