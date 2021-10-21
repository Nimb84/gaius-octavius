namespace GO.Service.Users.Enums
{
    [Flags]
    public enum Roles : byte
    {
        User = 1 << 0,
        Administration = 1 << 1
    }
}
