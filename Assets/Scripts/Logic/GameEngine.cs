using UnityEngine;
using System.Collections;

public class GameEngine : MonoBehaviour
{

    private void Awake()
    {

    }

    private void Start()
    {

    }

    private void Update()
    {
        //驱动所有的计时器
        TimeManager.instance.Loop(Time.deltaTime);
        
    }

}
