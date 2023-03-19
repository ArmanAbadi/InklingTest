using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpinnerGame : MonoBehaviour
{
    public GameObject SpinnerArm;
    float SpinnerSpeedStart = 45f;
    float CummilativeSpeed = 0f;
    float CummilativeSpeedMax = 360f;
    float CummilativeSpeedIncreaseRate = 10f;
    bool GameIsPlaying = false;

    private void Start()
    {
        GameController.instance.gameInitialized += Initialize;

    }
    public void Initialize()
    {
        SpinnerArm.transform.rotation = Quaternion.identity;
        CummilativeSpeed = 0f;
        GameIsPlaying = false;
    }
    // Update is called once per frame
    void Update()
    {
        if (GameController.instance == null) return;
        if (GameController.instance.gameState == GameController.GameState.Paused) return;
        if (GameController.instance.gameState == GameController.GameState.StartMenu) return;

        if ( !GameIsPlaying) {
            GameIsPlaying = true;
            CummilativeSpeed = 0f;
        }
        
        if (CummilativeSpeed > CummilativeSpeedMax) CummilativeSpeed = CummilativeSpeedMax;

        CummilativeSpeed += CummilativeSpeedIncreaseRate * Time.deltaTime;
        SpinnerArm.transform.Rotate(0, ( SpinnerSpeedStart + CummilativeSpeed ) * Time.deltaTime, 0);
    }

    private void OnCollisionEnter(Collision collision)
    {
        GameController.instance.GameOver();
    }
}
