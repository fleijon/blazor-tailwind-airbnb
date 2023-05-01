namespace blazor_tailwind_airbnb.StateContainers;

public static class StateContainerExtensions
{
    public static void Open(this StateContainer<IsOpen>  target)
    {
        target.Property = IsOpen.True;
    }

    public static void Close(this StateContainer<IsOpen> target)
    {
        target.Property = IsOpen.False;
    }
}
