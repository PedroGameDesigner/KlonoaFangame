using Colliders;
using PlatformerRails;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestMover : MonoBehaviour
{
    public float speed = 5;

    private IMover mover;

    // Start is called before the first frame update
    void Start()
    {
        mover = GetComponent<IMover>();
    }

    // Update is called once per frame
    void Update()
    {
        mover.Velocity = Vector3.forward * speed + Vector3.down * 1 * Time.deltaTime;
    }
}
