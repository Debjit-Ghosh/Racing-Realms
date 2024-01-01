using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Car : MonoBehaviour
{
    [SerializeField] float speed, speedGain;

    [SerializeField] int turnSpeed, steerValue;
    //bool start;
    public GameObject youDied;
    // Start is called before the first frame update
    void Start()
    {
        youDied.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
         Move();     
    }
    void Move()
    {
        speed += speedGain * Time.deltaTime;

        transform.Rotate(0f, steerValue * turnSpeed * Time.deltaTime, 0f);

        transform.Translate(Vector3.forward * speed * Time.deltaTime);
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("obstacle")) 
        {
            SceneManager.LoadScene(0);
        }
    }
    public void Steer(int value)
    { steerValue = value; }

    public void OnApplicationQuit()
    {
        Application.Quit();
    }

    public void Restart()
    {
        SceneManager.LoadScene(1);
    }
}
