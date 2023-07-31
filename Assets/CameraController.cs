using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField] float minX, maxX;
    Transform playerTransform;
    void Start()
    {
        //Playerin transform değerini bulma
        playerTransform = GameObject.Find("Player").transform;
    }

    // Update is called once per frame
    void Update()
    {

        transform.position = new Vector3(Mathf.Clamp(playerTransform.position.x,minX,maxX), transform.position.y, transform.position.z);
    }
}
