using System;
using System.Collections.Generic;
using System.Linq;
using System.Data.SqlClient;
using System.Data;
using System.Web;
using System.Configuration;

namespace Food_Feast.Models
{
    public class DB
    {
        private SqlCommand ExecuteCommand(string command, SqlParameter[] sqlParameters)
        {
            var conn = new SqlConnection(ConfigurationManager.ConnectionStrings["Food_Feast"].ConnectionString);
            using (var cmd = new SqlCommand(command, conn))
            {
                conn.Open();
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddRange(sqlParameters);
                return cmd;
            }
        }

        private SqlCommand ExecuteCommandWithoutParameter(string command)
        {
            var conn = new SqlConnection(ConfigurationManager.ConnectionStrings["Food_Feast"].ConnectionString);
            using (var cmd = new SqlCommand(command, conn))
            {
                conn.Open();
                cmd.CommandType = CommandType.StoredProcedure;
                return cmd;
            }
        }

        public SqlDataReader ExeccuteCommandReader(string command, SqlParameter[] sqlParameters)
        {
            try
            {
                SqlCommand cmd = ExecuteCommand(command, sqlParameters);
                return cmd.ExecuteReader();
            }
            catch (Exception e)
            {
                return null;
            }
        }

        public SqlDataReader ExeccuteCommandReaderWithoutParameter(string command)
        {
            try
            {
                SqlCommand cmd = ExecuteCommandWithoutParameter(command);
                return cmd.ExecuteReader();
            }
            catch (Exception e)
            {
                return null;
            }
        }

        public int ExeccuteCommandAffected(string command, SqlParameter[] sqlParameters)
        {
            try
            {
                SqlCommand cmd = ExecuteCommand(command, sqlParameters);
                return cmd.ExecuteNonQuery();
            }
            catch (Exception e)
            {
                return 0;
            }

        }

        public int ExeccuteCommandAffectedWithoutParameter(string command)
        {
            try
            {
                SqlCommand cmd = ExecuteCommandWithoutParameter(command);
                return cmd.ExecuteNonQuery();
            }
            catch (Exception e)
            {
                return 0;
            }

        }
    }
}