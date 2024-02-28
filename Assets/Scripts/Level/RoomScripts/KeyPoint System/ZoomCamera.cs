using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZoomCamera : AfterEffect
{
    CinemachineVirtualCamera camera;

    [SerializeField] float newOrthoSize = 2f;
    [SerializeField] float zoomTimeSeconds = 1f;

    private float normalOrthoSize;

    private void Start()
    {
        camera = GameObject.FindGameObjectsWithTag("Camera")[0].GetComponent<CinemachineVirtualCamera>();
    }

    public override void ApplyEffect()
    {
        normalOrthoSize = camera.m_Lens.OrthographicSize;

        StartCoroutine(Zoom());
    }

    IEnumerator Zoom()
    {
        float elapsedTime = 0f;

        float t = 0f;

        while (elapsedTime < zoomTimeSeconds)
        {
            elapsedTime += Time.deltaTime;
            t = Mathf.Lerp(normalOrthoSize, newOrthoSize, elapsedTime / zoomTimeSeconds);
            camera.m_Lens.OrthographicSize = t;
            yield return null;
        }
    }
}
