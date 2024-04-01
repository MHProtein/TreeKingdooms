using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Scene2_5Manager : TheSceneManager
{
    // Start is called before the first frame update
    void Start()
    {
        this.movable = true;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            this.LoadScene3();
        }
    }

    void LoadScene3()
    {
        SceneManager.LoadScene("Scene3");
    }

}
