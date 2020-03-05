using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Seeker : MonoBehaviour
{
    [SerializeField]
    private Transform carrying;
    public bool wantToCarry = false;
    public bool isCarrying = false;

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("Triggering...");
        if(!isCarrying && wantToCarry && other.CompareTag("Carriable"))
        {
            Carry(other.transform);
        }
    }

    public void Carry(Transform carriable)
    {
        Debug.Log("Carrying: " + carriable.name);
        isCarrying = true;
        carrying = carriable;
        carrying.position = new Vector3(0, 0, 0);
        carrying.transform.SetParent(transform);
        carrying.localPosition = new Vector3(0, 0, 0);
    }

    private void Update()
    {
        if (carrying == null)
            return;

        if (!wantToCarry)
        {
            isCarrying = false;
            carrying.transform.SetParent(null);
            carrying = null;
        }
    }
}
