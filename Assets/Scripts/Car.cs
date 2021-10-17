using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Car : MonoBehaviour
{
    [SerializeField] float speed = 10.0f;
    [SerializeField] float speedGainEverySecond = 0.3f;
    [SerializeField] float turnSpeed = 200.0f;

    private int steerValue;//In canvas, affects the rotation of car
 
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //speed is increasing every second, and we arrange to every frame with Time.deltatime 
        speed += speedGainEverySecond * Time.deltaTime;

        transform.Rotate(0f, turnSpeed * steerValue * Time.deltaTime, 0f);
        transform.Translate(Vector3.forward * speed * Time.deltaTime);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Obstacle"))
        {
            SceneManager.LoadScene(0);
        }
    }

    public void Steer(int value)
    {
        steerValue = value;
    }
}
