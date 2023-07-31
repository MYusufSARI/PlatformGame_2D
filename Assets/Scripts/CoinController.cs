using UnityEngine;
using UnityEngine.UI;

public class CoinController : MonoBehaviour
{
    [SerializeField] Text scoreValueText;
    [SerializeField] float coinRotateSpeed;
    private void Update()
    {
        transform.Rotate(new Vector3(0f, coinRotateSpeed, 0f));
    }


    
    private void OnTriggerEnter2D(Collider2D collision)
    {
        // Etkileşime giren gameObject'e erişmek için kullanılır.
        if (collision.gameObject.CompareTag("Player"))
        {
            /* Text stringini int değerine çevirme
            int scoreValue = int.Parse(scoreValueText.text);
            scoreValue += 50;
            //İnt değerini text stringine çevirme
            scoreValueText.text = scoreValue.ToString();*/
            GameObject.Find("LevelManager").GetComponent<LevelManager>().AddScore(50);
            Destroy(gameObject);
        }
    }
}
