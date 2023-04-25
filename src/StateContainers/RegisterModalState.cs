namespace blazor_tailwind_airbnb.StateContainers;

public readonly record struct IsOpen(bool Value)
{
    public static IsOpen True => new(true);
    public static IsOpen False => new(false);
};

public class RegisterModalState : StateContainer<IsOpen> {}