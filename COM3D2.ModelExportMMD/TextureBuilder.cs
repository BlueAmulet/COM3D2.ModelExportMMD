using System;
using System.IO;
using UnityEngine;

namespace COM3D2.ModelExportMMD
{
    public static class TextureBuilder
    {
        #region Methods

        public static Texture2D ConvertToTexture2D(RenderTexture renderTexture)
        {
            int width = renderTexture.width;
            int height = renderTexture.height;
            Texture2D texture2D = new Texture2D(width, height, TextureFormat.ARGB32, false);
            RenderTexture.active = renderTexture;
            texture2D.ReadPixels(new Rect(0.0f, 0.0f, width, height), 0, 0);
            texture2D.Apply();
            return texture2D;
        }

        public static void WriteTextureToFile(string path, Texture tex)
        {
            try
            {
                Texture2D texture2D = ((!(tex is RenderTexture)) ? (tex as Texture2D) : ConvertToTexture2D(tex as RenderTexture));
                Texture2D argb32Texture2D = new Texture2D(texture2D.width, texture2D.height, TextureFormat.ARGB32, false);
                Color[] pixels = texture2D.GetPixels();
                argb32Texture2D.SetPixels(pixels);
                byte[] bytes = argb32Texture2D.EncodeToPNG();
                File.WriteAllBytes(path, bytes);
                Debug.Log("Writing Texture :" + path);
            }
            catch (Exception error)
            {
                Debug.Log($"Error writing texture to file: {error.Message}\n\nStack trace:\n{error.StackTrace}");
            }
        }

        #endregion
    }
}
