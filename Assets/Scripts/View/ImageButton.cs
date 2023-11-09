using System.IO;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ImageButton : Button
{
    public override void OnPointerClick(PointerEventData eventData)
    {
        base.OnPointerClick(eventData);

        //Load image
        //...

        LoadImage();
    }

    private void LoadImage()
    {
		NativeGallery.Permission permission = NativeGallery.RequestPermission(NativeGallery.PermissionType.Read);

		if(permission == NativeGallery.Permission.Granted)
        {
			NativeGallery.GetImageFromGallery((path) =>
			{
				Debug.Log("Image path: " + path);
				if (path != null)
				{
                    // Create Texture from selected image
                    byte[] textureData = File.ReadAllBytes(path);
                    if (textureData.Length <= 0)
					{
						Debug.Log("Couldn't load texture from " + path);
						return;
					}

                    Texture2D Texture = ConvertToMonochromatic(textureData);

                    Sprite newSprite = Sprite.Create(Texture, new Rect(0, 0, Texture.width, Texture.height), new Vector2(0.5f, 0.5f));
                    newSprite.name = Path.GetFileNameWithoutExtension(path); ;

                    Image targetImage = (Image)targetGraphic;
                    targetImage.sprite = newSprite;

                    Text nameText = targetImage.transform.GetChild(1)?.GetComponent<Text>();
                    if (nameText != null)
                    {
                        nameText.text = newSprite.name;
                    }

                }
			}, "Select a PNG image", "image/png");
		}
        else
        {
			Debug.LogError($"Permission Status: {permission}");
		}
	}

    private Texture2D ConvertToMonochromatic(byte[] textureData)
    {
        Texture2D grayscaleTexture = new Texture2D(512, 512, TextureFormat.RGBA32, false);
        grayscaleTexture.LoadImage(textureData);

        Color[] pixels = grayscaleTexture.GetPixels();
        for (int i = 0; i < pixels.Length; i++)
        {
            Color pixel = pixels[i];
            float grayscale = pixel.grayscale;
            pixels[i] = new Color(grayscale, grayscale, grayscale, pixel.a);
        }
        grayscaleTexture.SetPixels(pixels);
        grayscaleTexture.Apply();

        return grayscaleTexture;
    }
}
