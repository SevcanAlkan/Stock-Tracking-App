namespace STA.Core.Enum
{
    public enum UserStatus : byte
    {
        Online = 1,
        Idle = 2,
        DontDisturb = 3,
        Invisible = 4,
        Offline = 5
    }

    public enum HTTPMethodType : byte
    {
        Unknown = 1,
        GET = 2,
        POST = 3,
        PUT = 4,
        DELETE = 5
    }

    public enum AdStatus : byte
    {
        NotStarted = 0,
        Started = 1,
        Done = 2,
        Cancelled = 3
    }

    public enum Gender : byte
    {
        Unknown = 0,
        Male = 1,
        Female = 2
    }

    public enum OrderStatus : byte
    {
        Created = 0,
        WatingShipment = 1,
        WaitingDelivery = 2,
        Completed = 3,
        Cancelled = 4,
        OnChange = 5
    }

    public enum RefTable : byte
    {
        Customer = 0,
        Model = 1,
        Brand = 2,
        Order = 3,
        Infuluencer = 4,
        AdCampain = 5
    }
}
