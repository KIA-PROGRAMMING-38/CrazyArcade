using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class test : MonoBehaviour
{
    // S1tart is called before the first frame update
    Animator anim;
    private void Awake()
    {
        anim = GetComponent<Animator>();
        anim.SetInteger("Type", 1);
    }
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
