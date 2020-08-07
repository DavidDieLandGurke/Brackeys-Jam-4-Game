using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class FinishLevel : MonoBehaviour
{
    public GameObject warningText;
    public string nextSceneName;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(FindObjectOfType<Enemies>() == null)
        {
            SceneManager.LoadScene(nextSceneName);
        }
        else
        {
            warningText.SetActive(true);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        warningText.SetActive(false);
    }
}
