using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class MoveCameraToObject : AfterEffect
{
    CinemachineVirtualCamera camera;
    PlayerController player;

    [SerializeField] bool doComeBackToPlayer = true;

    [SerializeField] GameObject target;
    [SerializeField] float targetTimeSeconds = 4f;

    private void Start()
    {
        camera = GameObject.FindGameObjectsWithTag("Camera")[0].GetComponent<CinemachineVirtualCamera>();
        player = GameObject.FindGameObjectsWithTag("Player")[0].GetComponent<PlayerController>();
    }

    public override void ApplyEffect()
    {
        StartCoroutine(ControlCameraCoroutine());
    }

    IEnumerator ControlCameraCoroutine()
    {
        camera.Follow = target.transform;

        if (doComeBackToPlayer)
        {
            yield return new WaitForSeconds(targetTimeSeconds);
            camera.Follow = player.transform;
        }
    }
}
