using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AlternateParent : MonoBehaviour
{
    public GameObject parentObj;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        transform.position = parentObj.transform.position;
    }
}
