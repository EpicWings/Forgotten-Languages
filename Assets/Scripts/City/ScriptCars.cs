using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScriptCars : MonoBehaviour
{
    [SerializeField]
    private float _speed;
    [SerializeField]
    private string _direction;

    private IEnumerator moveRoutine;

    private Vector3 _initialPosition;

    // Start is called before the first frame update
    void Start()
    {
        _initialPosition = transform.position;
        moveRoutine = MoveCar();

        // Generate a random number between 0 and 5
        float randomWait = Random.Range(0f, 5f);
        
        StartCoroutine(WaitAndStartMoveCar(randomWait));
    }

    private IEnumerator WaitAndStartMoveCar(float waitTime)
    {
        yield return new WaitForSeconds(waitTime);
        StartCoroutine(moveRoutine);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private IEnumerator MoveCar()
    {
        while (true)
        {
            if (_direction == "right")
            {
                transform.position += new Vector3(0.5f, 0, 0);
                yield return new WaitForSeconds(_speed);
                if (transform.position.x > 7)
                {
                    transform.position = _initialPosition;
                }
            }
            else if (_direction == "left")
            {
                    transform.position += new Vector3(-0.5f, 0, 0);
                    yield return new WaitForSeconds(_speed);
                    if (transform.position.x < -18)
                    {
                        transform.position = _initialPosition;
                    }

            }
        }
    }
}
