using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Campus.Windows;
using K12EmergencyContact.DAO;

namespace K12EmergencyContact.DetailContent
{
    [FISCA.Permission.FeatureCode("K12EmergencyContact.DetailContent.EmergencyDetailContent", "緊急聯絡人")]
    public partial class EmergencyDetailContent : FISCA.Presentation.DetailContent
    {
        BackgroundWorker _bgWorker;        
        private ChangeListener _ChangeListener;
        K12.Data.StudentRecord _StudRec;
        bool _isBusy = false;
        ErrorProvider _errorP;
        udt_K12EmergencyContact _K12EmergencyContactRecord=null;

        public EmergencyDetailContent()
        {
            InitializeComponent();
            this.Group = "緊急聯絡人";
            _bgWorker = new BackgroundWorker();
            
            _bgWorker.DoWork += _bgWorker_DoWork;
            _bgWorker.RunWorkerCompleted += _bgWorker_RunWorkerCompleted;
            _ChangeListener = new ChangeListener();
            _errorP = new ErrorProvider();
            _ChangeListener.StatusChanged += _ChangeListener_StatusChanged;
            _ChangeListener.Add(new TextBoxSource(txtName));
            _ChangeListener.Add(new TextBoxSource(txtPhone));
            
        }

        void _ChangeListener_StatusChanged(object sender, ChangeEventArgs e)
        {
            this.CancelButtonVisible = (e.Status == ValueStatus.Dirty);
            this.SaveButtonVisible = (e.Status == ValueStatus.Dirty);
        }

        void _bgWorker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            if (_isBusy)
            {
                _isBusy = false;
                _bgWorker.RunWorkerAsync();
                return;
            }
            LoadData();

        }

        void _bgWorker_DoWork(object sender, DoWorkEventArgs e)
        {
            _K12EmergencyContactRecord = UDTTransfer.GetStudentK12EmergencyContactByStudentID(PrimaryKey);
        }

        private void LoadData()
        {
            _ChangeListener.SuspendListen();
            txtName.Text = "";
            txtPhone.Text = "";
            if(_K12EmergencyContactRecord !=null)
            {
                txtName.Text = _K12EmergencyContactRecord.ContactName;
                txtPhone.Text = _K12EmergencyContactRecord.ContactPhone;
            }
            _ChangeListener.Reset();
            _ChangeListener.ResumeListen();
            this.Loading = false;
        }

        protected override void OnPrimaryKeyChanged(EventArgs e)
        {
            this.Loading = true;
            this.CancelButtonVisible = false;
            this.SaveButtonVisible = false;
            _BGRun();
        }

        private void _BGRun()
        {
            if (_bgWorker.IsBusy)
                _isBusy = true;
            else
                _bgWorker.RunWorkerAsync();
        }

        protected override void OnCancelButtonClick(EventArgs e)
        {
            this.CancelButtonVisible = false;
            this.SaveButtonVisible = false;
            LoadData();
        }


        protected override void OnSaveButtonClick(EventArgs e)
        {
            if(_K12EmergencyContactRecord==null)
            {
                _K12EmergencyContactRecord = new udt_K12EmergencyContact();
                _K12EmergencyContactRecord.RefStudentID = int.Parse(PrimaryKey);
            }
            _K12EmergencyContactRecord.ContactName = txtName.Text;
            _K12EmergencyContactRecord.ContactPhone = txtPhone.Text;
            _K12EmergencyContactRecord.Save();
            this.CancelButtonVisible = false;
            this.SaveButtonVisible = false;            
            _ChangeListener.Reset();
            _ChangeListener.ResumeListen();
        }

        private void txtName_TextChanged(object sender, EventArgs e)
        {
            _errorP.SetError(txtName, "");
        }

        private void txtPhone_TextChanged(object sender, EventArgs e)
        {
            _errorP.SetError(txtPhone, "");
        }

    }
}
