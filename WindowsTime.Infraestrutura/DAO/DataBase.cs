namespace WindowsTime.Infraestrutura.DAO
{
    public static class DataBase
    {
        public static ISessionFactoryHolder SessionFactoryHolder { get; private set; }

        public static void Inicializar<T>(ISessionFactoryHolder sessionFactoryHolder)
        {
            sessionFactoryHolder.InitializeSessionFactory<T>();

            SessionFactoryHolder = sessionFactoryHolder;
        }
    }
}