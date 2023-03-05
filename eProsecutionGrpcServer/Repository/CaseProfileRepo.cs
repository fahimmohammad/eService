using eProsecutionGrpc;
using eProsecutionGrpcServer.DAO;
using eProsecutionGrpcServer.Model;
using Google.Protobuf.Collections;
using Oracle.ManagedDataAccess.Client;
using System.Data;

namespace eProsecutionGrpcServer.Repository
{
    public class CaseProfileRepo : ICaseProfile
    {
        private readonly ServerDbContext _dbContext = null;
        public CaseProfileRepo(ServerDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public CaseProfile CaseEntry(CaseProfileReq req)
        {
            var conn = _dbContext.Database.GetDbConnection();
            var connectionState = conn.State;
            try
            {
                string caseId = String.Empty;
                string caseRequest = String.Empty;

                if (connectionState != ConnectionState.Open) conn.Open();
                var cmd = conn.CreateCommand();
                DateTime currDateTime = DateTime.Now;
                cmd.CommandText = "SQI_CASEPROFILE_PROCESSOR_TELPO";
                cmd.CommandType = CommandType.StoredProcedure;

                OracleParameter inParam = new OracleParameter("pExecuteType", OracleDbType.Varchar2, ParameterDirection.Input);
                inParam.Value = "0";
                cmd.Parameters.Add(inParam);

                inParam = new OracleParameter("pBRTA_ZONE_ID", OracleDbType.Varchar2, ParameterDirection.Input);
                inParam.Value = req.BrtaZoneId;
                cmd.Parameters.Add(inParam);

                inParam = new OracleParameter("pSERIES_ID", OracleDbType.Int64, ParameterDirection.Input);
                inParam.Value = req.SeriesId;
                cmd.Parameters.Add(inParam);

                inParam = new OracleParameter("pVEHICLE_NO", OracleDbType.Varchar2, ParameterDirection.Input);
                inParam.Value = req.VehicleNo;
                cmd.Parameters.Add(inParam);

                inParam = new OracleParameter("pPREVIOUS_CASE_ID", OracleDbType.Int64, ParameterDirection.Input);
                inParam.Value = 0;
                cmd.Parameters.Add(inParam);

                inParam = new OracleParameter("pACCUSED_PERSON_NAME", OracleDbType.Varchar2, ParameterDirection.Input);
                inParam.Value = req.AccusedPersonName;
                cmd.Parameters.Add(inParam);

                inParam = new OracleParameter("pACCUSED_PERSON_FATHER", OracleDbType.Varchar2, ParameterDirection.Input);
                inParam.Value = req.AccusedPersonFather;
                cmd.Parameters.Add(inParam);

                inParam = new OracleParameter("pADDRESS_LINE_1", OracleDbType.Varchar2, ParameterDirection.Input);
                inParam.Value = req.AddressLine1;
                cmd.Parameters.Add(inParam);

                inParam = new OracleParameter("pADDRESS_LINE_2", OracleDbType.Varchar2, ParameterDirection.Input);
                inParam.Value = req.AddressLine1;
                cmd.Parameters.Add(inParam);

                inParam = new OracleParameter("pPHONE_NUMBER", OracleDbType.Varchar2, ParameterDirection.Input);
                inParam.Value = req.MobileNumber;
                cmd.Parameters.Add(inParam);

                inParam = new OracleParameter("pCASE_LOCATION", OracleDbType.Varchar2, ParameterDirection.Input);
                inParam.Value = req.CaseLocation;
                cmd.Parameters.Add(inParam);

                inParam = new OracleParameter("pCOMMENTS_1", OracleDbType.Varchar2, ParameterDirection.Input);
                inParam.Value = req.Comments1;
                cmd.Parameters.Add(inParam);

                inParam = new OracleParameter("pCOMMENTS_2", OracleDbType.Varchar2, ParameterDirection.Input);
                inParam.Value = req.Comments1;
                cmd.Parameters.Add(inParam);

                inParam = new OracleParameter("pWITNESS", OracleDbType.Varchar2, ParameterDirection.Input);
                inParam.Value = req.Witness;
                cmd.Parameters.Add(inParam);

                inParam = new OracleParameter("pPROSECUTOR_ID", OracleDbType.Int64, ParameterDirection.Input);
                inParam.Value = req.ProsecutorId;
                cmd.Parameters.Add(inParam);

                inParam = new OracleParameter("pTOTAL_AMOUNT", OracleDbType.Int64, ParameterDirection.Input);
                inParam.Value = 0;
                cmd.Parameters.Add(inParam);

                inParam = new OracleParameter("pDATE_OF_OFFENCE", OracleDbType.Date, ParameterDirection.Input);
                inParam.Value = currDateTime;
                cmd.Parameters.Add(inParam);

                inParam = new OracleParameter("pCASE_ENTRY_MEDIUM", OracleDbType.Int64, ParameterDirection.Input);
                inParam.Value = 1;
                cmd.Parameters.Add(inParam);

                inParam = new OracleParameter("pRELEASE_STATUS", OracleDbType.Int64, ParameterDirection.Input);
                inParam.Value = 0;
                cmd.Parameters.Add(inParam);

                inParam = new OracleParameter("pRELEASE_DATE", OracleDbType.Date, ParameterDirection.Input);
                inParam.Value = currDateTime.AddDays(15);
                cmd.Parameters.Add(inParam);

                inParam = new OracleParameter("pRELEASE_BY", OracleDbType.Int64, ParameterDirection.Input);
                inParam.Value = 0;
                cmd.Parameters.Add(inParam);

                inParam = new OracleParameter("pCASE_STATUS", OracleDbType.Int64, ParameterDirection.Input);
                inParam.Value = 0;
                cmd.Parameters.Add(inParam);

                inParam = new OracleParameter("pCREATE_BY", OracleDbType.Int64, ParameterDirection.Input);
                inParam.Value = req.ProsecutorId;
                cmd.Parameters.Add(inParam);

                inParam = new OracleParameter("pCREATE_DATE", OracleDbType.Date, ParameterDirection.Input);
                inParam.Value = currDateTime;
                cmd.Parameters.Add(inParam);

                inParam = new OracleParameter("pCASEID", OracleDbType.Int64, ParameterDirection.Output);
                cmd.Parameters.Add(inParam);
                inParam = new OracleParameter("pCase_Request", OracleDbType.Varchar2, ParameterDirection.Output);
                inParam.Size = 255;
                cmd.Parameters.Add(inParam);
                cmd.ExecuteNonQuery();

                caseId = cmd.Parameters["pCaseId"].Value.ToString();
                caseRequest = cmd.Parameters["pCase_Request"].Value.ToString();
                int totalAmount = 0;
                DataTable dt = new DataTable();
                string seizedDocumentsName = "";
                RepeatedField<CaseProsecutions> caseProsecutions = new RepeatedField<CaseProsecutions>();
                if (!caseId.Equals(String.Empty) && caseRequest== "New Req")
                {
                    string prosecutionCode = String.Empty;
                    foreach (var prosecution in req.CaseProsecutions)
                    {
                        CaseProsecutions caseProsecution = new CaseProsecutions();
                        RepeatedField<String> comments = new RepeatedField<String>();
                        foreach (var comment in prosecution.Comment) {
                           
                            cmd = conn.CreateCommand();
                            cmd.CommandText = "SQI_CASEPROFILE_PROSECUTIONS_TELPO";
                            cmd.CommandType = CommandType.StoredProcedure;
                            long Id = Convert.ToInt64(DateTime.Now.Year.ToString() + DateTime.Now.Month.ToString().PadLeft(2, '0') +
                                                                    DateTime.Now.Day.ToString().PadLeft(2, '0') +
                                                                    DateTime.Now.ToString("HH").PadLeft(2, '0') +
                                                                    DateTime.Now.ToString("mm").PadLeft(2, '0') +
                                                                    DateTime.Now.ToString("ss").PadLeft(2, '0') +
                                                                    DateTime.Now.ToString("ffff"));
                            inParam = new OracleParameter("pExecuteType", OracleDbType.Varchar2, ParameterDirection.Input);
                            inParam.Value = "0";
                            cmd.Parameters.Add(inParam);

                            inParam = new OracleParameter("pID", OracleDbType.Int32, ParameterDirection.Input);
                            inParam.Value = Id;
                            cmd.Parameters.Add(inParam);

                            inParam = new OracleParameter("pCASEID", OracleDbType.Varchar2, ParameterDirection.Input);
                            inParam.Value = caseId;
                            cmd.Parameters.Add(inParam);

                            inParam = new OracleParameter("pPROSECUTIONID", OracleDbType.Int32, ParameterDirection.Input);
                            inParam.Value = prosecution.Prosecution;
                            cmd.Parameters.Add(inParam);

                            inParam = new OracleParameter("pPROSECUTION_CID", OracleDbType.Int32, ParameterDirection.Input);
                            inParam.Value = comment;
                            cmd.Parameters.Add(inParam);

                            inParam = new OracleParameter("pAMOUNT", OracleDbType.Int32, ParameterDirection.Input);
                            inParam.Value = 0;
                            cmd.Parameters.Add(inParam);

                            inParam = new OracleParameter("pSTATUS", OracleDbType.Varchar2, ParameterDirection.Input);
                            inParam.Value = "0";
                            cmd.Parameters.Add(inParam);

                            inParam = new OracleParameter("pRESULT_SET_OUT", OracleDbType.RefCursor, ParameterDirection.Output);
                            cmd.Parameters.Add(inParam);
                            dt = new DataTable();
                            using (var reader = cmd.ExecuteReader())
                            {
                                dt.Load(reader);
                                if (dt.Rows.Count > 0)
                                {
                                    foreach (DataRow row in dt.Rows)
                                    {
                                        comments.Add(dt.Rows[0]["COMMENTS"].ToString());
                                        prosecutionCode = dt.Rows[0]["CODE"].ToString();
                                    }
                                }

                            }
                        }
                        caseProsecution.Prosecution = prosecutionCode;
                        caseProsecution.Comment.Add(comments);
                        caseProsecutions.Add(caseProsecution);

                    }
                    var seizedDocuments = req.SeizedDocuments.Split(",");
                    foreach (var seizedDocument in seizedDocuments)
                    {
                        cmd = conn.CreateCommand();
                        cmd.CommandText = "SQI_CASEPROFILE_DOCUMENTS_TELPO";
                        cmd.CommandType = CommandType.StoredProcedure;

                        inParam = new OracleParameter("pCASEID", OracleDbType.Varchar2, ParameterDirection.Input);
                        inParam.Value = caseId.ToString();
                        cmd.Parameters.Add(inParam);
                        inParam = new OracleParameter("pSEIZED_DOCUMENTID", OracleDbType.Int32, ParameterDirection.Input);
                        inParam.Value = Int32.Parse(seizedDocument);
                        cmd.Parameters.Add(inParam);
                        inParam = new OracleParameter("pSTATUS", OracleDbType.Varchar2, ParameterDirection.Input);
                        inParam.Value = "0";
                        cmd.Parameters.Add(inParam);
                        inParam = new OracleParameter("pRESULT_SET_OUT", OracleDbType.RefCursor, ParameterDirection.Output);
                        cmd.Parameters.Add(inParam);
                        dt = new DataTable();
                        using (var reader = cmd.ExecuteReader())
                        {
                            dt.Load(reader);
                            if (dt.Rows.Count > 0)
                            {
                               seizedDocumentsName = seizedDocumentsName + dt.Rows[0]["ShortName"].ToString() + ",";
                                
                            }
                        }
                    }
                    seizedDocumentsName = seizedDocumentsName.Length >0 ?seizedDocumentsName.Remove(seizedDocumentsName.Length - 1):seizedDocumentsName;
                    cmd = conn.CreateCommand();
                    cmd.CommandText = "SQI_CASEPROFILE_UPDATEPROCESS_TELPO";
                    cmd.CommandType = CommandType.StoredProcedure;
                    inParam = new OracleParameter("pCaseId", OracleDbType.Int64, ParameterDirection.Input);
                    inParam.Value = caseId;
                    cmd.Parameters.Add(inParam);

                    inParam = new OracleParameter("pBrtaOfficeId", OracleDbType.Varchar2, ParameterDirection.Input);
                    inParam.Value = req.BrtaZoneId.ToString();
                    cmd.Parameters.Add(inParam);

                    inParam = new OracleParameter("pSeriesId", OracleDbType.Varchar2, ParameterDirection.Input);
                    inParam.Value = req.SeriesId.ToString();
                    cmd.Parameters.Add(inParam);

                    inParam = new OracleParameter("pRegistrationNo", OracleDbType.Varchar2, ParameterDirection.Input);
                    inParam.Value = req.VehicleNo;
                    cmd.Parameters.Add(inParam);
                    inParam = new OracleParameter("P_RESULT_SET_OUT", OracleDbType.RefCursor, ParameterDirection.Output);
                    cmd.Parameters.Add(inParam);
                    dt = new DataTable();
                    using (var reader = cmd.ExecuteReader())
                    {
                        dt.Load(reader);
                        if (dt.Rows.Count > 0)
                        {
                           /* foreach (DataRow row in dt.Rows)
                            {*/
                                totalAmount = Convert.ToInt32(dt.Rows[0]["TOTAL_AMOUNT"].ToString());
                           /* }*/
                        }

                    }
                }
                #region 
                /*Fetch Case Profile and Response */
                var caseProfile = new CaseProfile();
                cmd = conn.CreateCommand();
                cmd.CommandText = "SQI_GET_CASEPROFILE_BY_CASEID_TELPO";
                cmd.CommandType = CommandType.StoredProcedure;
                inParam = new OracleParameter("pCaseId", OracleDbType.Int64, ParameterDirection.Input);
                inParam.Value = caseId;
                cmd.Parameters.Add(inParam);
                inParam = new OracleParameter("P_RESULT_SET_OUT", OracleDbType.RefCursor, ParameterDirection.Output);
                cmd.Parameters.Add(inParam);
                dt = new DataTable();
                using (var reader = cmd.ExecuteReader())
                {
                    dt.Load(reader);
                    if (dt.Rows.Count > 0)
                    {
                        caseProfile.AccusedPersonName = dt.Rows[0]["ACCUSED_PERSON_NAME"].ToString();
                        caseProfile.AccusedPersonFather = dt.Rows[0]["ACCUSED_PERSON_FATHER"].ToString();
                        caseProfile.MobileNumber = dt.Rows[0]["PHONE_NUMBER"].ToString();
                        caseProfile.Address = dt.Rows[0]["ADDRESS_LINE_1"].ToString();
                        caseProfile.Witness = dt.Rows[0]["witness"].ToString();
                        caseProfile.Amount = dt.Rows[0]["TOTAL_AMOUNT"].ToString();
                        caseProfile.VehicleNo = dt.Rows[0]["VehicleNo"].ToString();
                        caseProfile.CaseLocation = dt.Rows[0]["LOCATION"].ToString();
                        caseProfile.DateOfOffense = dt.Rows[0]["DATE_OF_OFFENCE"].ToString();
                        caseProfile.LastDateofPayment = dt.Rows[0]["RELEASE_DATE"].ToString();
                    }
                    else {
                        caseProfile.AccusedPersonName = String.Empty;
                        caseProfile.AccusedPersonFather = String.Empty;
                        caseProfile.MobileNumber = String.Empty;
                        caseProfile.Address = String.Empty;
                        caseProfile.Witness = String.Empty;
                        caseProfile.Amount = String.Empty;
                        caseProfile.VehicleNo = String.Empty;
                        caseProfile.CaseLocation = String.Empty;
                        caseProfile.DateOfOffense = String.Empty;
                        caseProfile.LastDateofPayment = String.Empty;

                    }

                }
                caseProfile.ProsecutorId = req.ProsecutorId.ToString();
                caseProfile.CaseId = caseId;
                caseProfile.CaseProsecutions.Add(caseProsecutions);
                caseProfile.SeizedDocuments = seizedDocumentsName;
                #endregion


                return caseProfile;
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
        }
    }
}
