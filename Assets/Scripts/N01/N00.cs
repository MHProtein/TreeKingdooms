using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class N00 : MonoBehaviour
{
  [Header("移动速度")]
    public float speed;
    Animator animator;
    Vector3 movement;

    [SerializeField] private TheSceneManager sceneManager;
    
    void Start()
    {
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        if(TheSceneManager.talking)
            return;
        if (!sceneManager.movable)
            return;
        //移动
        movement = new Vector3(Input.GetAxisRaw("Horizontal") * Time.deltaTime * speed, 
            0.0f, transform.position.z);
        transform.Translate(movement);

        //翻面
        if(movement.x>0){
            transform.localScale = new Vector3(1, 1, 1);
        }
        if(movement.x<0){
            transform.localScale = new Vector3(-1, 1, 1);
        }
    }
}

