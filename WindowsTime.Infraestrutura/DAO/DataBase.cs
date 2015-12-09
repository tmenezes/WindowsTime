namespace WindowsTime.Infraestrutura.DAO
{
    public static class DataBase
    {
        public static ISessionFactoryHolder SessionFactoryHolder { get; private set; }

        public static void Inicializar(ISessionFactoryHolder sessionFactoryHolder)
        {
            sessionFactoryHolder.InitializeSessionFactory();

            SessionFactoryHolder = sessionFactoryHolder;
        }
    }
}