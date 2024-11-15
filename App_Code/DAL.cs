using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Configuration;
using System.Data.OracleClient;
using System.Data;
using System.Data.Common;
using System.Collections;

public class DAL
{
    public DAL()
    {


    }
    public class clsOracleConnection
    {
        public readonly string strConn = System.Configuration.ConfigurationManager.AppSettings["ConnectionString"];
        private OracleConnection _oraConn;
        private static OracleConnection _connSQLOle;

        public OracleConnection GetConnection()
        {
            try
            {
                if (this._oraConn == null)
                {
                    this._oraConn = new OracleConnection();
                    this._oraConn.ConnectionString = this.strConn;
                    this._oraConn.Open();
                    return this._oraConn;
                }
                else
                {
                    if (this._oraConn.State == ConnectionState.Open)
                        return this._oraConn;
                    this._oraConn.Open();
                    return this._oraConn;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public OracleConnection GetConnection(string conString)
        {
            try
            {
                if (this._oraConn == null)
                {
                    this._oraConn = new OracleConnection();
                    this._oraConn.ConnectionString = conString;
                    this._oraConn.Open();
                    return this._oraConn;
                }
                else
                {
                    if (this._oraConn.State == ConnectionState.Open)
                        return this._oraConn;
                    this._oraConn.Open();
                    return this._oraConn;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public OracleConnection GetConnectionTest()
        {
            try
            {
                if (this._oraConn == null)
                {
                    this._oraConn = new OracleConnection();
                    this._oraConn.ConnectionString = System.Configuration.ConfigurationManager.AppSettings["ConnectionString"];
                    this._oraConn.Open();
                    return this._oraConn;
                }
                else
                {
                    if (this._oraConn.State == ConnectionState.Open)
                        return this._oraConn;
                    this._oraConn.Open();
                    return this._oraConn;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public OracleTransaction BeginTransaction(OracleConnection Oraconn)
        {
            try
            {
                return Oraconn.BeginTransaction();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void CommitTransaction(OracleTransaction myTrans)
        {
            try
            {
                myTrans.Commit();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void RollBackTransaction(OracleTransaction myTrans)
        {
            try
            {
                myTrans.Rollback();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void CloseConnection()
        {
            try
            {
                if (this._oraConn == null)
                    return;
                this._oraConn.Close();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public static OracleConnection GetConnectionTrans()
        {
            return clsOracleConnection.GetOleConnection(clsOracleConnection.getOleConnectionString());
        }

        public static OracleConnection GetConnectionTrans(string conString)
        {
            return clsOracleConnection.GetOleConnection(conString);
        }

        internal static OracleConnection GetOleConnection(string connectionstring)
        {
            clsOracleConnection._connSQLOle = new OracleConnection();
            clsOracleConnection._connSQLOle.ConnectionString = connectionstring;
            clsOracleConnection._connSQLOle.Open();
            return clsOracleConnection._connSQLOle;
        }

        internal static string getOleConnectionString()
        {
            string input = "";
            try
            {
                input = ((object)ConfigurationManager.AppSettings["ConnectionString"]).ToString();
                //return StringEncryption.DecryptString(input);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            return input;
        }
    }
   
    public sealed class OracleDataAccess
    {
        private OracleDataAccess()
        {
        }

        
        private static void AttachParameters(OracleCommand command, OracleParameter[] commandParameters)
        {
            foreach (OracleParameter oracleParameter in commandParameters)
            {
                if (oracleParameter.Direction == ParameterDirection.InputOutput && oracleParameter.Value == null)
                {
                    oracleParameter.Value = (object)DBNull.Value;
                }
                if (oracleParameter.Value == null)
                {
                    oracleParameter.Value = (object)DBNull.Value;
                }
                if (oracleParameter.OracleType == OracleType.DateTime && oracleParameter.Value == null)
                {
                    oracleParameter.Value = (object)DBNull.Value;
                }

                if ((oracleParameter.OracleType == OracleType.DateTime && oracleParameter.Value != null))
                {
                    if (oracleParameter.Value != DBNull.Value)
                    {
                        oracleParameter.Value = Convert.ToDateTime(oracleParameter.Value).ToString("dd-MMM-yyyy");
                    }
                    else if (oracleParameter.IsNullable == false)
                    {
                        oracleParameter.Value = DateTime.Now.ToString("dd-MMM-yyyy");
                    }
                    else
                    {
                        oracleParameter.IsNullable = true;
                    }
                }

                command.Parameters.Add(oracleParameter);
            }
        }
        private static void AssignParameterValues(OracleParameter[] commandParameters, object[] parameterValues)
        {
            if (commandParameters == null || parameterValues == null)
                return;
            if (commandParameters.Length != parameterValues.Length)
                throw new ArgumentException("Parameter count does not match Parameter Value count.");
            int index = 0;
            for (int length = commandParameters.Length; index < length; ++index)
                commandParameters[index].Value = parameterValues[index];
        }

        private static void PrepareCommand(OracleCommand command, OracleConnection connection, OracleTransaction transaction, CommandType commandType, string commandText, OracleParameter[] commandParameters)
        {
            if (connection.State != ConnectionState.Open)
                connection.Open();
            command.Connection = connection;
            command.CommandText = commandText;
            if (transaction != null)
                command.Transaction = transaction;
            command.CommandType = commandType;
            if (commandParameters == null)
                return;
            OracleDataAccess.AttachParameters(command, commandParameters);
        }

        public static int ExecuteNonQuery(string connectionString, CommandType commandType, string commandText)
        {
            return OracleDataAccess.ExecuteNonQuery(connectionString, commandType, commandText, (OracleParameter[])null);
        }

        public static int ExecuteNonQuery(string connectionString, CommandType commandType, string commandText, params OracleParameter[] commandParameters)
        {
            using (OracleConnection connection = new OracleConnection(connectionString))
            {
                connection.Open();
                return OracleDataAccess.ExecuteNonQuery(connection, commandType, commandText, commandParameters);
            }
        }


        public static int ExecuteNonQuery(string connectionString, string spName, params object[] parameterValues)
        {
            if (parameterValues == null || parameterValues.Length <= 0)
                return OracleDataAccess.ExecuteNonQuery(connectionString, CommandType.StoredProcedure, spName);
            OracleParameter[] spParameterSet = OracleDataAccessParameterCache.GetSpParameterSet(connectionString, spName);
            OracleDataAccess.AssignParameterValues(spParameterSet, parameterValues);
            return OracleDataAccess.ExecuteNonQuery(connectionString, CommandType.StoredProcedure, spName, spParameterSet);
        }

        public static int ExecuteNonQuery(OracleConnection connection, CommandType commandType, string commandText)
        {
            return OracleDataAccess.ExecuteNonQuery(connection, commandType, commandText, (OracleParameter[])null);
        }

        public static int ExecuteNonQuery(OracleConnection connection, CommandType commandType, string commandText, params OracleParameter[] commandParameters)
        {
            OracleCommand command = new OracleCommand();
            OracleDataAccess.PrepareCommand(command, connection, (OracleTransaction)null, commandType, commandText, commandParameters);
            int num = command.ExecuteNonQuery();
            //command.Parameters.Clear();
            return num;
        }

        public static int ExecuteNonQuery(OracleConnection connection, string spName, params object[] parameterValues)
        {
            if (parameterValues == null || parameterValues.Length <= 0)
                return OracleDataAccess.ExecuteNonQuery(connection, CommandType.StoredProcedure, spName);
            OracleParameter[] spParameterSet = OracleDataAccessParameterCache.GetSpParameterSet(connection.ConnectionString, spName);
            OracleDataAccess.AssignParameterValues(spParameterSet, parameterValues);
            return OracleDataAccess.ExecuteNonQuery(connection, CommandType.StoredProcedure, spName, spParameterSet);
        }

        public static int ExecuteNonQuery(OracleTransaction transaction, CommandType commandType, string commandText)
        {
            return OracleDataAccess.ExecuteNonQuery(transaction, commandType, commandText, (OracleParameter[])null);
        }

        public static int ExecuteNonQuery(OracleTransaction transaction, CommandType commandType, string commandText, params OracleParameter[] commandParameters)
        {
            OracleCommand command = new OracleCommand();
            OracleDataAccess.PrepareCommand(command, transaction.Connection, transaction, commandType, commandText, commandParameters);
            int num = command.ExecuteNonQuery();
            command.Parameters.Clear();
            return num;
        }

        public static int ExecuteNonQuery(OracleTransaction transaction, string spName, params object[] parameterValues)
        {
            if (parameterValues == null || parameterValues.Length <= 0)
                return OracleDataAccess.ExecuteNonQuery(transaction, CommandType.StoredProcedure, spName);
            OracleParameter[] spParameterSet = OracleDataAccessParameterCache.GetSpParameterSet(transaction.Connection.ConnectionString, spName);
            OracleDataAccess.AssignParameterValues(spParameterSet, parameterValues);
            return OracleDataAccess.ExecuteNonQuery(transaction, CommandType.StoredProcedure, spName, spParameterSet);
        }

        public static DataSet ExecuteDataset(string connectionString, CommandType commandType, string commandText)
        {
            return OracleDataAccess.ExecuteDataset(connectionString, commandType, commandText, (OracleParameter[])null);
        }

        public static DataSet ExecuteDataset(string connectionString, CommandType commandType, string commandText, params OracleParameter[] commandParameters)
        {
            using (OracleConnection connection = new OracleConnection(connectionString))
            {
                connection.Open();
                return OracleDataAccess.ExecuteDataset(connection, commandType, commandText, commandParameters);
            }
        }

        public static DataSet ExecuteDataset(string connectionString, string spName, params object[] parameterValues)
        {
            if (parameterValues == null || parameterValues.Length <= 0)
                return OracleDataAccess.ExecuteDataset(connectionString, CommandType.StoredProcedure, spName);
            OracleParameter[] spParameterSet = OracleDataAccessParameterCache.GetSpParameterSet(connectionString, spName);
            OracleDataAccess.AssignParameterValues(spParameterSet, parameterValues);
            return OracleDataAccess.ExecuteDataset(connectionString, CommandType.StoredProcedure, spName, spParameterSet);
        }

        public static DataSet ExecuteDataset(OracleConnection connection, CommandType commandType, string commandText)
        {
            return OracleDataAccess.ExecuteDataset(connection, commandType, commandText, (OracleParameter[])null);
        }

        public static DataSet ExecuteDataset(OracleConnection connection, CommandType commandType, string commandText, params OracleParameter[] commandParameters)
        {
            OracleCommand oracleCommand = new OracleCommand();
            OracleDataAccess.PrepareCommand(oracleCommand, connection, (OracleTransaction)null, commandType, commandText, commandParameters);
            OracleDataAdapter oracleDataAdapter = new OracleDataAdapter(oracleCommand);
            DataSet dataSet = new DataSet();
            ((DataAdapter)oracleDataAdapter).Fill(dataSet);
            oracleCommand.Parameters.Clear();
            return dataSet;
        }

        public static DataSet ExecuteDataset(OracleConnection connection, string spName, params object[] parameterValues)
        {
            if (parameterValues == null || parameterValues.Length <= 0)
                return OracleDataAccess.ExecuteDataset(connection, CommandType.StoredProcedure, spName);
            OracleParameter[] spParameterSet = OracleDataAccessParameterCache.GetSpParameterSet(connection.ConnectionString, spName);
            OracleDataAccess.AssignParameterValues(spParameterSet, parameterValues);
            return OracleDataAccess.ExecuteDataset(connection, CommandType.StoredProcedure, spName, spParameterSet);
        }

        public static DataSet ExecuteDataset(OracleTransaction transaction, CommandType commandType, string commandText)
        {
            return OracleDataAccess.ExecuteDataset(transaction, commandType, commandText, (OracleParameter[])null);
        }

        public static DataSet ExecuteDataset(OracleTransaction transaction, CommandType commandType, string commandText, params OracleParameter[] commandParameters)
        {
            OracleCommand oracleCommand = new OracleCommand();
            OracleDataAccess.PrepareCommand(oracleCommand, transaction.Connection, transaction, commandType, commandText, commandParameters);
            OracleDataAdapter oracleDataAdapter = new OracleDataAdapter(oracleCommand);
            DataSet dataSet = new DataSet();
            ((DataAdapter)oracleDataAdapter).Fill(dataSet);
            oracleCommand.Parameters.Clear();
            return dataSet;
        }

        public static DataSet ExecuteDataset(OracleTransaction transaction, string spName, params object[] parameterValues)
        {
            if (parameterValues == null || parameterValues.Length <= 0)
                return OracleDataAccess.ExecuteDataset(transaction, CommandType.StoredProcedure, spName);
            OracleParameter[] spParameterSet = OracleDataAccessParameterCache.GetSpParameterSet(transaction.Connection.ConnectionString, spName);
            OracleDataAccess.AssignParameterValues(spParameterSet, parameterValues);
            return OracleDataAccess.ExecuteDataset(transaction, CommandType.StoredProcedure, spName, spParameterSet);
        }

        public static DataTable ExecuteDataTable(OracleConnection connection, CommandType commandType, string commandText, params OracleParameter[] commandParameters)
        {
            OracleCommand oracleCommand = new OracleCommand();
            OracleDataAccess.PrepareCommand(oracleCommand, connection, (OracleTransaction)null, commandType, commandText, commandParameters);
            OracleDataAdapter oracleDataAdapter = new OracleDataAdapter(oracleCommand);
            DataTable dataTable = new DataTable();
            oracleDataAdapter.Fill(dataTable);
            return dataTable;
        }

        private static OracleDataReader ExecuteReader(OracleConnection connection, OracleTransaction transaction, CommandType commandType, string commandText, OracleParameter[] commandParameters, OracleDataAccess.OracleConnectionOwnership connectionOwnership)
        {
            OracleCommand command = new OracleCommand();
            OracleDataAccess.PrepareCommand(command, connection, transaction, commandType, commandText, commandParameters);
            OracleDataReader oracleDataReader = connectionOwnership != OracleDataAccess.OracleConnectionOwnership.External ? command.ExecuteReader(CommandBehavior.CloseConnection) : command.ExecuteReader();
            command.Parameters.Clear();
            return oracleDataReader;
        }

        public static OracleDataReader ExecuteReader(string connectionString, CommandType commandType, string commandText)
        {
            return OracleDataAccess.ExecuteReader(connectionString, commandType, commandText, (OracleParameter[])null);
        }

        public static OracleDataReader ExecuteReader(string connectionString, CommandType commandType, string commandText, params OracleParameter[] commandParameters)
        {
            OracleConnection connection = new OracleConnection(connectionString);
            connection.Open();
            try
            {
                return OracleDataAccess.ExecuteReader(connection, (OracleTransaction)null, commandType, commandText, commandParameters, OracleDataAccess.OracleConnectionOwnership.Internal);
            }
            catch
            {
                connection.Close();
                throw;
            }
        }

        public static OracleDataReader ExecuteReader(string connectionString, string spName, params object[] parameterValues)
        {
            if (parameterValues == null || parameterValues.Length <= 0)
                return OracleDataAccess.ExecuteReader(connectionString, CommandType.StoredProcedure, spName);
            OracleParameter[] spParameterSet = OracleDataAccessParameterCache.GetSpParameterSet(connectionString, spName);
            OracleDataAccess.AssignParameterValues(spParameterSet, parameterValues);
            return OracleDataAccess.ExecuteReader(connectionString, CommandType.StoredProcedure, spName, spParameterSet);
        }

        public static OracleDataReader ExecuteReader(OracleConnection connection, CommandType commandType, string commandText)
        {
            return OracleDataAccess.ExecuteReader(connection, commandType, commandText, (OracleParameter[])null);
        }

        public static OracleDataReader ExecuteReader(OracleConnection connection, CommandType commandType, string commandText, params OracleParameter[] commandParameters)
        {
            return OracleDataAccess.ExecuteReader(connection, (OracleTransaction)null, commandType, commandText, commandParameters, OracleDataAccess.OracleConnectionOwnership.External);
        }

        public static OracleDataReader ExecuteReader(OracleConnection connection, string spName, params object[] parameterValues)
        {
            if (parameterValues == null || parameterValues.Length <= 0)
                return OracleDataAccess.ExecuteReader(connection, CommandType.StoredProcedure, spName);
            OracleParameter[] spParameterSet = OracleDataAccessParameterCache.GetSpParameterSet(connection.ConnectionString, spName);
            OracleDataAccess.AssignParameterValues(spParameterSet, parameterValues);
            return OracleDataAccess.ExecuteReader(connection, CommandType.StoredProcedure, spName, spParameterSet);
        }

        public static OracleDataReader ExecuteReader(OracleTransaction transaction, CommandType commandType, string commandText)
        {
            return OracleDataAccess.ExecuteReader(transaction, commandType, commandText, (OracleParameter[])null);
        }

        public static OracleDataReader ExecuteReader(OracleTransaction transaction, CommandType commandType, string commandText, params OracleParameter[] commandParameters)
        {
            return OracleDataAccess.ExecuteReader(transaction.Connection, transaction, commandType, commandText, commandParameters, OracleDataAccess.OracleConnectionOwnership.External);
        }

        public static OracleDataReader ExecuteReader(OracleTransaction transaction, string spName, params object[] parameterValues)
        {
            if (parameterValues == null || parameterValues.Length <= 0)
                return OracleDataAccess.ExecuteReader(transaction, CommandType.StoredProcedure, spName);
            OracleParameter[] spParameterSet = OracleDataAccessParameterCache.GetSpParameterSet(transaction.Connection.ConnectionString, spName);
            OracleDataAccess.AssignParameterValues(spParameterSet, parameterValues);
            return OracleDataAccess.ExecuteReader(transaction, CommandType.StoredProcedure, spName, spParameterSet);
        }

        public static object ExecuteScalar(string connectionString, CommandType commandType, string commandText)
        {
            return OracleDataAccess.ExecuteScalar(connectionString, commandType, commandText, (OracleParameter[])null);
        }

        public static object ExecuteScalar(string connectionString, CommandType commandType, string commandText, params OracleParameter[] commandParameters)
        {
            using (OracleConnection connection = new OracleConnection(connectionString))
            {
                connection.Open();
                return OracleDataAccess.ExecuteScalar(connection, commandType, commandText, commandParameters);
            }
        }

        public static object ExecuteScalar(string connectionString, string spName, params object[] parameterValues)
        {
            if (parameterValues == null || parameterValues.Length <= 0)
                return OracleDataAccess.ExecuteScalar(connectionString, CommandType.StoredProcedure, spName);
            OracleParameter[] spParameterSet = OracleDataAccessParameterCache.GetSpParameterSet(connectionString, spName);
            OracleDataAccess.AssignParameterValues(spParameterSet, parameterValues);
            return OracleDataAccess.ExecuteScalar(connectionString, CommandType.StoredProcedure, spName, spParameterSet);
        }

        public static object ExecuteScalar(OracleConnection connection, CommandType commandType, string commandText)
        {
            //return OracleDataAccess.ExecuteScalar(connection, commandType, commandText);
            OracleCommand command = new OracleCommand();
            OracleDataAccess.PrepareCommand(command, connection, (OracleTransaction)null, commandType, commandText, null);
            object obj = command.ExecuteScalar();
            return obj;
        }

        public static object ExecuteScalar(OracleConnection connection, CommandType commandType, string commandText, params OracleParameter[] commandParameters)
        {
            OracleCommand command = new OracleCommand();
            OracleDataAccess.PrepareCommand(command, connection, (OracleTransaction)null, commandType, commandText, commandParameters);
            object obj = command.ExecuteScalar();
            command.Parameters.Clear();
            return obj;
        }

        public static object ExecuteScalar(OracleConnection connection, string spName, params object[] parameterValues)
        {
            if (parameterValues == null || parameterValues.Length <= 0)
                return OracleDataAccess.ExecuteScalar(connection, CommandType.StoredProcedure, spName);
            OracleParameter[] spParameterSet = OracleDataAccessParameterCache.GetSpParameterSet(connection.ConnectionString, spName);
            OracleDataAccess.AssignParameterValues(spParameterSet, parameterValues);
            return OracleDataAccess.ExecuteScalar(connection, CommandType.StoredProcedure, spName, spParameterSet);
        }

        public static object ExecuteScalar(OracleTransaction transaction, CommandType commandType, string commandText)
        {
            return OracleDataAccess.ExecuteScalar(transaction, commandType, commandText, (OracleParameter[])null);
        }

        public static object ExecuteScalar(OracleTransaction transaction, CommandType commandType, string commandText, params OracleParameter[] commandParameters)
        {
            OracleCommand command = new OracleCommand();
            OracleDataAccess.PrepareCommand(command, transaction.Connection, transaction, commandType, commandText, commandParameters);
            object obj = command.ExecuteScalar();
            command.Parameters.Clear();
            return obj;
        }

        public static object ExecuteScalar(OracleTransaction transaction, string spName, params object[] parameterValues)
        {
            if (parameterValues == null || parameterValues.Length <= 0)
                return OracleDataAccess.ExecuteScalar(transaction, CommandType.StoredProcedure, spName);
            OracleParameter[] spParameterSet = OracleDataAccessParameterCache.GetSpParameterSet(transaction.Connection.ConnectionString, spName);
            OracleDataAccess.AssignParameterValues(spParameterSet, parameterValues);
            return OracleDataAccess.ExecuteScalar(transaction, CommandType.StoredProcedure, spName, spParameterSet);
        }

        public static DataSet convertDataReaderToDataSet(OracleDataReader reader)
        {
            DataSet dataSet = new DataSet();
            do
            {
                DataTable schemaTable = reader.GetSchemaTable();
                DataTable table = new DataTable();
                if (schemaTable != null)
                {
                    for (int index = 0; index < schemaTable.Rows.Count; ++index)
                    {
                        DataRow dataRow = schemaTable.Rows[index];
                        DataColumn column = new DataColumn((string)dataRow["ColumnName"], (Type)dataRow["DataType"]);
                        table.Columns.Add(column);
                    }
                    dataSet.Tables.Add(table);
                    while (reader.Read())
                    {
                        DataRow row = table.NewRow();
                        for (int ordinal = 0; ordinal < reader.FieldCount; ++ordinal)
                            row[ordinal] = reader.GetValue(ordinal);
                        table.Rows.Add(row);
                    }
                }
                else
                {
                    DataColumn column = new DataColumn("RowsAffected");
                    table.Columns.Add(column);
                    dataSet.Tables.Add(table);
                    DataRow row = table.NewRow();
                    row[0] = (object)reader.RecordsAffected;
                    table.Rows.Add(row);
                }
            }
            while (reader.NextResult());
            return dataSet;
        }

        private enum OracleConnectionOwnership
        {
            Internal,
            External,
        }
    }
    public sealed class OracleDataAccessParameterCache
    {
        private static Hashtable paramCache = Hashtable.Synchronized(new Hashtable());

        static OracleDataAccessParameterCache()
        {
        }

        private OracleDataAccessParameterCache()
        {
        }

        private static OracleParameter[] DiscoverSpParameterSet(string connectionString, string spName, bool includeReturnValueParameter)
        {
            using (OracleConnection connection = new OracleConnection(connectionString))
            {
                using (OracleCommand oracleCommand = new OracleCommand(spName, connection))
                {
                    connection.Open();
                    oracleCommand.CommandType = CommandType.StoredProcedure;
                    if (!includeReturnValueParameter)
                        oracleCommand.Parameters.RemoveAt(0);
                    OracleParameter[] array = new OracleParameter[oracleCommand.Parameters.Count];
                    oracleCommand.Parameters.CopyTo(array, 0);
                    return array;
                }
            }
        }

        private static OracleParameter[] CloneParameters(OracleParameter[] originalParameters)
        {
            OracleParameter[] oracleParameterArray = new OracleParameter[originalParameters.Length];
            int index = 0;
            for (int length = originalParameters.Length; index < length; ++index)
                oracleParameterArray[index] = (OracleParameter)originalParameters[index];
            return oracleParameterArray;
        }

        public static void CacheParameterSet(string connectionString, string commandText, params OracleParameter[] commandParameters)
        {
            string str = connectionString + ":" + commandText;
            OracleDataAccessParameterCache.paramCache[(object)str] = (object)commandParameters;
        }

        public static OracleParameter[] GetCachedParameterSet(string connectionString, string commandText)
        {
            string str = connectionString + ":" + commandText;
            OracleParameter[] originalParameters = (OracleParameter[])OracleDataAccessParameterCache.paramCache[(object)str];
            if (originalParameters == null)
                return (OracleParameter[])null;
            else
                return OracleDataAccessParameterCache.CloneParameters(originalParameters);
        }

        public static void CacheParameters(string cacheKey, params OracleParameter[] cmdParms)
        {
            OracleDataAccessParameterCache.paramCache[(object)cacheKey] = (object)cmdParms;
        }

        public static OracleParameter[] GetSpParameterSet(string connectionString, string spName)
        {
            return OracleDataAccessParameterCache.GetSpParameterSet(connectionString, spName, false);
        }

        public static OracleParameter[] GetSpParameterSet(string connectionString, string spName, bool includeReturnValueParameter)
        {
            string str = connectionString + ":" + spName + (includeReturnValueParameter ? ":include ReturnValue Parameter" : "");
            return OracleDataAccessParameterCache.CloneParameters((OracleParameter[])OracleDataAccessParameterCache.paramCache[(object)str] ?? (OracleParameter[])(OracleDataAccessParameterCache.paramCache[(object)str] = (object)OracleDataAccessParameterCache.DiscoverSpParameterSet(connectionString, spName, includeReturnValueParameter)));
        }

        public static OracleParameter[] GetCachedParameters(string cacheKey)
        {
            OracleParameter[] oracleParameterArray1 = (OracleParameter[])OracleDataAccessParameterCache.paramCache[(object)cacheKey];
            if (oracleParameterArray1 == null)
                return (OracleParameter[])null;
            OracleParameter[] oracleParameterArray2 = new OracleParameter[oracleParameterArray1.Length];
            int index = 0;
            for (int length = oracleParameterArray1.Length; index < length; ++index)
                oracleParameterArray2[index] = (OracleParameter)oracleParameterArray1[index];
            return oracleParameterArray2;
        }
    }
   
}