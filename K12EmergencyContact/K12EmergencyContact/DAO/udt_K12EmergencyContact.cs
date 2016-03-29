using FISCA.UDT;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace K12EmergencyContact.DAO
{

    /// <summary>
    /// K12用緊急聯絡人
    /// </summary>
    [TableName("k12_emergency_contact")]
    public class udt_K12EmergencyContact:ActiveRecord
    {
        /// <summary>
        /// 學生系統編號
        /// </summary>
        [Field(Field = "ref_student_id", Indexed = false)]
        public int RefStudentID { get; set; }

        /// <summary>
        /// 緊急聯絡人名稱
        /// </summary>
        [Field(Field = "contact_name", Indexed = false)]
        public string ContactName { get; set; }

        /// <summary>
        /// 緊急聯絡人電話
        /// </summary>
        [Field(Field = "contact_phone", Indexed = false)]
        public string ContactPhone { get; set; }
    }
}
