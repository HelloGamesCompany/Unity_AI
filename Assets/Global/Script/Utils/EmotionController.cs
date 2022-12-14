using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EmotionController : MonoBehaviour
{
    public List<Sprite> sprites;

    private SpriteRenderer spRenderer = null;

    // Start is called before the first frame update
    void Start()
    {
        spRenderer = GetComponent<SpriteRenderer>();
    }

    public void ChangeSprite(int index)
    {
        CancelInvoke();
        if (index>=0 && index < sprites.Count)
        {
            spRenderer.sprite = sprites[index];
        }
        else
        {
            spRenderer.sprite = null;
        }
    }

    public void ChangeSprite(int index, float time)
    {
        if (index >= 0 && index < sprites.Count)
        {
            Invoke("SetSpriteNull", time);
            spRenderer.sprite = sprites[index];
        }
        else
        {
            spRenderer.sprite = null;
        }
    }

    private void SetSpriteNull()
    {
        spRenderer.sprite = null;
    }
}
