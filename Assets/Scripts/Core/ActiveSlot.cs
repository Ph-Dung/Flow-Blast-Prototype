public class ActiveSlot
{
    public Box box;

    public bool IsEmpty => box == null;

    public void Clear()
    {
        box = null;
    }
}