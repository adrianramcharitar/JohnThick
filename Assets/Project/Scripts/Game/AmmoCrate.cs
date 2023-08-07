using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AmmoCrate : MonoBehaviour
{

    [Header("Visuals")]
    public GameObject container;
    public float rotationSpeed = 180f;
    [Header("Gameplay")]
    public int ammo = 12;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        container.transform.Rotate(Vector3.up * rotationSpeed * Time.deltaTime);
    }
}
