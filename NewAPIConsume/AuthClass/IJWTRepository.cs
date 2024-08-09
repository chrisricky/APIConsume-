namespace NewAPIConsume.AuthClass
{
    public interface IJWTRepository
    {
        Tokens Authenticate(Users users);
    }
}
