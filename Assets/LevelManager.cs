using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LevelManager : MonoBehaviour
{
    [SerializeField]Text scoreValueText;
   public void NextLevel()
    {
        Time.timeScale = 1;
        //Scenemanager da bir sonraki sahneyi çağırma kodu
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);

    }
    public void Restart()
    {
        Time.timeScale = 1;
        //Scenemanager da mevcut sahneyi çğaırma kodu
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
    public void ClosePanel(string parentName)
    {
        GameObject.Find(parentName).SetActive(false);
    }

    public void AddScore(int score)
    {
        int scoreValue = int.Parse(scoreValueText.text);
        scoreValue += score;
        //İnt değerini text stringine çevirme
        scoreValueText.text = scoreValue.ToString();
    }
}
