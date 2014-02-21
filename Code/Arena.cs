using UnityEngine;
//using System.Collections;

public class Arena : MonoBehaviour {
    void Update()
    {
        transform.Rotate(0, 5, 0);
        Debug.DrawLine(Vector3.zero, new Vector3(10, 10, 0), Color.red);
        Debug.Log("toto!");
    }
}