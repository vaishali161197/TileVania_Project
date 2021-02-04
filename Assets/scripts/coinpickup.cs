using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class coinpickup : MonoBehaviour
{
    [SerializeField] AudioClip coinpickupSFX;
    [SerializeField] int pointsForCoinsPickUps = 100;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        AudioSource.PlayClipAtPoint(coinpickupSFX, Camera.main.transform.position);
        FindObjectOfType<GameSession>().AddToScore(pointsForCoinsPickUps);
        Destroy(gameObject);
    }
}
