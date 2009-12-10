namespace Wigraf.Views
{
    partial class PreviewView
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(PreviewView));
            this.picture = new System.Windows.Forms.PictureBox();
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.mnuPreviewSave2 = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.toolStripDropDownButton1 = new System.Windows.Forms.ToolStripDropDownButton();
            this.mnuSizeNormal = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuSizeZoom = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuPreviewSave = new System.Windows.Forms.ToolStripButton();
            ((System.ComponentModel.ISupportInitialize)(this.picture)).BeginInit();
            this.toolStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // picture
            // 
            resources.ApplyResources(this.picture, "picture");
            this.picture.Name = "picture";
            this.picture.TabStop = false;
            // 
            // toolStrip1
            // 
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mnuPreviewSave2,
            this.toolStripSeparator1,
            this.toolStripDropDownButton1});
            resources.ApplyResources(this.toolStrip1, "toolStrip1");
            this.toolStrip1.Name = "toolStrip1";
            // 
            // mnuPreviewSave2
            // 
            this.mnuPreviewSave2.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            resources.ApplyResources(this.mnuPreviewSave2, "mnuPreviewSave2");
            this.mnuPreviewSave2.Name = "mnuPreviewSave2";
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            resources.ApplyResources(this.toolStripSeparator1, "toolStripSeparator1");
            // 
            // toolStripDropDownButton1
            // 
            this.toolStripDropDownButton1.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripDropDownButton1.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mnuSizeNormal,
            this.mnuSizeZoom});
            resources.ApplyResources(this.toolStripDropDownButton1, "toolStripDropDownButton1");
            this.toolStripDropDownButton1.Name = "toolStripDropDownButton1";
            // 
            // mnuSizeNormal
            // 
            this.mnuSizeNormal.Name = "mnuSizeNormal";
            resources.ApplyResources(this.mnuSizeNormal, "mnuSizeNormal");
            // 
            // mnuSizeZoom
            // 
            this.mnuSizeZoom.Name = "mnuSizeZoom";
            resources.ApplyResources(this.mnuSizeZoom, "mnuSizeZoom");
            // 
            // mnuPreviewSave
            // 
            this.mnuPreviewSave.Name = "mnuPreviewSave";
            resources.ApplyResources(this.mnuPreviewSave, "mnuPreviewSave");
            // 
            // PreviewView
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.toolStrip1);
            this.Controls.Add(this.picture);
            this.Name = "PreviewView";
            ((System.ComponentModel.ISupportInitialize)(this.picture)).EndInit();
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.PictureBox picture;
        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.ToolStripButton mnuPreviewSave;
        private System.Windows.Forms.ToolStripButton mnuPreviewSave2;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripDropDownButton toolStripDropDownButton1;
        private System.Windows.Forms.ToolStripMenuItem mnuSizeNormal;
        private System.Windows.Forms.ToolStripMenuItem mnuSizeZoom;
    }
}