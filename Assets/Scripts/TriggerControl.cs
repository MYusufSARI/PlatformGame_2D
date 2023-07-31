using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerControl : MonoBehaviour
{
    [SerializeField] GameObject player;

    //Trigerra girdiğinde işlem yapmak için kullandığımız metod. Çarpışma durumlarında kullanılan metoddur.
    private void OnTriggerEnter2D(Collider2D collision)
    {
        //Player objesinin içindeki player controller scriptinde bulunanan özelliği true yapma kodu.
        player.GetComponent<PlayerController>().onGround = true;
    }

    //Triggerdan çıktığında işlem yapmak için kullandığımız metod.
    private void OnTriggerExit2D(Collider2D collision)
    {
        // //Player objesinin içindeki player controller scriptinde bulunanan özelliği false yapma kodu.
        player.GetComponent<PlayerController>().onGround = false;
    }
}
