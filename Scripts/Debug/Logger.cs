using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using VContainer;

public class Logger : MonoBehaviour
{
    public UserDataModel userDataModel;
    public PartyParam partyParam;
    public List<AnimalParam> animals;
    [Inject]
    public void Init(UserDataModel userDataModel)
    {
        this.userDataModel = userDataModel;
    }

    // Update is called once per frame
    void Update()
    {
        partyParam = userDataModel.party.Value;
        animals = userDataModel.ownedAnimals;
    }
}
