using FISCA.DSAUtil;
using FISCA.UDT;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace K12EmergencyContact.DAO
{
    public class UDTTransfer
    {
        /// <summary>
        /// 建立使用到的 UDT Table
        /// </summary>
        public static void CreateUDTTable()
        {
            FISCA.UDT.SchemaManager Manager = new SchemaManager(new DSConnection(FISCA.Authentication.DSAServices.DefaultDataSource));
            Manager.SyncSchema(new udt_K12EmergencyContact());
        }

        /// <summary>
        /// 取得單一學生緊急聯絡人
        /// </summary>
        /// <param name="StudentID"></param>
        /// <returns></returns>
        public static udt_K12EmergencyContact GetStudentK12EmergencyContactByStudentID(string StudentID)
        {
            udt_K12EmergencyContact value = null;
            AccessHelper accHelper = new AccessHelper();
            string qry = " ref_student_id=" + StudentID;
            List<udt_K12EmergencyContact> dataList = accHelper.Select<udt_K12EmergencyContact>(qry);
            if (dataList.Count > 0)
                value = dataList[0];

            if (value == null)
                value = new udt_K12EmergencyContact();

            return value;
        }

        /// <summary>
        /// 透過學生IDList取得緊急聯絡人資料
        /// </summary>
        /// <param name="StudentIDList"></param>
        /// <returns></returns>
        public static List<udt_K12EmergencyContact> GetStudentK12EmergencyContactByStudentIDList(List<string> StudentIDList)
        {
            List<udt_K12EmergencyContact> value = new List<udt_K12EmergencyContact>();
            if(StudentIDList.Count>0)
            {
                AccessHelper accHelper = new AccessHelper();
                string qry = " ref_student_id in(" + string.Join(",", StudentIDList.ToArray()) + ")";
                value = accHelper.Select<udt_K12EmergencyContact>(qry);
            }           

            return value;
        }
    }
}
