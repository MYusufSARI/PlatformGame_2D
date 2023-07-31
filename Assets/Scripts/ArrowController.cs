using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ArrowController : MonoBehaviour
{
    [SerializeField] GameObject effect;
    [SerializeField] Text scoreValueText;

    private void Start()
    {
        scoreValueText = GameObject.Find("Score Value").GetComponent<Text>();
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!(collision.gameObject.tag == "Player"))
        {
            //Bu kontrollera sahip obje oyun esnasında yok olacaktır.
            Destroy(gameObject);
            

            if (collision.gameObject.CompareTag("Enemy"))
            {
                //Oka verilen particle effectin poziyonunu kullanmak için kullanılır.
                Instantiate(effect, collision.gameObject.transform.position, Quaternion.identity);
                GameObject.Find("LevelManager").GetComponent<LevelManager>().AddScore(100);
                Destroy(collision.gameObject);
            }
        }
        //Ok kullanıldıktan sonra yok etme kodu.
        if (gameObject.name == "Arrow(Clone)")
        {
            Destroy(gameObject, 2);
        }


    }
}
