
namespace MSHB.TsetmcReader.WinApp
{
    partial class frmMain
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmMain));
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.tsmBase = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmOpenBrowser = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmExit = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmTime = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmMinimize = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmActiveListener = new System.Windows.Forms.ToolStripMenuItem();
            this.tmClock = new System.Windows.Forms.Timer(this.components);
            this.notifyIconManage = new System.Windows.Forms.NotifyIcon(this.components);
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.lbStatus = new System.Windows.Forms.ToolStripStatusLabel();
            this.tmServiceChecker = new System.Windows.Forms.Timer(this.components);
            this.menuStrip1.SuspendLayout();
            this.statusStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsmBase,
            this.tsmExit,
            this.tsmTime,
            this.tsmMinimize,
            this.tsmActiveListener});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(860, 24);
            this.menuStrip1.TabIndex = 0;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // tsmBase
            // 
            this.tsmBase.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsmOpenBrowser});
            this.tsmBase.Name = "tsmBase";
            this.tsmBase.Size = new System.Drawing.Size(80, 20);
            this.tsmBase.Text = "اطلاعات پایه";
            // 
            // tsmOpenBrowser
            // 
            this.tsmOpenBrowser.Name = "tsmOpenBrowser";
            this.tsmOpenBrowser.Size = new System.Drawing.Size(144, 22);
            this.tsmOpenBrowser.Text = "بازکردن مرورگر";
            this.tsmOpenBrowser.Click += new System.EventHandler(this.tsmOpenBrowser_Click);
            // 
            // tsmExit
            // 
            this.tsmExit.Name = "tsmExit";
            this.tsmExit.Size = new System.Drawing.Size(44, 20);
            this.tsmExit.Text = "خروج";
            this.tsmExit.Click += new System.EventHandler(this.tsmExit_Click);
            // 
            // tsmTime
            // 
            this.tsmTime.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
            this.tsmTime.Name = "tsmTime";
            this.tsmTime.Size = new System.Drawing.Size(79, 20);
            this.tsmTime.Text = "زمان سیستم";
            // 
            // tsmMinimize
            // 
            this.tsmMinimize.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
            this.tsmMinimize.Name = "tsmMinimize";
            this.tsmMinimize.Size = new System.Drawing.Size(73, 20);
            this.tsmMinimize.Text = "پنهان سازی";
            this.tsmMinimize.Click += new System.EventHandler(this.tsmMinimize_Click);
            // 
            // tsmActiveListener
            // 
            this.tsmActiveListener.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
            this.tsmActiveListener.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.tsmActiveListener.ForeColor = System.Drawing.Color.Red;
            this.tsmActiveListener.Name = "tsmActiveListener";
            this.tsmActiveListener.Size = new System.Drawing.Size(172, 20);
            this.tsmActiveListener.Tag = "0";
            this.tsmActiveListener.Text = "وضعیت دریافت کننده: غیرفعال";
            this.tsmActiveListener.Click += new System.EventHandler(this.tsmActiveListener_Click);
            // 
            // tmClock
            // 
            this.tmClock.Enabled = true;
            this.tmClock.Interval = 1000;
            this.tmClock.Tick += new System.EventHandler(this.tmClock_Tick);
            // 
            // notifyIconManage
            // 
            this.notifyIconManage.Icon = ((System.Drawing.Icon)(resources.GetObject("notifyIconManage.Icon")));
            this.notifyIconManage.Text = "مشاهده گر اطلاعات آنلاین بورس";
            this.notifyIconManage.Visible = true;
            this.notifyIconManage.Click += new System.EventHandler(this.notifyIconManage_Click);
            // 
            // statusStrip1
            // 
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.lbStatus});
            this.statusStrip1.Location = new System.Drawing.Point(0, 528);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.statusStrip1.Size = new System.Drawing.Size(860, 22);
            this.statusStrip1.TabIndex = 1;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // lbStatus
            // 
            this.lbStatus.Name = "lbStatus";
            this.lbStatus.Size = new System.Drawing.Size(48, 17);
            this.lbStatus.Text = "Ready...";
            // 
            // tmServiceChecker
            // 
            this.tmServiceChecker.Enabled = true;
            this.tmServiceChecker.Interval = 60000;
            this.tmServiceChecker.Tick += new System.EventHandler(this.tmServiceChecker_Tick);
            // 
            // frmMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(10F, 24F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(860, 550);
            this.Controls.Add(this.statusStrip1);
            this.Controls.Add(this.menuStrip1);
            this.Font = new System.Drawing.Font("B Nazanin", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(178)));
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MainMenuStrip = this.menuStrip1;
            this.Margin = new System.Windows.Forms.Padding(5, 6, 5, 6);
            this.Name = "frmMain";
            this.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "صفحه اصلی";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.frmMain_FormClosing);
            this.Load += new System.EventHandler(this.frmMain_Load);
            this.Resize += new System.EventHandler(this.frmMain_Resize);
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem tsmBase;
        private System.Windows.Forms.ToolStripMenuItem tsmExit;
        private System.Windows.Forms.ToolStripMenuItem tsmTime;
        private System.Windows.Forms.Timer tmClock;
        private System.Windows.Forms.ToolStripMenuItem tsmMinimize;
        private System.Windows.Forms.NotifyIcon notifyIconManage;
        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.ToolStripStatusLabel lbStatus;
        private System.Windows.Forms.ToolStripMenuItem tsmActiveListener;
        private System.Windows.Forms.ToolStripMenuItem tsmOpenBrowser;
        private System.Windows.Forms.Timer tmServiceChecker;
    }
}

