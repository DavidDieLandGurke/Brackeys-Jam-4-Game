using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

public class SecondCutscene : MonoBehaviour
{
    public TextMeshProUGUI text;

    private void Start()
    {
        StartCoroutine(idk());
    }

    IEnumerator idk()
    {
        yield return new WaitForSeconds(4);
        text.text = "He Now Wants To Go Back To Being An Old Man.";
        yield return new WaitForSeconds(4);
        text.text = "He Came To Think Of It, That Everyone Has A Lifetime" +
            "And it's theirs,\n And They Should Be Happy With It.";
        yield return new WaitForSeconds(4);
        text.text = "So Now Get Him Back To Being An Old Man.";
        yield return new WaitForSeconds(4);
        SceneManager.LoadScene("Main 2");
    }
}
