using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    [SerializeField] bool onGround;
    private float width;
    private Rigidbody2D mybody;
    [SerializeField] LayerMask engel;
    [SerializeField] float speed;
    
    
    // static eklmemeizin sebebi diğer tanımlamalar objeye ait olan tanımlamalardır static ise public class sınıfına aittir.
    // private static int totalEnemyNumber = 0;

    void Start()
    {
        /*Düşman sayısını arttırma kodu.
        totalEnemyNumber++;
        Debug.Log("Düşman " + gameObject.name + "oluştu." + "Oyundaki toplam düşman sayısı: " + totalEnemyNumber);*/

        //Sprite renderer componentinin enini yani genişliğini öğrenmek için kullanılan kod.
        width = GetComponent<SpriteRenderer>().bounds.extents.x;

        mybody = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        //Enemy objesinin engelin üzerinde yapacağı sağa ve sola yürüyüşleri kontrol etmek için kullanılır.
        RaycastHit2D hit = Physics2D.Raycast(transform.position + (transform.right * width / 2), Vector2.down, 2f, engel);


        //Enemy objesinin zemin üzerindeki kontrolünü sağlamak için kullanılır.
        if (hit.collider != null)
        {
            onGround = true;
        }
        else
        {
            onGround = false;
        }
        //Enemy objesi ground yani engel üzerinde değilse 180 derece döndür anlamına gelir.
        if (!onGround)
        {
            transform.eulerAngles += new Vector3(0, 180f, 0);
        }

        //Enemy objesinin sağa ve sola kendi kendine gitmesi için kullanılan kod.
        mybody.velocity = new Vector2(transform.right.x * speed, 0f);
    }

    

    // RaycastHit2D structından çıkacak ışınları sceeen ekranında görebilmek için kullanılan metod. Test amaçlı kullanılır.
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        // Raycastten çıkacak ışının hızı ve pozisyonunu tanımlarız.
        Gizmos.DrawLine(transform.position + (transform.right * width / 2), transform.position + (transform.right * width / 2) + new Vector3(0, -2f, 0));

    }

}
