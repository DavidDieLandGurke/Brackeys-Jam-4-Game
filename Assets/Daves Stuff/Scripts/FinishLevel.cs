using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class FinishLevel : MonoBehaviour
{
    public GameObject warningText;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(FindObjectOfType<Enemies>() == null)
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
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
