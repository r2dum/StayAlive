using UnityEngine.UI;

public static class AlphaExtension
{
    public static T ChangeButtonAlpha<T>(this T button, float alphaValue) where T : Button
    {
        button.TryGetComponent(out Image image);
        var tempColor = image.color;
        tempColor.a = alphaValue;
        image.color = tempColor;
        return button;
    }
}
