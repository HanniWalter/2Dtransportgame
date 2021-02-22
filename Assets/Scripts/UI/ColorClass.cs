
public class ColorClass
{
    static public UnityEngine.Color  getBackground()
    {
        return new UnityEngine.Color(0x53 / 255f, 0x5c / 255f, 0x68 / 255f);
    }

    static public UnityEngine.Color getHighlighted()
    {
        return new UnityEngine.Color(0xf6 / 255f, 0xe5 / 255f, 0x8d / 255f);
    }

    static public UnityEngine.Color getSelected()
    {
        return new UnityEngine.Color(0xba / 255f, 0xdc / 255f, 0x58 / 255f);
    }

    static public UnityEngine.Color getUnselected()
    {
        return new UnityEngine.Color(0x95/255f,0xaf / 255f, 0xc0 / 255f);
    }

    static public UnityEngine.Color getOutline()
    {
        return new UnityEngine.Color(0xf9 / 255f, 0xca / 255f, 0x24 / 255f);
    }
}
