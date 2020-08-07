using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TutorialManager : MonoBehaviour
{
    public GameObject[] texts;
    int textsIndex;

    void Update()
    {
        for (int i = 0; i < texts.Length; i++)
        {
            texts[i].SetActive(i == textsIndex);
        }



        //if (Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.Space)
        //        || Input.GetKeyDown(KeyCode.S) || Input.GetKeyDown(KeyCode.D) ||
        //        Input.GetKeyDown(KeyCode.UpArrow) || Input.GetKeyDown(KeyCode.DownArrow)
        //        || Input.GetKeyDown(KeyCode.LeftArrow) || Input.GetKeyDown(KeyCode.RightArrow))
        //{

        //      if (textsIndex == 0)
        //    {
        //        textsIndex++;
        //    }
        //}
        //else if (Input.GetMouseButtonDown(0) || Input.GetKeyDown(KeyCode.Space))
        //{
        //    if (textsIndex == 1)
        //                textsIndex++;
        //}
        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (textsIndex < 3)
                textsIndex++;
            if (textsIndex == 4)
            {
                textsIndex++;
                SceneManager.LoadScene(sceneBuildIndex: 0);
            }
        }
        else if ((Input.GetMouseButtonDown(0)))
        {
            if (textsIndex == 1)
                textsIndex++;
        }
    }

    //void OnCollisionEnter2D(Collision2D collision)
    //{
    //    if (textsIndex == 3 && collision.gameObject.CompareTag("Finish"))
    //    {
    //        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    //    }
    //}
}
