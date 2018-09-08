using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IISM.Classes
{
    public class SQLCommand
    {

        private string dbConnection { get; } = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source=C:\\IISM\\IISM\\IISM_DB\\IISM.accdb;Persist Security Info=True";

        public SQLCommand()
        {

        }

        private string GetStringConnection()
        {

            return this.dbConnection;
        }

        public  bool DeleteQry(string strCommand)
        {
                        
            
            try
            {
                System.Data.OleDb.OleDbConnection conn = new System.Data.OleDb.OleDbConnection(dbConnection);
                conn.Open();
                System.Data.OleDb.OleDbCommand cmd = new System.Data.OleDb.OleDbCommand(strCommand, conn);
                cmd.ExecuteNonQuery();
                conn.Close();
            }
            catch (Exception)
            {

                return true;
            }

            return false;



        }



        ~SQLCommand()
        {
            //here you can write File.Close
            Console.WriteLine("Destructor");
        }
        public void Dispose()
        {
            //Close the file here
            GC.SuppressFinalize(this);
        }




    }






}
