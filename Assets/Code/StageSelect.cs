using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class StageSelect : MonoBehaviour
{
    Button[] stageButtons;

    private void Start()
    {
        stageButtons = GetComponentsInChildren<Button>();
        

        var highestClear = Unlocks.HighestCleared;

        
        for(int i = 0; i < stageButtons.Length; i++)
        {
            int capture = i;
            if (highestClear >= i)
                stageButtons[i].onClick.AddListener(() => GotoStage(capture));
            else
                stageButtons[i].interactable = false;
        }
    }

    void GotoStage(int stageNumber)
    {
        SceneManager.LoadScene(stageNumber + 2);
    }
}
