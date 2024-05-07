using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using TikTokLiveSharp.Events.Objects; // Make sure to import the namespace where Picture class resides

public class DisplayPicture : MonoBehaviour
{
    public static DisplayPicture Insatance {  get; private set; }

    private void Awake()
    {
        Insatance = this;
    }

    public Image imageComponent;
    public Picture pictureData;

    // Method to load the image from URL and assign it to the Image component
    public IEnumerator LoadImageFromUrl(string url)
    {
        using (WWW www = new WWW(url))
        {
            yield return www;

            if (string.IsNullOrEmpty(www.error))
            {
                // Create a sprite from the downloaded texture
                Sprite sprite = Sprite.Create(www.texture, new Rect(0, 0, www.texture.width, www.texture.height), Vector2.zero);

                // Assign the sprite to the Image component
                imageComponent.sprite = sprite;
            }
            else
            {
                Debug.LogError("Error downloading image: " + www.error);
            }
        }
    }

    // Method to set the Picture data and load the image
    public void SetPictureData(Picture picture)
    {
        pictureData = picture;

        // Choose one of the URLs from the Picture's URL list
        string imageUrl = picture.Urls[0];

        // Start the coroutine to load the image
        StartCoroutine(LoadImageFromUrl(imageUrl));
    }
}
