using eProsecutionGrpcServer.DAO;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Oracle.ManagedDataAccess.Client;
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

        public List<Datalist.BrtaOffice> GetBrtaOffice()
        {

            List<Datalist.BrtaOffice> data = new List<Datalist.BrtaOffice>();
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
                                var brtaOffice = new Datalist.BrtaOffice(id: row["id"].ToString(), code: row["code"].ToString());
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


        public List<Datalist.Location> GetLocation()
        {
            List<Datalist.Location> data = new List<Datalist.Location>();
            var dt = new DataTable();
            var conn =  _dbContext.Database.GetDbConnection();
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
                        if (dt.Rows.Count >0 ){
                            foreach (DataRow row in dt.Rows)
                            {
                                var location = new Datalist.Location(id:row["id"].ToString(), locationName:row["location"].ToString());
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

        public List<Datalist.ProsecutionCode> GetProsecutionCode(string userid)
        {

            List<Datalist.ProsecutionCode> data = new List<Datalist.ProsecutionCode>();
            var dt = new DataTable();
            var conn = _dbContext.Database.GetDbConnection();
            var connectionState = conn.State;
            try
            {
                if (connectionState != ConnectionState.Open) conn.Open();
                using (var cmd = conn.CreateCommand())
                {
                    cmd.CommandText = "SQI_PROSECUTION_DOWNLOAD";
                    cmd.CommandType = CommandType.StoredProcedure;
                    OracleParameter inParam = new OracleParameter("pUserId",OracleDbType.Int32, ParameterDirection.Input);
                    inParam.Value = Int32.Parse(userid);
                    cmd.Parameters.Add(inParam);
                    OracleParameter outParam = new OracleParameter("pRESULT_SET_OUT", OracleDbType.RefCursor, ParameterDirection.Output);
                    cmd.Parameters.Add(outParam);
                    using (var reader = cmd.ExecuteReader())
                    {
                        dt.Load(reader);
                        if (dt.Rows.Count > 0)
                        {
                            foreach (DataRow row in dt.Rows)
                            {
                                var prosecutionCode = new Datalist.ProsecutionCode(id: row["id"].ToString(), code: row["code"].ToString());
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

        public List<Datalist.SeizedDocument> GetSeizedDocument()
        {

            List<Datalist.SeizedDocument> data = new List<Datalist.SeizedDocument>();
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
                                var seizedDocument = new Datalist.SeizedDocument(id: row["id"].ToString(), shortName: row["shortname"].ToString());
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
