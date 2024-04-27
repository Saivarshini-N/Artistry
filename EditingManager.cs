using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;
public class EditingManager : MonoBehaviour
{
    // Start is called before the first frame update
    public Slider transparencySlider;
    public static EditingManager instance;
    public Image targetImage;
    public Slider rotationSlider;
    public Button savebutton;


    private void Awake()
    {
        instance = this;
    }
    // Update is called once per frame
    void Update()
    {
        
    }
    public void SetTransparency()
    {
        if(targetImage == null)
        {
            return;
        }
        Color oldColor = targetImage.color;
        Color newColor = new Color(oldColor.r, oldColor.g, oldColor.b, transparencySlider.value);
        targetImage.color = newColor;
    }
    public void RotateImage()
    {
        if (targetImage == null)
        {
            return;
        }
        RectTransform rect = targetImage.rectTransform;
        rect.eulerAngles = new Vector3(0, 0, rotationSlider.value);
    }
    public void Scale1x()
    {
        SetSizeDelta(100, 100);
        if (targetImage != null)
        {
            float min = 290;
            float max = 816;
            targetImage.GetComponent<ExampleClass>().UpdateYClampValues(min, max);
        }
        if (targetImage != null)
        {
            float min = 709;
            float max = 1746;
            targetImage.GetComponent<ExampleClass>().UpdateXClampValues(min, max);
        }
    }

    public void Scale2x()
    {
        SetSizeDelta(200, 200);
        if (targetImage != null)
        {
            float min = 372;
            float max = 700;
            targetImage.GetComponent<ExampleClass>().UpdateYClampValues(min, max);
        }
        if (targetImage != null)
        {
            float min = 850;
            float max = 1627;
            targetImage.GetComponent<ExampleClass>().UpdateXClampValues(min, max);
        }
    }

    public void Scale3x()
    {
        SetSizeDelta(300, 300);
        if (targetImage != null)
        {
            float min = 490;
            float max = 590;
            targetImage.GetComponent<ExampleClass>().UpdateYClampValues(min, max);
        }
        if (targetImage != null)
        {
            float min = 962;
            float max = 1506;
            targetImage.GetComponent<ExampleClass>().UpdateXClampValues(min, max);
        }
    }

    public void SetSizeDelta(float width, float height)
    {
        if (targetImage == null)
        {
            return;
        }
        float scaledWidth = Mathf.Min(width, 300f);
        float scaledHeight = Mathf.Min(height, 300f);

        targetImage.rectTransform.sizeDelta = new Vector2(scaledWidth, scaledHeight);
        Debug.Log($"Image scaled to {scaledWidth}x{scaledHeight}");
    }

    void Start()
    {
        // Check if the button reference is not null and add a listener
        if (savebutton != null)
        {
            savebutton.onClick.AddListener(SaveImage);
        }
    }
    public void SaveImage()
    {
        if (targetImage == null)
        {
            Debug.LogWarning("Target image is not set.");
            return;
        }
        StartCoroutine(LoadImage());
        // Get the texture from the target image
        // Encode the texture to PNG format
        
        // Specify the file path to save the image
        
    }
    IEnumerator LoadImage()
    {
        yield return new WaitForEndOfFrame();
        // Create a new texture and render the image onto it
        Texture2D tex = GetTextureFromImage(targetImage);
        byte[] textureBytes = ImageConversion.EncodeToPNG(tex);
        string texAsString = Convert.ToBase64String(textureBytes);
        Debug.Log("Converted Texture" + texAsString);
    }
    private Texture2D GetTextureFromImage(Image image)
    {
        RectTransform rectTransform = image.rectTransform;
        Texture2D texture = new Texture2D((int)rectTransform.rect.width, (int)rectTransform.rect.height);
        RenderTexture currentRT = RenderTexture.active;
        RenderTexture renderTexture = new RenderTexture(texture.width, texture.height, 32);
        Graphics.Blit(null, renderTexture);
        RenderTexture.active = renderTexture;
        texture.ReadPixels(rectTransform.rect, 0, 0);
        texture.Apply();
        RenderTexture.active = currentRT;
        return texture;
    }
}
