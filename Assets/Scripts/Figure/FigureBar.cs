using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FigureBar : MonoBehaviour
{
    [SerializeField] GameObject _figureImagePrefab;

    public void FillFigureIntoBar(Sprite[] sprite)
    {
        for (int i = 0; i < sprite.Length; i++)
        {
            GameObject obj = Instantiate(_figureImagePrefab, transform.position, Quaternion.identity);
            Image image = obj.GetComponent<Image>();
            image.sprite = sprite[i];
            image.SetNativeSize();
            obj.transform.SetParent(transform);
        }
    }
}
