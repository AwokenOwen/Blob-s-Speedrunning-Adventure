using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FloatingScript : MonoBehaviour
{
    [SerializeField]
    float amount;
    [SerializeField]
    float speed;

    Vector3 startingPos;

    private void Start()
    {
        startingPos = GetComponent<RectTransform>().position;
    }

    void FixedUpdate()
    {
        GetComponent<RectTransform>().position = startingPos + new Vector3(0f, Mathf.Sin(Time.time * speed) * amount, 0f);
    }
}
