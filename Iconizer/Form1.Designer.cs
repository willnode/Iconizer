namespace Iconizer
{
    partial class Form1
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            this._options = new System.Windows.Forms.CheckedListBox();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this._clear = new System.Windows.Forms.Button();
            this._add = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this._saveQuick = new System.Windows.Forms.Button();
            this._saveFull = new System.Windows.Forms.Button();
            this._view = new System.Windows.Forms.ListView();
            this._openDiag = new System.Windows.Forms.OpenFileDialog();
            this._saveDiag = new System.Windows.Forms.FolderBrowserDialog();
            this.tableLayoutPanel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // _options
            // 
            this._options.ColumnWidth = 90;
            this._options.Dock = System.Windows.Forms.DockStyle.Fill;
            this._options.FormattingEnabled = true;
            this._options.Items.AddRange(new object[] {
            "8px",
            "16px",
            "32px",
            "48px",
            "64px",
            "128px",
            "256px",
            "Keep Crisp",
            "Keep Ratio"});
            this._options.Location = new System.Drawing.Point(165, 3);
            this._options.MultiColumn = true;
            this._options.Name = "_options";
            this.tableLayoutPanel1.SetRowSpan(this._options, 2);
            this._options.Size = new System.Drawing.Size(303, 54);
            this._options.TabIndex = 0;
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 4;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 162F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 110F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 103F));
            this.tableLayoutPanel1.Controls.Add(this._clear, 2, 1);
            this.tableLayoutPanel1.Controls.Add(this._add, 2, 0);
            this.tableLayoutPanel1.Controls.Add(this.label1, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this._options, 1, 0);
            this.tableLayoutPanel1.Controls.Add(this._saveQuick, 3, 0);
            this.tableLayoutPanel1.Controls.Add(this._saveFull, 3, 1);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 2;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(684, 60);
            this.tableLayoutPanel1.TabIndex = 1;
            // 
            // _clear
            // 
            this._clear.Dock = System.Windows.Forms.DockStyle.Fill;
            this._clear.Location = new System.Drawing.Point(474, 33);
            this._clear.Name = "_clear";
            this._clear.Size = new System.Drawing.Size(104, 24);
            this._clear.TabIndex = 3;
            this._clear.Text = "Clear";
            this._clear.UseVisualStyleBackColor = true;
            this._clear.Click += new System.EventHandler(this._clear_Click);
            // 
            // _add
            // 
            this._add.Dock = System.Windows.Forms.DockStyle.Fill;
            this._add.Location = new System.Drawing.Point(474, 3);
            this._add.Name = "_add";
            this._add.Size = new System.Drawing.Size(104, 24);
            this._add.TabIndex = 4;
            this._add.Text = "Add ...";
            this._add.UseVisualStyleBackColor = true;
            this._add.Click += new System.EventHandler(this._add_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Cursor = System.Windows.Forms.Cursors.Help;
            this.label1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 24F);
            this.label1.Location = new System.Drawing.Point(3, 0);
            this.label1.Name = "label1";
            this.tableLayoutPanel1.SetRowSpan(this.label1, 2);
            this.label1.Size = new System.Drawing.Size(156, 60);
            this.label1.TabIndex = 0;
            this.label1.Text = "Iconizer";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.label1.Click += new System.EventHandler(this.label1_Click);
            // 
            // _saveQuick
            // 
            this._saveQuick.Dock = System.Windows.Forms.DockStyle.Fill;
            this._saveQuick.Location = new System.Drawing.Point(584, 3);
            this._saveQuick.Name = "_saveQuick";
            this._saveQuick.Size = new System.Drawing.Size(97, 24);
            this._saveQuick.TabIndex = 1;
            this._saveQuick.Text = "Quick Save";
            this._saveQuick.UseVisualStyleBackColor = true;
            this._saveQuick.Click += new System.EventHandler(this._saveQuick_Click);
            // 
            // _saveFull
            // 
            this._saveFull.Dock = System.Windows.Forms.DockStyle.Fill;
            this._saveFull.Location = new System.Drawing.Point(584, 33);
            this._saveFull.Name = "_saveFull";
            this._saveFull.Size = new System.Drawing.Size(97, 24);
            this._saveFull.TabIndex = 2;
            this._saveFull.Text = "Save ...";
            this._saveFull.UseVisualStyleBackColor = true;
            this._saveFull.Click += new System.EventHandler(this._saveFull_Click);
            // 
            // _view
            // 
            this._view.AllowDrop = true;
            this._view.Dock = System.Windows.Forms.DockStyle.Fill;
            this._view.FullRowSelect = true;
            this._view.Location = new System.Drawing.Point(0, 60);
            this._view.Name = "_view";
            this._view.Size = new System.Drawing.Size(684, 315);
            this._view.TabIndex = 2;
            this._view.UseCompatibleStateImageBehavior = false;
            this._view.ItemDrag += new System.Windows.Forms.ItemDragEventHandler(this._view_ItemDrag);
            this._view.DragDrop += new System.Windows.Forms.DragEventHandler(this._view_DragDrop);
            this._view.DragEnter += new System.Windows.Forms.DragEventHandler(this._view_DragEnter);
            this._view.KeyDown += new System.Windows.Forms.KeyEventHandler(this._view_KeyDown);
            // 
            // _openDiag
            // 
            this._openDiag.Filter = "Image or Binary Files|*.png;*.jpg;*.jpeg;*.bmp;*.exe;*.dll";
            this._openDiag.Multiselect = true;
            this._openDiag.RestoreDirectory = true;
            // 
            // _saveDiag
            // 
            this._saveDiag.Description = "Select the folder where icons will be saved";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(684, 375);
            this.Controls.Add(this._view);
            this.Controls.Add(this.tableLayoutPanel1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MinimumSize = new System.Drawing.Size(700, 100);
            this.Name = "Form1";
            this.Text = "Iconizer";
            this.Activated += new System.EventHandler(this.Form1_Activated);
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.CheckedListBox _options;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button _saveQuick;
        private System.Windows.Forms.Button _saveFull;
        private System.Windows.Forms.ListView _view;
        private System.Windows.Forms.Button _clear;
        private System.Windows.Forms.Button _add;
        private System.Windows.Forms.OpenFileDialog _openDiag;
        private System.Windows.Forms.FolderBrowserDialog _saveDiag;
    }
}

