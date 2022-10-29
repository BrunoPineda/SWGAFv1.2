using BackendSWGAF.Context;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

namespace BackendSWGAF.Helpers
{
    public class SqlHelper
    {
        // Set the connection, command, and then execute the command with non query.  
        public static Int32 ExecuteNonQuery(AppDbContext context, String commandText,
            CommandType commandType, params SqlParameter[] parameters)
        {
            // using (SqlConnection conn = (SqlConnection)context.Database.GetDbConnection())
            // {
            SqlConnection conn = (SqlConnection)context.Database.GetDbConnection();
                using (SqlCommand cmd = new SqlCommand(commandText, conn))
                {
                    // There're three command types: StoredProcedure, Text, TableDirect. The TableDirect   
                    // type is only for OLE DB.    
                    cmd.CommandType = commandType;
                    cmd.Parameters.AddRange(parameters);

                    conn.Open();
                    return cmd.ExecuteNonQuery();
                }
            //}
        }
  
        public static string message;
        public static void InfoMessageHandler(object mySender, SqlInfoMessageEventArgs myEvent)
        {
            message = (myEvent.Message);
        }
        public static string ExecuteNonQueryShowMessage(AppDbContext context, String commandText,
         CommandType commandType, params SqlParameter[] parameters)
        {
            // using (SqlConnection conn = (SqlConnection)context.Database.GetDbConnection())
            // {
            SqlConnection conn = (SqlConnection)context.Database.GetDbConnection();
            using (SqlCommand cmd = new SqlCommand(commandText, conn))
            {
                // There're three command types: StoredProcedure, Text, TableDirect. The TableDirect   
                // type is only for OLE DB.    
                cmd.CommandType = commandType;
                cmd.Parameters.AddRange(parameters);

                conn.Open();
                conn.InfoMessage += new SqlInfoMessageEventHandler(InfoMessageHandler);
                cmd.ExecuteNonQuery();
                return message;
            }
            //}
        }

        // Set the connection, command, and then execute the command and only return one value.  
        public static Object ExecuteScalar(AppDbContext context, String commandText,
            CommandType commandType, params SqlParameter[] parameters)
        {
            SqlConnection conn = (SqlConnection)context.Database.GetDbConnection();
            
            using (SqlCommand cmd = new SqlCommand(commandText, conn))
            {
                cmd.CommandType = commandType;
                cmd.Parameters.AddRange(parameters);

                conn.Open();
                return cmd.ExecuteScalar();
            }
        }

        // Set the connection, command, and then execute the command with query and return the reader.  
        public static SqlDataReader ExecuteReader(AppDbContext context, String commandText,
            CommandType commandType, params SqlParameter[] parameters)
        {
            SqlConnection conn = (SqlConnection)context.Database.GetDbConnection();

            using (SqlCommand cmd = new SqlCommand(commandText, conn))
            {
                cmd.CommandType = commandType;
                cmd.Parameters.AddRange(parameters);

                conn.Open();
                // When using CommandBehavior.CloseConnection, the connection will be closed when the   
                // IDataReader is closed.  
                SqlDataReader reader = cmd.ExecuteReader(CommandBehavior.CloseConnection);

                return reader;
            }
        }
    }
}
