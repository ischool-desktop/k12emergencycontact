using FISCA;
using FISCA.Permission;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;

namespace K12EmergencyContact
{
    public class Program
    {
        static BackgroundWorker _bgLLoadUDT = new BackgroundWorker();
        [MainMethod()]
        public static void Main()
        {
            _bgLLoadUDT.DoWork += _bgLLoadUDT_DoWork;
            _bgLLoadUDT.RunWorkerCompleted += _bgLLoadUDT_RunWorkerCompleted;
            _bgLLoadUDT.RunWorkerAsync();
        }

        static void _bgLLoadUDT_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {

            Catalog catalog01 = RoleAclSource.Instance["學生"]["資料項目"];
            catalog01.Add(new DetailItemFeature(typeof(DetailContent.EmergencyDetailContent)));

            FeatureAce UserPermission = FISCA.Permission.UserAcl.Current["K12EmergencyContact.DetailContent.EmergencyDetailContent"];
        }

        static void _bgLLoadUDT_DoWork(object sender, DoWorkEventArgs e)
        {
            // 載入時自動檢查與建立UDT
            DAO.UDTTransfer.CreateUDTTable();
        }
    }
}
