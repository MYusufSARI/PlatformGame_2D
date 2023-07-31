using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class PlayerController : MonoBehaviour
{
    // Unity üzerindeki scripte değer atamak için kullanılır.(Program üzerinde). Publicden farkı private olmasıdır.
    [SerializeField] float speed;
    [SerializeField] float jumpPower;
    [SerializeField] GameObject arrow;
    [SerializeField] bool attacked;
    //Player'ın attığı oka zaman belirleme
    [SerializeField] float currentAttackTimer;
    [SerializeField] float defaultAttacktimer;
    [SerializeField] int arrowNumber;
    [SerializeField] Text arrowNumberText;
    [SerializeField] GameObject winPanel, losePanel;
    private float mySpeedx;
    private Animator myAnimator;


    // Player'ın zeminin üzerinde olup olmadığını kontrol ettiğimiz kod.
    public bool onGround;
    private bool doubleJump;
    // private bir rigidbody2D oluşturmamızın sebebi sürekli harddiskten rigidbody2d çağırıp istemciye yük olmamaktır.
    private Rigidbody2D myBody;

    private Vector3 defaultLocalScale;

    void Start()
    {
        attacked = false;
        myBody = GetComponent<Rigidbody2D>();
        myAnimator = gameObject.GetComponent<Animator>();
        // Bu kodun amacı oyun içinde yapılan transform değişikliklerinde sürekli kod tarafına gelip değer revizasyonu yapmamaktır.
        defaultLocalScale = transform.localScale;
        arrowNumberText.text = arrowNumber.ToString();
    }


    void Update()
    {
        // -1 ile 1 arasında sağ veya sol yön tuşuna basılması süresine göre değerler gelicek. Oyuncunun sağa ve sola gitmesi içiçn yazılan kod.
        // Input. ile başlayan kod classı tuşların alınmasını sağlayan classdır.
        mySpeedx = Input.GetAxis("Horizontal");

        // Animatorda parametre kısmında belirtilen speed değerine ulaşmak için bu kodu kullanıyoruz. Mathf.abs() ise değerin mutlak değerini almak için kullanılır. -1-1.
        // SetFloat dememizin sebebi animatorda bulunan speed parametresinin float bir değişken olmasıdır. Koşma animasyonu oluşturmak için kullanılır.
        myAnimator.SetFloat("Speed", Mathf.Abs(mySpeedx));



        // İstediğimiz componentin özelliklerine erişmek kullandığımız kod.
        //GetComponent<Rigidbody2D>().velocity = new Vector2(mySpeedx * speed, GetComponent<Rigidbody2D>().velocity.y);

        myBody.velocity = new Vector2(mySpeedx * speed, myBody.velocity.y);

        #region Playerin sağ ve sol yönüne göre yüzünün değişmesi
        // Karakter yön tuşlarına bağlı olarak gittiği yöne doğru vücudunu döndürmek için kullanılan kod.
        if (mySpeedx > 0)
        {
            transform.localScale = new Vector3(defaultLocalScale.x, defaultLocalScale.y, defaultLocalScale.z);
        }

        else if (mySpeedx < 0)
        {
            transform.localScale = new Vector3(-defaultLocalScale.x, defaultLocalScale.y, defaultLocalScale.z);
        }
        #endregion


        #region Playerin zıplamasının kontrol edilmesi
        // Karakterin zıplamasını sağlayan kod.
        if (Input.GetKeyDown(KeyCode.Space))
        {

            if (onGround == true)
            {
                myBody.velocity = new Vector2(myBody.velocity.x, jumpPower);
                doubleJump = true;
                //Zıplama animasyonu
                myAnimator.SetTrigger("Jump");
            }



            //Çift zıplama özelliğini sağlama kodu.
            else
            {
                if (doubleJump == true)
                {
                    myBody.velocity = new Vector2(myBody.velocity.x, jumpPower);
                    doubleJump = false;
                }
            }
        }
        #endregion


        #region Playerin ok atmasının kontrolu

        //Tek tek mouse tıklaması ve ok oluştur.
        if (Input.GetMouseButtonDown(0)&&arrowNumber>0)
        {
            if (attacked == false)
            {
                attacked = true;

                //Attack animasyonunun kodu
                myAnimator.SetTrigger("Attack");

                //Fonksiyonun çalışma süresini erteleme kodu
                Invoke("Fire", 0.5f);
            }
        }



        if (attacked == true)
        {
            currentAttackTimer -= Time.deltaTime;

        }

        else
        {
            currentAttackTimer = defaultAttacktimer;
        }

        if (currentAttackTimer <= 0)
        {
            attacked = false;
        }
        #endregion

    }
    // Oluşturulan kodların void ile fonksiyon haline getirerek kodların daha okunabilir ve anlaşılabilir hale gelmesini sağlıyoruz.
    void Fire()
    {
        //Oluşturulan okların playerin pozisyonuna göre konumlanması için kullanılır.
        GameObject okumuz = Instantiate(arrow, transform.position, Quaternion.identity);

        //Playerin vücudunun yönüne göre okun çıkması kodu.
        if (transform.localScale.x > 0)
        {
            okumuz.GetComponent<Rigidbody2D>().velocity = new Vector2(5f, 0f);
        }

        else
        {   //Okun scale x görünümünü tersine çevirmek için kullanılan kod.
            Vector3 okumuzScale = okumuz.transform.localScale;
            okumuz.transform.localScale = new Vector3(-okumuzScale.x, okumuzScale.y, okumuzScale.z);

            okumuz.GetComponent<Rigidbody2D>().velocity = new Vector2(-5f, 0f);
        }

        arrowNumber--;
        //İnt olan değeri string değeri çevirme
        arrowNumberText.text = arrowNumber.ToString();
    }
    //Çarpışma metodudur.
    private void OnCollisionEnter2D(Collision2D collision)
    {
        //Çarpılan objenin adını console bölümünde gösterme kodudur.
        //Debug.Log(collision.gameObject.name);

        //Enemy olarak taglendirdiğimiz yani tag verdiğimiz objeler ile çarpışma gerçekleşince die animasyonunun gerçekleşmesi kodudur.
        if (collision.gameObject.tag==("Enemy"))
        {
            GetComponent<TimeControl>().enabled = false;
            Die();
        }
        else if (collision.gameObject.tag==("Finish"))
        {
            /* winPanel.active = true;
             Time.timeScale = 0;*/
            Destroy(collision.gameObject);
            StartCoroutine(Wait(true));
        }
    }

    public void Die()
    {
        //Bu metodun anlamı player die animasyonu gerçekleştridiğinde speedini 0 la ve constraintsini dondur.
        myAnimator.SetTrigger("Die");
        myAnimator.SetFloat("Speed",0);

        //Rigidbody compenentinin içerisindeki constraints kısmında değişiklik yapmak için kullanılır.
        myBody.constraints = RigidbodyConstraints2D.FreezeAll;

        //Bu kod üzerine işlem yaptığımız scripti devre dışı bırak komutunu oluşturan koddur.
        enabled = false;

        //Ölüm gerçekleştiğinde unity üzerinde yapılan lose panelin ekrana gelmesi
        // losePanel.SetActive(true);
        
        StartCoroutine(Wait(false));
        
    }

    // Ölüm den sonra win ekranının veya loseun gelmesi için delay koyma işlemi
    IEnumerator Wait(bool win)
    {
        yield return new WaitForSecondsRealtime(2f);
        Time.timeScale = 0;
        if (win==true)
        {
            winPanel.SetActive(true); // winPanel.active = true kodu ile aynıdır.
        }

        else
        {
            losePanel.SetActive(true);
        }
        

    }
}
