using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Web.Services;

namespace WebApplication1
{
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    [System.Web.Script.Services.ScriptService]

    public class WebService1 : System.Web.Services.WebService
    {
        public class contact
        {
            public string fullname;
            public string address;
            public string[] phone;
        }

        [WebMethod]
        public List<contact> GetNewData(int lastId)
        {
            int curId = lastId; int countReaderRrow = 1;

            List<contact> contactList = new List<contact>();
            List<string> Fullname = new List<string>();
            List<string> Address = new List<string>();
            List<List<string>> listlistphone = new List<List<string>>();

            SqlDataReader dr;

            using (SqlConnection con = new SqlConnection(@"Data Source=home-pc\SQLExpress;Initial Catalog=Database;Integrated Security=True"))
            {
                using (SqlCommand cmd = new SqlCommand())
                {
                    cmd.CommandText = "GetNewData";

                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Connection = con;

                    cmd.Parameters.AddWithValue("@lastId", lastId);

                    con.Open();

                    dr = cmd.ExecuteReader(CommandBehavior.CloseConnection);

                    List<string> phonelist = new List<string>();

                    if (dr.HasRows)
                    {
                        while (dr.Read())
                        {
                            if (countReaderRrow == 1)
                            {
                                curId = (int)dr["Id"];

                                Fullname.Add(dr["FullName"] is DBNull ? null : dr["FullName"].ToString());

                                Address.Add(dr["Address"] is DBNull ? null : dr["Address"].ToString());

                                phonelist.Add(dr["PhoneNumber"] is DBNull ? null : dr["PhoneNumber"].ToString());
                                countReaderRrow++;
                            }
                            else
                            {
                                if ((int)dr["Id"] == curId)
                                {
                                    phonelist.Add(dr["PhoneNumber"] is DBNull ? null : dr["PhoneNumber"].ToString());
                                }
                                else
                                {
                                    curId = (int)dr["Id"];

                                    Fullname.Add(dr["FullName"] is DBNull ? null : dr["FullName"].ToString());

                                    Address.Add(dr["Address"] is DBNull ? null : dr["Address"].ToString());

                                    List<string> temp = new List<string>();

                                    foreach (string number in phonelist)
                                    {
                                        temp.Add(number);
                                    }

                                    listlistphone.Add(temp);

                                    phonelist.Clear();

                                    phonelist.Add(dr["PhoneNumber"] is DBNull ? null : dr["PhoneNumber"].ToString());
                                }
                            }
                        }                     

                        List<string> temp1 = new List<string>();

                        foreach (string number in phonelist)
                        {
                            temp1.Add(number);
                        }

                        listlistphone.Add(temp1);

                        phonelist.Clear();

                    }
                }
            }

            for (int i = 0; i < Fullname.Count; i++)
            {
                List<string> phonelist = new List<string>();
                phonelist = listlistphone[i];

                contactList.Add(new contact
                {
                    fullname = Fullname[i],
                    address = Address[i],
                    phone = phonelist.ToArray()
                });
            }

            contactList.Add(new contact
            {
                fullname = curId.ToString(),
                address = "",
                phone = null
            });

            return contactList;
        }
    }
}

