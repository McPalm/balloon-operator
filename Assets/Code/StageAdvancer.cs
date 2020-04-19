using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StageAdvancer : MonoBehaviour
{
    public void NextStageWithDelay(float delay) => Invoke("NextStage", delay);

    public void NextStage()
    {
        int index = SceneManager.GetActiveScene().buildIndex;

        if (index == 0 && Unlocks.HighestCleared == 0)
            index++;

        

        index += 1;
        index %= SceneManager.sceneCountInBuildSettings;
        SceneManager.LoadScene(index);
    }

    public void ReplayWithDelay(float delay) => Invoke("Replay", delay);

    public void Replay()
    {
        int index = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(index);
    }
}
