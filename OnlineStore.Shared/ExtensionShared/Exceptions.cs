namespace OnlineStore.Shared.ExtensionShared
{
    public class GameKeyNotUniqueException : Exception
    {
        public GameKeyNotUniqueException(string gameKey)
            : base($"Game key '{gameKey}' is not unique.")
        {
        }
    }

    public class PlatformNotFoundException : Exception
    {
        public PlatformNotFoundException(Guid platformId)
            : base($"Platform with ID '{platformId}' is not found.")
        {
        }
    }

    public class GenreNotFoundException : Exception
    {
        public GenreNotFoundException(Guid genreId)
            : base($"Genre with ID '{genreId}' is not found.")
        {
        }
    }

    public class GameIdNotFoundException : Exception
    {
        public GameIdNotFoundException(Guid gameId)
            : base($"Game with ID '{gameId}' is not found.")
        {
        }
    }

    public class GameKeyNotFoundException : Exception
    {
        public GameKeyNotFoundException(string gameKey)
            : base($"Game with Key '{gameKey}' is not found.")
        {
        }
    }

    public class GameNoNameException : Exception
    {
        public GameNoNameException()
            : base("No name")
        {
        }
    }
}
