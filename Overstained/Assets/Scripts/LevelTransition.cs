using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelTransition : MonoBehaviour
{
    public float transitionTime = 5f;

    void Start()
    {
        UI.instance.transitionTimer.gameObject.SetActive(true);
    }

    void Update()
    {
        transitionTime -= Time.deltaTime;

        UI.instance.transitionTimer.text = "Next level in: " + transitionTime.ToString("0");

        if(transitionTime < 0)
        {
            TransitionToLevel();
        }
    }

    public void TransitionToLevel()
    {
        // TODO CHANGE TO NEXT LEVEL MATT

        // Example test :D this will just load the next scene under build settings
        // SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex+1);
        UI.instance.transitionTimer.text = "WE SHOULD CHANGE LEVEL. MATT HAS NOT IMPLEMENTED THIS YET!";
    }
}
