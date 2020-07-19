using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimalFSM : MonoBehaviour
{
    //Animal상태
    enum AnimalState
    {
        Default, Run, Attack, Down
    }

    AnimalState anis = AnimalState.Default;

    public int hp = 100;
    public float speed = 1.0f;
    public float attackTime = 1.5f;
    private float curTime = 0.0f;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
