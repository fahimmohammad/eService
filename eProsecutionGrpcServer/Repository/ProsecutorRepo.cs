using eProsecutionGrpcServer.DAO;
using eProsecutionGrpcServer.Model;
using eTPS.Utilities.EncryptionEngine;
using Microsoft.EntityFrameworkCore;


namespace eProsecutionGrpcServer.Repository
{
    public class ProsecutorRepo : IProsecutor
    {
        private readonly ServerDbContext _dbContext = null;
        public ProsecutorRepo(ServerDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        
        public Prosecutor ProsecutroLogin(string id, string password)
        {
            password = Encryption.Instance().EncryptWord(password);
            string sql = "Select PCT.PROSECUTORID AS ID,PCT.LOGINNAME,PP.NAME,PMS_DIVISION.NAME AS DIVISIONNAME"
                            + " FROM PMS_PROSECUTOR_TERMINALACCESS PCT "
                            + " INNER JOIN PMS_PROSECUTOR PP ON PP.ID = PCT.PROSECUTORID"
                            + " INNER JOIN PMS_DIVISION ON PMS_DIVISION.ID=PP.DIVISIONID " +
                            "WHERE LOGINNAME='" + id +
                            "' AND PASSWORD='" + password + "'";
            var data = _dbContext.Prosecutor.FromSqlRaw<Prosecutor>(sql).AsEnumerable().FirstOrDefault();
                //FromSql($"Select PCT.PROSECUTORID AS ID,PCT.LOGINNAME,PP.NAME,PMS_DIVISION.NAME AS DIVISIONNAME FROM PMS_PROSECUTOR_TERMINALACCESS PCT  INNER JOIN PMS_PROSECUTOR PP ON PP.ID = PCT.PROSECUTORID INNER JOIN PMS_DIVISION ON PMS_DIVISION.ID=PP.DIVISIONID WHERE LOGINNAME='8917203417' AND PASSWORD='{password}'").FirstOrDefault();
            return data;
        }
    }
}
