using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BloodControl : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject bloodSelf;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Destroy(bloodSelf, 1f);
    }
}
