using Oracle.ManagedDataAccess.Client;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LTools.Utility
{
    public class DBServiceLib
    {
        //private static ConnectionStringSettings _connSetting = null;
        public static DBServiceLib DBInstance(string connectKey = "Oracle")
        {
            return new DBServiceLib();
        }
        public DataSet GetDataSet(string sql, string connectKey = "Oracle")
        {
            DataSet ds = new DataSet();
            try
            {
                var connSetting = System.Configuration.ConfigurationManager.ConnectionStrings[connectKey];

                if (connSetting.ProviderName == "Oracle.DataAccess.Client")
                {
                    using (OracleConnection conn = new OracleConnection(connSetting.ConnectionString))
                    {
                        try
                        {
                            conn.Open();
                            OracleCommand cmd = conn.CreateCommand();
                            cmd.CommandText = sql;
                            OracleDataAdapter da = new OracleDataAdapter();
                            da.SelectCommand = cmd;
                            da.Fill(ds);
                        }
                        catch (OracleException ex)
                        {
                            MessageBoxEx.Show(ex.ToString(), new string[] { "确定" });
                        }
                        finally
                        {
                           conn.Close();;
                        }
                    }
                }
                else if (connSetting.ProviderName == "System.Data.SqlClient")
                {
                    using (SqlConnection conn = new SqlConnection(connSetting.ConnectionString))
                    {
                        try
                        {
                            conn.Open();
                            SqlCommand sc = new SqlCommand(sql, conn);
                            SqlDataAdapter da = new SqlDataAdapter(sc);
                            da.Fill(ds);
                        }
                        catch (SqlException ex)
                        {
                            MessageBoxEx.Show(ex.ToString(), new string[] { "确定" });
                        }
                        finally
                        {
                            conn.Close();
                        }
                    }
                }
            }
            catch (OracleException ex)
            {
                MessageBoxEx.Show(ex.ToString(), new string[] { "确定" });
                ds = null;
            }
            return ds;
        }


        /// <summary>
        ///   task.ContinueWith(t =>{  todo..
        ///    this.BeginInvoke(new System.Threading.ThreadStart(delegate () { todo update UI  }));
        /// </summary>
        /// <param name="sql"></param>
        /// <param name="connectKey"></param>
        /// <returns></returns>
        public Task<DataSet> GetTaskDataSet(string sql, string connectKey = "Oracle")
        {

            Task<DataSet> task = new Task<DataSet>(() =>
            {
                DataSet ds = new DataSet();
                try
                {
                    var connSetting = System.Configuration.ConfigurationManager.ConnectionStrings[connectKey];

                    if (connSetting.ProviderName == "Oracle.DataAccess.Client")
                    {
                        using (OracleConnection conn = new OracleConnection(connSetting.ConnectionString))
                        {
                            try
                            {
                                conn.Open();
                                OracleCommand cmd = conn.CreateCommand();
                                cmd.CommandText = sql;
                                OracleDataAdapter da = new OracleDataAdapter();
                                da.SelectCommand = cmd;
                                da.Fill(ds);
                            }
                            catch (OracleException ex)
                            {
                                MessageBoxEx.Show(ex.ToString(), new string[] { "确定" });
                            }
                            finally
                            {
                               conn.Close();;
                            }
                        }
                    }
                    else if (connSetting.ProviderName == "System.Data.SqlClient")
                    {
                        using (SqlConnection conn = new SqlConnection(connSetting.ConnectionString))
                        {
                            try
                            {
                                conn.Open();
                                SqlCommand sc = new SqlCommand(sql, conn);
                                SqlDataAdapter da = new SqlDataAdapter(sc);
                                da.Fill(ds);
                            }
                            catch (SqlException ex)
                            {
                                MessageBoxEx.Show(ex.ToString(), new string[] { "确定" });
                            }
                            finally
                            {
                                conn.Close();
                            }
                        }
                    }
                }
                catch (OracleException ex)
                {
                    MessageBoxEx.Show(ex.ToString(), new string[] { "确定" });
                    ds = null;
                }
                return ds;
            }
            );
            task.Start();
            return task;
        }
            public int GetCount(string sql, string connectKey = "Oracle")
        {
            DataSet ds = new DataSet();
            var connSetting = System.Configuration.ConfigurationManager.ConnectionStrings[connectKey];

            if (connSetting.ProviderName == "Oracle.DataAccess.Client")
            {
                using (OracleConnection conn = new OracleConnection(connSetting.ConnectionString))
                {
                    try
                    {
                        conn.Open();
                        OracleCommand cmd = conn.CreateCommand();
                        cmd.CommandText = sql;
                        OracleDataAdapter da = new OracleDataAdapter();
                        da.SelectCommand = cmd;
                        da.Fill(ds);
                        if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                        {
                            if (!string.IsNullOrEmpty(ds.Tables[0].Rows[0][0].ToString()))
                            {
                                return Convert.ToInt32(ds.Tables[0].Rows[0][0].ToString());
                            }
                            else
                            {
                                return 0;
                            }
                        }
                        else
                        {
                            return 0;
                        }
                    }
                    catch (OracleException ex)
                    {
                        MessageBoxEx.Show(ex.ToString(), new string[] { "确定" });
                        return -1;
                    }
                    finally
                    {
                       conn.Close();;
                    }
                }
            }
            else if (connSetting.ProviderName == "System.Data.SqlClient")
            {
                using (SqlConnection conn = new SqlConnection(connSetting.ConnectionString))
                {
                    try
                    {
                        conn.Open();
                        SqlCommand sc = new SqlCommand(sql, conn);
                        SqlDataAdapter da = new SqlDataAdapter(sc);
                        da.Fill(ds);
                        if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                        {
                            if (!string.IsNullOrEmpty(ds.Tables[0].Rows[0][0].ToString()))
                            {
                                return Convert.ToInt32(ds.Tables[0].Rows[0][0].ToString());
                            }
                            else
                            {
                                return 0;
                            }
                        }
                        else
                        {
                            return 0;
                        }
                    }
                    catch (SqlException ex)
                    {
                        MessageBoxEx.Show(ex.ToString(), new string[] { "确定" });
                        return -1;
                    }
                    finally
                    {
                        conn.Close();
                    }
                }
            }
            return 0;
        }

        public int SQLExecute(string sql, string connectKey = "Oracle")
        {
            var connSetting = System.Configuration.ConfigurationManager.ConnectionStrings[connectKey];

            if (connSetting.ProviderName == "Oracle.DataAccess.Client")
            {
                using (OracleConnection conn = new OracleConnection(connSetting.ConnectionString))
                {
                    try
                    {
                        conn.Open();
                        OracleCommand cmd = conn.CreateCommand();
                        cmd.CommandText = sql;
                        return cmd.ExecuteNonQuery();
                    }
                    catch (OracleException ex)
                    {
                        MessageBoxEx.Show(ex.ToString(), new string[] { "确定" });
                        return -1;
                    }
                    finally
                    {
                       conn.Close();;
                    }
                }
            }
            else if (connSetting.ProviderName == "System.Data.SqlClient")
            {
                using (SqlConnection conn = new SqlConnection(connSetting.ConnectionString))
                {
                    try
                    {
                        conn.Open();
                        SqlCommand sc = new SqlCommand(sql, conn);
                        return sc.ExecuteNonQuery();
                    }
                    catch (SqlException ex)
                    {
                        MessageBoxEx.Show(ex.ToString(), new string[] { "确定" });
                        return -1;
                    }
                    finally
                    {
                        conn.Close();
                    }
                }
            }

            return 0;
        }

        public bool ExcuteSqlTran(List<string> sql)
        {
            var connSetting = System.Configuration.ConfigurationManager.ConnectionStrings["Oracle"];

            if (connSetting.ProviderName == "Oracle.DataAccess.Client")
            {
                using (OracleConnection conn = new OracleConnection(connSetting.ConnectionString))
                {
                    OracleTransaction tran = conn.BeginTransaction();

                    try
                    {
                        conn.Open();
                        foreach (string item in sql)
                        {
                            try
                            {
                                OracleCommand cmd = new OracleCommand();
                                cmd.Transaction = tran;
                                cmd.Connection = conn;
                                cmd.CommandText = item;
                                cmd.ExecuteNonQuery();
                            }
                            catch (OracleException ex)
                            {
                                MessageBoxEx.Show(ex.ToString(), new string[] { "确定" });
                                return false;
                            }
                        }
                        tran.Commit();
                        return true;
                    }

                    catch (OracleException ex)
                    {
                        tran.Rollback();
                        MessageBoxEx.Show(ex.ToString(), new string[] { "确定" });
                        return false;
                    }
                    finally
                    {
                       conn.Close();;
                    }
                }
            }
            return true;
        }

        public string GetSingle(string sql, string connectKey = "Oracle")
        {
            string result = string.Empty;
            DataSet ds = new DataSet();
            var connSetting = System.Configuration.ConfigurationManager.ConnectionStrings[connectKey];
            if (connSetting.ProviderName == "Oracle.DataAccess.Client")
            {
                using (OracleConnection conn = new OracleConnection(connSetting.ConnectionString))
                {
                    try
                    {
                        conn.Open();
                        OracleCommand cmd = conn.CreateCommand();
                        cmd.CommandText = sql;
                        OracleDataAdapter da = new OracleDataAdapter();
                        da.SelectCommand = cmd;
                        da.Fill(ds);
                        if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                        {
                            result = ds.Tables[0].Rows[0][0].ToString();
                        }
                    }
                    catch (OracleException ex)
                    {
                        MessageBoxEx.Show(ex.ToString(), new string[] { "确定" });
                    }
                    finally
                    {
                       conn.Close();;
                    }
                }
            }
            else if (connSetting.ProviderName == "System.Data.SqlClient")
            {
                using (SqlConnection conn = new SqlConnection(connSetting.ConnectionString))
                {
                    try
                    {
                        conn.Open();
                        SqlCommand sc = new SqlCommand(sql, conn);
                        SqlDataAdapter da = new SqlDataAdapter(sc);
                        da.Fill(ds);
                        if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                        {
                            result = ds.Tables[0].Rows[0][0].ToString();
                        }
                    }
                    catch (SqlException ex)
                    {
                        MessageBoxEx.Show(ex.ToString(), new string[] { "确定" });
                    }
                    finally
                    {
                        conn.Close();
                    }
                }
            }
            return result;
        }

        public int ExcuteSqlImage(string sql, byte[] imgByte, string connectKey = "Oracle")
        {
            var connSetting = System.Configuration.ConfigurationManager.ConnectionStrings[connectKey];
            if (connSetting.ProviderName == "Oracle.DataAccess.Client")
            {
                using (OracleConnection conn = new OracleConnection(connSetting.ConnectionString))
                {
                    try
                    {
                        conn.Open();
                        OracleCommand cmd = conn.CreateCommand();
                        cmd.CommandText = sql;
                        cmd.Parameters.Add("fs", OracleDbType.Blob, imgByte.Length);
                        cmd.Parameters[0].Value = imgByte;
                        return cmd.ExecuteNonQuery();

                    }
                    catch (OracleException ex)
                    {
                        MessageBoxEx.Show(ex.ToString(), new string[] { "确定" });
                    }
                    finally
                    {
                        conn.Close();
                    }
                }
            }
            return 0;
        }

        /// <summary>
        /// 执行oracle存储过程
        /// </summary>
        /// <param name="tableName"></param>
        /// <param name="paramers"> OracleParameter[] parameters = {new OracleParameter("as_tablename", OracleType.VarChar),  new OracleParameter("l_value", OracleType.Number)	};	parameters[0].Value = TableName;    parameters[1].Direction = ParameterDirection.ReturnValue;//输出</param>
       	
        /// <param name="ConnectionString"></param>
        /// <returns></returns>
        public DataSet ExecuteProOracle(string tableName, OracleParameter[] paramers, string connectKey = "Oracle")
        {
            OracleCommand oracom = new OracleCommand();
            //OracleCommand oracom2 = new OracleCommand();
            var connSetting = System.Configuration.ConfigurationManager.ConnectionStrings[connectKey];
            using (OracleConnection conn = new OracleConnection(connSetting.ConnectionString))
            {

                PrepareCommandOracle(oracom, conn, CommandType.StoredProcedure, tableName);

                foreach (System.Data.IDataParameter paramer in paramers)
                {                    
                    oracom.Parameters.Add(paramer);
                }
                OracleDataAdapter sda = new OracleDataAdapter();
                sda.SelectCommand = oracom;
                DataSet ds = new DataSet();
                sda.Fill(ds);
                oracom.Parameters.Clear();
                return ds;
            }
        }
        private static void PrepareCommandOracle(OracleCommand cmd, OracleConnection conn, CommandType cmdType, string cmdText)
        {
            if (conn.State != ConnectionState.Open)
                conn.Open();
            cmd.Connection = conn;
            cmd.CommandText = cmdText;
            cmd.CommandType = cmdType;

        }
    }
}