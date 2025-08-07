using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerUI : MonoBehaviour
{
    public Sprite emptyHeart;
    public Sprite fullHeart;
    public List<Image> hearts;

    public Player player;

    void Start()
    {

    }

    void UpdateHeart()
    {
        for (int i = 0; i < hearts.Count; i++)
        {
            if (i < player.health)
            {
                hearts[i].sprite = fullHeart;
            }
            else
            {
                hearts[i].sprite = emptyHeart;
            }
        }
    }

    private void OnEnable()
    {
        Player.onPlayerHit += UpdateHeart;
    }

    private void OnDisable()
    {
        Player.onPlayerHit -= UpdateHeart;
    }
}
