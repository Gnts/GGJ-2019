using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelTransition : MonoBehaviour
{
    public float transitionTime = 5f;

    public void TransitionToLevel()
    {
        SceneManager.LoadScene($"Home_{Random.Range(1, 2)}");
    }

    public void TransitionToMenu()
    {
        SceneManager.LoadScene("Menu");
    }
}
