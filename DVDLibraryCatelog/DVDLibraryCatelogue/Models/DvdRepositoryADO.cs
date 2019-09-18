using Dapper;
using DVDLibraryCatelogue.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace DVDLibraryCatelog.Models
{
    public class DvdRepositoryADO : IDvdRepository
    {
        public IEnumerable<Dvd> GetAllDVDs()
        {
            using (var cn = new SqlConnection())
            {
                cn.ConnectionString = ConfigurationManager.ConnectionStrings["DVDLibrary"].ConnectionString;

                return cn.Query<Dvd>("DvdSelectAll", commandType: CommandType.StoredProcedure);
            }
        }

        public Dvd GetDvdById(int dvdId)
        {
            using (var cn = new SqlConnection())
            {
                cn.ConnectionString = ConfigurationManager.ConnectionStrings["DVDLibrary"].ConnectionString;

                var parameters = new DynamicParameters();
                parameters.Add("@dvdId", dvdId);

                var found = cn.Query<Dvd>("DvdSelectById", parameters, commandType: CommandType.StoredProcedure).FirstOrDefault();
                return found;
            }
        }

        public void DvdDelete(int dvdId)
        {
            using (var cn = new SqlConnection())
            {
                cn.ConnectionString = ConfigurationManager.ConnectionStrings["DVDLibrary"].ConnectionString;

                SqlCommand cmd = new SqlCommand();
                cmd.CommandText = "DvdDelete";
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@dvdId", dvdId);
                cmd.Connection = cn;
                cn.Open();
                cmd.ExecuteNonQuery();
            }
        }

        public void DvdCreate(Dvd dvd)
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
            }
        }

        public void DvdEdit(int dvdId, Dvd dvd)
        {
            using (var cn = new SqlConnection())
            {
                cn.ConnectionString = ConfigurationManager.ConnectionStrings["DVDLibrary"].ConnectionString;

                //var parameters = new DynamicParameters();
                SqlCommand cmd = new SqlCommand();
                cmd.CommandText = "DvdUpdate";
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@dvdId", dvdId);
                cmd.Parameters.AddWithValue("@title", dvd.title);
                cmd.Parameters.AddWithValue("@director", dvd.director);
                cmd.Parameters.AddWithValue("@rating", dvd.rating);
                cmd.Parameters.AddWithValue("@realeaseYear", dvd.realeaseYear);
                cmd.Parameters.AddWithValue("@notes", dvd.notes);
                cmd.Connection = cn;
                cn.Open();
                cmd.ExecuteNonQuery();
                //cn.Execute("DvdUpdate", parameters, commandType: CommandType.StoredProcedure);
            }
        }

        public IEnumerable<Dvd> SearchTerm(string category, string term)
        {
            using (var cn = new SqlConnection())
            {
                cn.ConnectionString = ConfigurationManager.ConnectionStrings["DVDLibrary"].ConnectionString;
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = cn;
                cmd.CommandType = CommandType.StoredProcedure;
                
                switch (category)
                {
                    case "title":
                        cmd.CommandText = "SelectByTitle";
                        break;
                    case "realeaseYear":
                        if (int.TryParse(term, out int year))
                        {
                            cmd.CommandText = "SelectByReleaseYear";
                            break;
                        }
                        else
                        {
                            break;
                        }
                    case "director":
                        cmd.CommandText = "SelectByDirector";
                        break;
                    case "rating":
                        cmd.CommandText = "SelectByRating";
                        break;
                    default:
                        break;
                }
                cmd.Parameters.AddWithValue("@term", term);
                
                cn.Open();
                using (SqlDataReader dr = cmd.ExecuteReader())
                {
                    while (dr.Read())
                    {
                        Dvd currentRow = new Dvd
                        {
                            dvdId = (int)dr["dvdId"],
                            director = dr["director"].ToString(),
                            title = dr["title"].ToString(),
                            realeaseYear = (int)dr["realeaseYear"],
                            rating = dr["rating"].ToString(),
                            notes = dr["notes"].ToString()
                        };

                        yield return currentRow;
                    }
                    yield break;
                }
            }
        }
    }
}