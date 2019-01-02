using Oracle.ManagedDataAccess.Client;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LAssembly.Utility
{
    public class DBServiceLib
    {
        public static DBServiceLib DBInstance()
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
                            MessageBoxsHelper.ShowInformation(ex.ToString());
                        }
                        finally
                        {
                            conn.Clone();
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
                            MessageBoxsHelper.ShowInformation(ex.ToString());
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
                MessageBoxsHelper.ShowInformation(ex.ToString());
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
                                MessageBoxsHelper.ShowInformation(ex.ToString());
                            }
                            finally
                            {
                                conn.Clone();
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
                                MessageBoxsHelper.ShowInformation(ex.ToString());
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
                    MessageBoxsHelper.ShowInformation(ex.ToString());
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
                        MessageBoxsHelper.ShowInformation(ex.ToString());
                        return -1;
                    }
                    finally
                    {
                        conn.Clone();
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
                        MessageBoxsHelper.ShowInformation(ex.ToString());
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
                        MessageBoxsHelper.ShowInformation(ex.ToString());
                        return -1;
                    }
                    finally
                    {
                        conn.Clone();
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
                        MessageBoxsHelper.ShowInformation(ex.ToString());
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
                                MessageBoxsHelper.ShowInformation(ex.ToString());
                                return false;
                            }
                        }
                        tran.Commit();
                        return true;
                    }

                    catch (OracleException ex)
                    {
                        tran.Rollback();
                        MessageBoxsHelper.ShowInformation(ex.ToString());
                        return false;
                    }
                    finally
                    {
                        conn.Clone();
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
                        MessageBoxsHelper.ShowInformation(ex.ToString());
                    }
                    finally
                    {
                        conn.Clone();
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
                        MessageBoxsHelper.ShowInformation(ex.ToString());
                    }
                    finally
                    {
                        conn.Close();
                    }
                }
            }
            return result;
        }

        public int ExcuteSqlImage(string sql, byte[] imgByte)
        {
            var connSetting = System.Configuration.ConfigurationManager.ConnectionStrings["Oracle"];
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
                        MessageBoxsHelper.ShowInformation(ex.ToString());
                    }
                    finally
                    {
                        conn.Clone();
                    }
                }
            }
            return 0;
        }
    }
}