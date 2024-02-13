using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coin : MonoBehaviour
{
    public int count;
    public AudioClip audioClipCoin;

    private AudioSource audioSourceCoin;
    private GameObject player;

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        audioSourceCoin = player.GetComponent<AudioSource>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            collision.GetComponent<Player>().AddCoin(count);
            audioSourceCoin.PlayOneShot(audioClipCoin);
            Destroy(gameObject);
        }
    }
    
    
}
