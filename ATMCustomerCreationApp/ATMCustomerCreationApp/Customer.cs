using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Oracle.ManagedDataAccess.Client;
using System.Data;

namespace ATMCustomerCreationApp
{
    class Customer
    {
        private int custID;
        private string firstName;
        private string lastName;
        private string gender;
        private string address;
        private string phoneNo;
        private DateTime dob;

        public Customer()
        {
            custID = 000;
            firstName = "No name";
            lastName = "No name";
            gender = "U";
            address = "No address";
            phoneNo = "0000000000";
            dob = DateTime.MinValue;

        }

        public void setCustID(int custID)
        {
            this.custID = custID;
        }

        public void setFirstName(string firstName)
        {
            this.firstName = firstName;
        }

        public void setLastName(string lastName)
        {
            this.lastName = lastName;
        }

        public void setGender(string gender)
        {
            this.gender = gender;
        }

        public void setAddress(string address)
        {
            this.address = address;
        }

        public void setPhoneNo(string phoneNo)
        {
            this.phoneNo = phoneNo;
        }

        public void setDateTime(DateTime dob)
        {
            this.dob = dob;
        }

        public int getCustID()
        {
            return custID;
        }

        public string getFirstName()
        {
            return firstName;
        }

        public string getLastName()
        {
            return lastName;
        }

        public string getGender()
        {
            return gender;
        }

        public string getAddress()
        {
            return address;
        }

        public string getPhoneNo()
        {
            return phoneNo;
        }

        public DateTime getDOB()
        {
            return dob;
        }

        //define a static method to get data
        public static DataSet getAllCustomers(DataSet DS)
        {
            OracleConnection conn = new OracleConnection(DataConnection.oracConn);

            //connection name 
            conn.Open();


            String strSQL = "SELECT * FROM CUSTOMER ORDER BY CUSTOMERID";
            OracleCommand cmd = new OracleCommand(strSQL, conn);

            //cmd.CommandType = CommandType.Text;
            OracleDataReader dr = cmd.ExecuteReader();

            OracleDataAdapter da = new OracleDataAdapter(cmd);

            da.SelectCommand = cmd;

            da.Fill(DS, "ss");

            conn.Close();

            return DS;
        }

        public static DataSet getCustomersDate(DataSet DS)
        {
            OracleConnection conn = new OracleConnection(DataConnection.oracConn);

            //connection name 
            conn.Open();


            String strSQL = "SELECT CUSTOMERID,FIRSTNAME,LASTNAME,DOB FROM CUSTOMER  " +
                "ORDER BY CUSTOMERID";
            OracleCommand cmd = new OracleCommand(strSQL, conn);

            //cmd.CommandType = CommandType.Text;
            OracleDataReader dr = cmd.ExecuteReader();

            OracleDataAdapter da = new OracleDataAdapter(cmd);

            da.SelectCommand = cmd;

            da.Fill(DS, "ss");

            conn.Close();

            return DS;
        }

        public static DataSet getView(DataSet DS)
        {
            OracleConnection conn = new OracleConnection(DataConnection.oracConn);

            //connection name 
            conn.Open();


            String strSQL = "SELECT * FROM VW_CARD WHERE ACCOUNTBALANCE > = 5000";
            OracleCommand cmd = new OracleCommand(strSQL, conn);

            //cmd.CommandType = CommandType.Text;
            OracleDataReader dr = cmd.ExecuteReader();

            OracleDataAdapter da = new OracleDataAdapter(cmd);

            da.SelectCommand = cmd;

            da.Fill(DS, "ss");

            conn.Close();

            return DS;
        }

        public void getCust(int CustID)
        {
            OracleConnection myConn = new OracleConnection(DataConnection.oracConn);
            myConn.Open();

            //Define SQL query to get MAX stock_No used
            String strSQL = "SELECT * FROM CUSTOMER WHERE CUSTOMERID = " + CustID;

            OracleCommand cmd = new OracleCommand(strSQL, myConn);

            //execute the SQL query
            OracleDataReader dr = cmd.ExecuteReader();

            //If first stockNo, assign value 1, otherwise add 1 to MAX value
            if (dr.Read())
            {
                setCustID(dr.GetInt32(0));
                setFirstName(dr.GetString(1));
                setLastName(dr.GetString(2));
                setGender(dr.GetString(3));
                setAddress(dr.GetString(4));
                setPhoneNo(dr.GetString(5));
                setDateTime(dr.GetDateTime(6));
       
            }
            myConn.Close();
        }

        public void regCustomer()
        {
            try
            {
                //connect to database
                OracleConnection myConn = new OracleConnection(DataConnection.oracConn);
                myConn.Open();

                //Define SQL query to INSERT Supplier record
                String strSQL = "INSERT INTO CUSTOMER VALUES(" + this.custID.ToString() +
                    ",'" + this.firstName + "','" + this.lastName + "','" +
                    this.gender + "','" + this.address + "'," + this.phoneNo + ",'" + String.Format("{0:dd-MMM-yy}", this.dob)
                    + "')";

                //Execute the command
                OracleCommand cmd = new OracleCommand(strSQL, myConn);
                cmd.ExecuteNonQuery();

                //close DB connection
                myConn.Close();

            }
            catch (Exception ex)
            {
                throw (ex);
            }
        }

        public void updCustomer()
        {
            //connect to database
            OracleConnection myConn = new OracleConnection(DataConnection.oracConn);
            myConn.Open();

           
            String strSQL = "UPDATE CUSTOMER SET FIRSTNAME = '" + this.firstName + "', LASTNAME = '" + this.lastName +
                "', GENDER = '" + this.gender + "', ADDRESS = '" + this.address + "', PHONENUMBER = '" + this.phoneNo  +
                "', DOB = '" + String.Format("{0:dd-MMM-yy}", this.dob) + "' WHERE CUSTOMERID = " + this.custID;

            //Execute the command
            OracleCommand cmd = new OracleCommand(strSQL, myConn);
            cmd.ExecuteNonQuery();

            //close DB connection
            myConn.Close();

        }

        public static void removeCust(int custID)
        {
            OracleConnection myConn = new OracleConnection(DataConnection.oracConn);
            myConn.Open();

            String strSQL = "DELETE FROM CUSTOMER WHERE CUSTOMERID = " + custID;

            OracleCommand cmd = new OracleCommand(strSQL, myConn);
            cmd.ExecuteNonQuery();


            myConn.Close();
        }

        public static int nextCust()
        {
            int nextCustomer;
     
            OracleConnection myConn = new OracleConnection(DataConnection.oracConn);
            myConn.Open();

            String strSQL = "SELECT MAX(CUSTOMERID) FROM CUSTOMER";

            OracleCommand cmd = new OracleCommand(strSQL, myConn);

            //execute the SQL query and put result in OracleDataReader object
            OracleDataReader dr = cmd.ExecuteReader();

            //read the first (only) value returned by query
            //If first suppID, assign value 1, otherwise add 1 to MAX value
            dr.Read();

            if (dr.IsDBNull(0))
                nextCustomer = 132;
            else
                nextCustomer = Convert.ToInt16(dr.GetValue(0)) + 1;


            myConn.Close();


            //return next StockNo
            return nextCustomer;

        }



    }
}
