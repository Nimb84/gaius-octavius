namespace GO.Service.Users.Enums
{
    [Flags]
    public enum Scopes : ushort
    {
        None = 1 << 0,
        Movies = 1 << 1,
        Support = 1 << 2
    }
}
