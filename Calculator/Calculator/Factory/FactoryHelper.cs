using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;

namespace Calculator.Factory
{
    public class FactoryHelper
    {
        private static string _repositoryType;

        public static string GetRepositoryType()
        {
            if (string.IsNullOrEmpty(_repositoryType))
                _repositoryType = ConfigurationManager.AppSettings["RepositoryType"].ToString();

            return _repositoryType;
        }
    }
}