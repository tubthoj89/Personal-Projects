using Dapper;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace DVDLibraryCatelog.Models
{
    public class DvdRepositoryADO
    {
        public IEnumerable<DvdVM> GetAllDvds()
        {
            using (var cn = new SqlConnection())
            {
                cn.ConnectionString = ConfigurationManager.ConnectionStrings["DVDLibrary"].ConnectionString;

                return cn.Query<DvdVM>("DvdSelectAll", commandType: CommandType.StoredProcedure);
            }
        }

        public DvdVM GetDvdById(int id)
        {
            using (var cn = new SqlConnection())
            {
                cn.ConnectionString = ConfigurationManager.ConnectionStrings["DVDLibrary"].ConnectionString;
                
                var parameters = new DynamicParameters();
                parameters.Add("@dvdId", id);

                return cn.Query<DvdVM>("DvdSelectById", parameters, commandType: CommandType.StoredProcedure).FirstOrDefault();
            }
        }

        public void DvdDelete(int id)
        {
            using (var cn = new SqlConnection())
            {
                cn.ConnectionString = ConfigurationManager.ConnectionStrings["DVDLibrary"].ConnectionString;
                
                var parameters = new DynamicParameters();
                parameters.Add("@dvdId", id);

                cn.Execute("DvdDelete", parameters, commandType: CommandType.StoredProcedure);
            }
        }

        public void DvdInsert(DvdVM dvd)
        {
            using (var cn = new SqlConnection())
            {
                cn.ConnectionString = ConfigurationManager.ConnectionStrings["DVDLibrary"].ConnectionString;
                
                var parameters = new DynamicParameters();
                
                parameters.Add("@dvdId", dbType: DbType.Int32, direction: ParameterDirection.Output);
                parameters.Add("@title", dvd.title);
                parameters.Add("@director", dvd.director);
                parameters.Add("@rating", dvd.rating);
                parameters.Add("@realeaseYear", dvd.realeaseYear);
                parameters.Add("@notes", dvd.notes);

                cn.Execute("DvdInsert", parameters, commandType: CommandType.StoredProcedure);

                dvd.dvdId = parameters.Get<int>("@dvdId");
            }
        }

        public void DvdEdit(DvdVM dvd)
        {
            using (var cn = new SqlConnection())
            {
                cn.ConnectionString = ConfigurationManager.ConnectionStrings["DVDLibrary"].ConnectionString;
                
                var parameters = new DynamicParameters();
                
                parameters.Add("@dvdId", dvd.dvdId);
                parameters.Add("@title", dvd.title);
                parameters.Add("@director", dvd.director);
                parameters.Add("@rating", dvd.rating);
                parameters.Add("@realeaseYear", dvd.realeaseYear);
                parameters.Add("@notes", dvd.notes);

                cn.Execute("DvdUpdate", parameters, commandType: CommandType.StoredProcedure);
            }
        }


    }
}