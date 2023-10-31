using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;
using UnityEngine.SceneManagement;
using VContainer;

public class TameModel
{
    public ReactiveProperty<AnimalParam> tameAnimal = new();
    public ReactiveProperty<TameState> currentState = new(TameState.開始前);
    private AnimalMasterData animalMasterData;
    private UserDataModel userDataModel;
    private Vector2Int targetArea;
    private GameObject player;
    [Inject]
    public TameModel(AnimalMasterData animalMasterData,UserDataModel userDataModel)
    {
        this.animalMasterData = animalMasterData;
        this.userDataModel = userDataModel;
    }

    public void InitPlayer(GameObject player)
    {
        this.player = player;
    }

    public Vector3 GetPlayerPosition()
    {
        return player.transform.position;
    }

    public void SetAnimal(AnimalParam tameAnimal)
    {
        this.tameAnimal.Value = tameAnimal;
    }

    public void SetCurrentState(TameState currentTameState)
    {
        currentState.Value = currentTameState;
    }

    public void CreateTameAnimal()
    {
        var animal = Object.Instantiate(animalMasterData.GetAnimal(tameAnimal.Value.name),new Vector3(0,0,0), Quaternion.identity);
        animal.Init(tameAnimal.Value,this, false);
    }

    public void StartTameScene()
    {
        SceneManager.LoadScene("TameScene");
    }

    public void FinishTameScene()
    {
        SceneManager.LoadScene("MainScene");
    }

    /// <summary>
    /// テイム成功ゾーンの値を決める
    /// </summary>
    public void SetTargetAreaValue(bool isUseItem)
    {
        
        var v = tameAnimal.Value.rank;
        if (isUseItem)
        {
            v += 1;
        }
        switch (v)
        {
            case AnimalParam.ANIMAL_RANK.S:
                targetArea = GenerateRandomPair(0,100,5,10);
                break;
            case AnimalParam.ANIMAL_RANK.A:
                targetArea =  GenerateRandomPair(0,100, 10, 15);
                break;
            case AnimalParam.ANIMAL_RANK.B:
                targetArea =  GenerateRandomPair(0, 100, 15, 20);
                break;
            case AnimalParam.ANIMAL_RANK.C:
                targetArea =  GenerateRandomPair(0, 100, 20, 25);
                break;
            default:
                targetArea = GenerateRandomPair(0, 100, 25, 30);
                break;
        }
    }

    public Vector2Int GetTargetAreaValue()
    {
        return targetArea;
    }

    public Vector2Int GenerateRandomPair(int min, int max, int minDifference, int maxDifference)
    {

        int x = Random.Range(min, max - maxDifference + 1);
        int y = x + Random.Range(minDifference, maxDifference + 1);
        return new Vector2Int(x,y);
    }

    public bool IsSuccess(int stopPosition)
    {
        if (targetArea.x < stopPosition && stopPosition < targetArea.y)
            return true;
        else
            return false;
    }

    public void SuccessTame()
    {
        userDataModel.AddAnimal(tameAnimal.Value);
        userDataModel.UpdateExp(100);
    }
}
