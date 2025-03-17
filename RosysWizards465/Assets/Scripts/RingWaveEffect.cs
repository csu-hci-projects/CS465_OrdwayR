using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RingWaveEffect : MonoBehaviour
{
    public int points = 100;
    public float radius = 5f;
    public float waveAmplitude = 0.5f;
    public float waveFrequency = 2f;
    public float waveSpeed = 2f;
    private LineRenderer lineRenderer;

    void Start()
    {
        lineRenderer = GetComponent<LineRenderer>();
        lineRenderer.positionCount = points;
    }

    void Update()
    {
        for (int i = 0; i < points; i++)
        {
            float angle = i * Mathf.PI * 2 / points;
            float yOffset = Mathf.Sin(Time.time * waveSpeed + i * waveFrequency) * waveAmplitude;
            Vector3 pos = new Vector3(Mathf.Cos(angle) * radius, yOffset, Mathf.Sin(angle) * radius);
            lineRenderer.SetPosition(i, pos);
        }
    }
}