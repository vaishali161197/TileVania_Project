using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VerticaScrolll : MonoBehaviour
{
    [Tooltip("Game Units Per Second")]
    [SerializeField] float ScrollRate = 0.2f;

    // Update is called once per frame
    void Update()
    {
        float yMove = ScrollRate * Time.deltaTime;
        transform.Translate(new Vector2(0f, yMove));
    }
}
