using eProsecutionGrpc;
using eProsecutionGrpcServer.DAO;
using Google.Protobuf.Collections;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Oracle.ManagedDataAccess.Client;
using System.Collections.Generic;
using System.Data;
using System.Security.Policy;

namespace eProsecutionGrpcServer.Repository
{
    public class DatalistRepo : IDatalist
    {
        private readonly ServerDbContext _dbContext = null;
        public DatalistRepo(ServerDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public RepeatedField<BrtaOffice> GetBrtaOffice()
        {

            RepeatedField<BrtaOffice> data = new RepeatedField<BrtaOffice>();
            var dt = new DataTable();
            var conn = _dbContext.Database.GetDbConnection();
            var connectionState = conn.State;
            try
            {
                if (connectionState != ConnectionState.Open) conn.Open();
                using (var cmd = conn.CreateCommand())
                {
                    cmd.CommandText = "SP_DOWNLOAD_BRTA_OFFICE";
                    cmd.CommandType = CommandType.StoredProcedure;
                    OracleParameter p4 = new OracleParameter("pRESULT_SET_OUT", OracleDbType.RefCursor, ParameterDirection.Output);
                    cmd.Parameters.Add(p4);
                    using (var reader = cmd.ExecuteReader())
                    {
                        dt.Load(reader);
                        if (dt.Rows.Count > 0)
                        {
                            foreach (DataRow row in dt.Rows)
                            {
                                var brtaOffice = new BrtaOffice();
                                brtaOffice.Id = row["id"].ToString();
                                brtaOffice.Code = row["code"].ToString();
                                data.Add(brtaOffice);
                            }
                        }

                    }
                }
            }
            catch (Exception ex)
            {
                // error handling
                throw;
            }
            finally
            {
                if (connectionState != ConnectionState.Closed) conn.Close();
            }

            return data;
        }

        public RepeatedField<BrtaSeries> GetBrtaSeries()
        {

            RepeatedField<BrtaSeries> data = new RepeatedField<BrtaSeries>();
            var dt = new DataTable();
            var conn = _dbContext.Database.GetDbConnection();
            var connectionState = conn.State;
            try
            {
                if (connectionState != ConnectionState.Open) conn.Open();
                using (var cmd = conn.CreateCommand())
                {
                    cmd.CommandText = "GET_VRS_BRTA_VEHICLESERIES_TELPO";
                    cmd.CommandType = CommandType.StoredProcedure;
                    OracleParameter p4 = new OracleParameter("pRESULT_SET_OUT", OracleDbType.RefCursor, ParameterDirection.Output);
                    cmd.Parameters.Add(p4);
                    using (var reader = cmd.ExecuteReader())
                    {
                        dt.Load(reader);
                        if (dt.Rows.Count > 0)
                        {
                            foreach (DataRow row in dt.Rows)
                            {
                                var brtaSeries = new BrtaSeries();
                                brtaSeries.Id = row["id"].ToString();
                                brtaSeries.Name = row["name"].ToString();
                                data.Add(brtaSeries);
                            }
                        }

                    }
                }
            }
            catch (Exception ex)
            {
                // error handling
                throw;
            }
            finally
            {
                if (connectionState != ConnectionState.Closed) conn.Close();
            }

            return data;
        }
        public RepeatedField<Location> GetLocation()
        {
            RepeatedField<Location> data = new RepeatedField<Location>();
            var dt = new DataTable();
            var conn = _dbContext.Database.GetDbConnection();
            var connectionState = conn.State;
            try
            {
                if (connectionState != ConnectionState.Open) conn.Open();
                using (var cmd = conn.CreateCommand())
                {
                    cmd.CommandText = "SP_LocationDownload";
                    cmd.CommandType = CommandType.StoredProcedure;
                    OracleParameter p4 = new OracleParameter("pRecordSet", OracleDbType.RefCursor, ParameterDirection.Output);
                    cmd.Parameters.Add(p4);
                    using (var reader = cmd.ExecuteReader())
                    {
                        dt.Load(reader);
                        if (dt.Rows.Count > 0)
                        {
                            foreach (DataRow row in dt.Rows)
                            {
                                var location = new Location();
                                location.Id = row["id"].ToString();
                                location.LocationName = row["location"].ToString();
                                data.Add(location);
                            }
                        }

                    }
                }
            }
            catch (Exception ex)
            {
                // error handling
                throw;
            }
            finally
            {
                if (connectionState != ConnectionState.Closed) conn.Close();
            }

            return data;
        }

        public RepeatedField<ProsecutionCode> GetProsecutionCode(long userid)
        {
            RepeatedField<ProsecutionCode> data = new RepeatedField<ProsecutionCode>();
            var dt = new DataTable();
            var conn = _dbContext.Database.GetDbConnection();
            var connectionState = conn.State;
            try
            {
                if (connectionState != ConnectionState.Open) conn.Open();
                using (var cmd = conn.CreateCommand())
                {
                    cmd.CommandText = "SQI_PROSECUTION_DOWNLOAD_WIZ";
                    cmd.CommandType = CommandType.StoredProcedure;
                    OracleParameter inParam = new OracleParameter("pUserId", OracleDbType.Int64, ParameterDirection.Input);
                    inParam.Value = userid;
                    cmd.Parameters.Add(inParam);
                    //cmd.Parameters.Add(new OracleParameter("pUserId", OracleDbType.Int64, ParameterDirection.Input).Value= userid);
                    // OracleParameter outParam = new OracleParameter("pRESULT_SET_OUT", OracleDbType.RefCursor, ParameterDirection.Output);
                    cmd.Parameters.Add(new OracleParameter("pRESULT_SET_OUT", OracleDbType.RefCursor, ParameterDirection.Output));
                    using (var reader = cmd.ExecuteReader())
                    {
                        dt.Load(reader);
                        if (dt.Rows.Count > 0)
                        {
                            foreach (DataRow row in dt.Rows)
                            {
                                var prosecutionCode = new ProsecutionCode();
                                prosecutionCode.Id = row["id"].ToString();
                                prosecutionCode.Code = row["code"].ToString();
                                prosecutionCode.Cid = row["cid"].ToString();
                                prosecutionCode.Comment = row["comments"].ToString();
                                data.Add(prosecutionCode);
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                // error handling
                throw;
            }
            finally
            {
                if (connectionState != ConnectionState.Closed) conn.Close();
            }

            return data;
        }

        public RepeatedField<SeizedDocument> GetSeizedDocument()
        {
            RepeatedField<SeizedDocument> data = new RepeatedField<SeizedDocument>();
            var dt = new DataTable();
            var conn = _dbContext.Database.GetDbConnection();
            var connectionState = conn.State;
            try
            {
                if (connectionState != ConnectionState.Open) conn.Open();
                using (var cmd = conn.CreateCommand())
                {
                    cmd.CommandText = "SQI_DOWNLOAD_SEIZED_DOC_WIZ";
                    cmd.CommandType = CommandType.StoredProcedure;
                    OracleParameter outParam = new OracleParameter("pRESULT_SET_OUT", OracleDbType.RefCursor, ParameterDirection.Output);
                    cmd.Parameters.Add(outParam);
                    using (var reader = cmd.ExecuteReader())
                    {
                        dt.Load(reader);
                        if (dt.Rows.Count > 0)
                        {
                            foreach (DataRow row in dt.Rows)
                            {
                                var seizedDocument = new SeizedDocument();
                                seizedDocument.Id = row["id"].ToString();
                                seizedDocument.Shortname = row["shortname"].ToString();
                                data.Add(seizedDocument);
                            }
                        }

                    }
                }
            }
            catch (Exception ex)
            {
                // error handling
                throw;
            }
            finally
            {
                if (connectionState != ConnectionState.Closed) conn.Close();
            }

            return data;
        }

    }
}
