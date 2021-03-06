﻿using NHibernate;

namespace WindowsTime.Infraestrutura.DAO
{
    public interface ISessionFactoryHolder
    {
        void InitializeSessionFactory();
        ISession OpenSession();
    }
}