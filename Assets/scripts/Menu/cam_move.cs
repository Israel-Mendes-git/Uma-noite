using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cam_move : MonoBehaviour
{
    public float speed = 1f;

    void Update()
    {
        transform.position += Vector3.right * speed * Time.deltaTime;
    }
}