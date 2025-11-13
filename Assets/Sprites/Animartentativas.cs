using UnityEngine;
using UnityEngine.UIElements;

public class UISpriteAnimator : MonoBehaviour
{
    public UIDocument uiDocument;     // The UI Document
    public Sprite[] sprites;          // Sprites (sliced or from a sheet)
    public float framesPerSecond = 10f;

    private VisualElement uiImage;
    private Texture2D[] frameTextures; // Textures for each frame
    private int currentFrame = 0;
    private float timer = 0f;

    void Start()
    {
        var root = uiDocument.rootVisualElement;
        uiImage = root.Q<VisualElement>("P1HearthTest");

        if (uiImage == null)
        {
            Debug.LogError("UI Image not found!");
            return;
        }

        if (sprites.Length == 0)
        {
            Debug.LogError("No sprites assigned!");
            return;
        }

        // Convert all sprites into individual textures
        frameTextures = new Texture2D[sprites.Length];
        for (int i = 0; i < sprites.Length; i++)
        {
            frameTextures[i] = SpriteToTexture(sprites[i]);
        }

        // Set initial frame
        uiImage.style.backgroundImage = new StyleBackground(frameTextures[0]);
    }

    void Update()
    {
        if (frameTextures.Length == 0 || uiImage == null) return;

        timer += Time.deltaTime;
        if (timer >= 1f / framesPerSecond)
        {
            timer -= 1f / framesPerSecond;
            currentFrame = (currentFrame + 1) % frameTextures.Length;
            uiImage.style.backgroundImage = new StyleBackground(frameTextures[currentFrame]);
        }
    }

    /// <summary>
    /// Converts a Sprite (from a sprite sheet or individual) to a Texture2D of just that frame.
    /// </summary>
    private Texture2D SpriteToTexture(Sprite sprite)
    {
        Texture2D tex = new Texture2D((int)sprite.rect.width, (int)sprite.rect.height);
        Color[] pixels = sprite.texture.GetPixels(
            (int)sprite.textureRect.x,
            (int)sprite.textureRect.y,
            (int)sprite.textureRect.width,
            (int)sprite.textureRect.height
        );
        tex.SetPixels(pixels);
        tex.Apply();
        return tex;
    }
}