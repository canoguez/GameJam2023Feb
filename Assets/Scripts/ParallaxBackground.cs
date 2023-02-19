using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParallaxBackground : MonoBehaviour
{
    public float[] layerSpeeds; // The speed of each layer, with index 0 being the slowest

    public Transform[] layers; // The transforms of each layer
    private float[] layerStartPos; // The starting position of each layer
    private Vector3 velocity = Vector3.zero;

    private void Start()
    {
        // Get the transforms and starting positions of each layer
        layers = new Transform[transform.childCount];
        layerStartPos = new float[layers.Length];

        for (int i = 0; i < layers.Length; i++)
        {
            layers[i] = transform.GetChild(i);
            layerStartPos[i] = layers[i].position.x;
        }
    }

    private void LateUpdate()
    {
        for (int i = 0; i < layers.Length; i++)
        {
            float layerSpeed = layerSpeeds[i];
            float layerOffset = (Time.deltaTime * layerSpeed);
            Vector3 targetpos = new Vector3(layerOffset, 0, 0);
            targetpos += layers[i].localPosition;
            layers[i].localPosition = targetpos;
        }
    }
}
