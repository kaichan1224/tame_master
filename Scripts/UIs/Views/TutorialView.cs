using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using TMPro;
using VContainer;

public class TutorialView : MonoBehaviour
{
    [SerializeField] Text text;
    [SerializeField] TMP_InputField nameInput;
    UserDataModel userDataModel;

    [Inject]
    public void Inject(UserDataModel userDataModel)
    {
        this.userDataModel = userDataModel;
    }

    //各セリフが最後まで再生されたか
    public bool isEndWord = false;
    public IEnumerator SetWord(string word)
    {
        isEndWord = false;
        yield return text.DOText(word,word.Length *0.1f).SetEase(Ease.Linear).WaitForCompletion();
        isEndWord = true;
        yield break;
    }

    public void ResetWord()
    {
        text.text = "";
    }

    public IEnumerator SetName(string word)
    {
        yield return text.DOText(word, word.Length * 0.1f).SetEase(Ease.Linear).WaitForCompletion();
        nameInput.ActivateInputField();
        while (true)
        {
            if (nameInput.text != "")
            {
                userDataModel.SetName(nameInput.text);
                break;
            }
            yield return null;
        }
        isEndWord = true;
        yield break;
    }
}
