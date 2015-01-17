namespace Bau.Controls.ImageControls.Print
{
	partial class ImagePrinterPreview
	{
		/// <summary>
		/// Variable del diseñador requerida.
		/// </summary>
		private System.ComponentModel.IContainer components = null;

		/// <summary>
		/// Limpiar los recursos que se estén utilizando.
		/// </summary>
		/// <param name="disposing">true si los recursos administrados se deben eliminar; false en caso contrario, false.</param>
		protected override void Dispose(bool disposing)
		{
			if (disposing && (components != null))
			{
				components.Dispose();
			}
			base.Dispose(disposing);
		}

		#region Código generado por el Diseñador de componentes

		/// <summary>
		/// Método necesario para admitir el Diseñador. No se puede modificar 
		/// el contenido del método con el editor de código.
		/// </summary>
		private void InitializeComponent()
		{
			this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
			this.prvControl = new System.Windows.Forms.PrintPreviewControl();
			this.tlbMain = new System.Windows.Forms.ToolStrip();
			this.cmdPageSetup = new System.Windows.Forms.ToolStripButton();
			this.cmdPrint = new System.Windows.Forms.ToolStripButton();
			this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
			this.toolStripLabel1 = new System.Windows.Forms.ToolStripLabel();
			this.txtPages = new System.Windows.Forms.ToolStripTextBox();
			this.toolStripSeparator3 = new System.Windows.Forms.ToolStripSeparator();
			this.cmdPageZoom = new System.Windows.Forms.ToolStripSplitButton();
			this.mnuZoom200 = new System.Windows.Forms.ToolStripMenuItem();
			this.mnuZoom100 = new System.Windows.Forms.ToolStripMenuItem();
			this.mnuZoom75 = new System.Windows.Forms.ToolStripMenuItem();
			this.mnuZoom50 = new System.Windows.Forms.ToolStripMenuItem();
			this.mnuZoom25 = new System.Windows.Forms.ToolStripMenuItem();
			this.toolStripMenuItem1 = new System.Windows.Forms.ToolStripSeparator();
			this.mnuZoomAuto = new System.Windows.Forms.ToolStripMenuItem();
			this.cmdPageLeft = new System.Windows.Forms.ToolStripButton();
			this.lblPage = new System.Windows.Forms.ToolStripLabel();
			this.cmdPageRight = new System.Windows.Forms.ToolStripButton();
			this.cmdRefresh = new System.Windows.Forms.ToolStripButton();
			this.tableLayoutPanel1.SuspendLayout();
			this.tlbMain.SuspendLayout();
			this.SuspendLayout();
			// 
			// tableLayoutPanel1
			// 
			this.tableLayoutPanel1.ColumnCount = 1;
			this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
			this.tableLayoutPanel1.Controls.Add(this.prvControl, 0, 1);
			this.tableLayoutPanel1.Controls.Add(this.tlbMain, 0, 0);
			this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
			this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
			this.tableLayoutPanel1.Name = "tableLayoutPanel1";
			this.tableLayoutPanel1.RowCount = 2;
			this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 26F));
			this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
			this.tableLayoutPanel1.Size = new System.Drawing.Size(622, 544);
			this.tableLayoutPanel1.TabIndex = 0;
			// 
			// prvControl
			// 
			this.prvControl.AutoZoom = false;
			this.prvControl.Dock = System.Windows.Forms.DockStyle.Fill;
			this.prvControl.Location = new System.Drawing.Point(3, 29);
			this.prvControl.Name = "prvControl";
			this.prvControl.Size = new System.Drawing.Size(616, 512);
			this.prvControl.TabIndex = 1;
			this.prvControl.Zoom = 0.45765611633875108;
			// 
			// tlbMain
			// 
			this.tlbMain.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
			this.tlbMain.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.cmdPageSetup,
            this.cmdPrint,
            this.toolStripSeparator1,
            this.toolStripLabel1,
            this.txtPages,
            this.toolStripSeparator3,
            this.cmdPageZoom,
            this.cmdPageLeft,
            this.lblPage,
            this.cmdPageRight,
            this.cmdRefresh});
			this.tlbMain.Location = new System.Drawing.Point(0, 0);
			this.tlbMain.Name = "tlbMain";
			this.tlbMain.Size = new System.Drawing.Size(622, 25);
			this.tlbMain.TabIndex = 0;
			this.tlbMain.Text = "toolStrip1";
			// 
			// cmdPageSetup
			// 
			this.cmdPageSetup.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
			this.cmdPageSetup.Image = global::Bau.Controls.ImageControls.Properties.Resources.PageSetup;
			this.cmdPageSetup.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.cmdPageSetup.Name = "cmdPageSetup";
			this.cmdPageSetup.Size = new System.Drawing.Size(23, 22);
			this.cmdPageSetup.Text = "Configuración página";
			this.cmdPageSetup.Click += new System.EventHandler(this.cmdPageSetup_Click);
			// 
			// cmdPrint
			// 
			this.cmdPrint.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
			this.cmdPrint.Image = global::Bau.Controls.ImageControls.Properties.Resources.Printer;
			this.cmdPrint.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.cmdPrint.Name = "cmdPrint";
			this.cmdPrint.Size = new System.Drawing.Size(23, 22);
			this.cmdPrint.Text = "Imprimir";
			this.cmdPrint.Click += new System.EventHandler(this.cmdPrint_Click);
			// 
			// toolStripSeparator1
			// 
			this.toolStripSeparator1.Name = "toolStripSeparator1";
			this.toolStripSeparator1.Size = new System.Drawing.Size(6, 25);
			// 
			// toolStripLabel1
			// 
			this.toolStripLabel1.Name = "toolStripLabel1";
			this.toolStripLabel1.Size = new System.Drawing.Size(48, 22);
			this.toolStripLabel1.Text = "Páginas:";
			// 
			// txtPages
			// 
			this.txtPages.MaxLength = 1;
			this.txtPages.Name = "txtPages";
			this.txtPages.Size = new System.Drawing.Size(20, 25);
			this.txtPages.Text = "1";
			this.txtPages.TextBoxTextAlign = System.Windows.Forms.HorizontalAlignment.Right;
			this.txtPages.TextChanged += new System.EventHandler(this.txtPages_TextChanged);
			// 
			// toolStripSeparator3
			// 
			this.toolStripSeparator3.Name = "toolStripSeparator3";
			this.toolStripSeparator3.Size = new System.Drawing.Size(6, 25);
			// 
			// cmdPageZoom
			// 
			this.cmdPageZoom.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
			this.cmdPageZoom.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mnuZoom200,
            this.mnuZoom100,
            this.mnuZoom75,
            this.mnuZoom50,
            this.mnuZoom25,
            this.toolStripMenuItem1,
            this.mnuZoomAuto});
			this.cmdPageZoom.Image = global::Bau.Controls.ImageControls.Properties.Resources.page_white_magnify;
			this.cmdPageZoom.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.cmdPageZoom.Name = "cmdPageZoom";
			this.cmdPageZoom.Size = new System.Drawing.Size(32, 22);
			this.cmdPageZoom.Text = "Zoom";
			// 
			// mnuZoom200
			// 
			this.mnuZoom200.Name = "mnuZoom200";
			this.mnuZoom200.Size = new System.Drawing.Size(152, 22);
			this.mnuZoom200.Text = "200%";
			this.mnuZoom200.Click += new System.EventHandler(this.mnuZoom200_Click);
			// 
			// mnuZoom100
			// 
			this.mnuZoom100.Name = "mnuZoom100";
			this.mnuZoom100.Size = new System.Drawing.Size(152, 22);
			this.mnuZoom100.Text = "100%";
			this.mnuZoom100.Click += new System.EventHandler(this.mnuZoom100_Click);
			// 
			// mnuZoom75
			// 
			this.mnuZoom75.Name = "mnuZoom75";
			this.mnuZoom75.Size = new System.Drawing.Size(152, 22);
			this.mnuZoom75.Text = "75%";
			this.mnuZoom75.Click += new System.EventHandler(this.mnuZoom75_Click);
			// 
			// mnuZoom50
			// 
			this.mnuZoom50.Name = "mnuZoom50";
			this.mnuZoom50.Size = new System.Drawing.Size(152, 22);
			this.mnuZoom50.Text = "50%";
			this.mnuZoom50.Click += new System.EventHandler(this.mnuZoom50_Click);
			// 
			// mnuZoom25
			// 
			this.mnuZoom25.Name = "mnuZoom25";
			this.mnuZoom25.Size = new System.Drawing.Size(152, 22);
			this.mnuZoom25.Text = "25%";
			this.mnuZoom25.Click += new System.EventHandler(this.mnuZoom25_Click);
			// 
			// toolStripMenuItem1
			// 
			this.toolStripMenuItem1.Name = "toolStripMenuItem1";
			this.toolStripMenuItem1.Size = new System.Drawing.Size(149, 6);
			// 
			// mnuZoomAuto
			// 
			this.mnuZoomAuto.Name = "mnuZoomAuto";
			this.mnuZoomAuto.Size = new System.Drawing.Size(152, 22);
			this.mnuZoomAuto.Text = "&Automático";
			this.mnuZoomAuto.Click += new System.EventHandler(this.mnuZoomAuto_Click);
			// 
			// cmdPageLeft
			// 
			this.cmdPageLeft.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
			this.cmdPageLeft.Image = global::Bau.Controls.ImageControls.Properties.Resources.ArrowLeft;
			this.cmdPageLeft.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.cmdPageLeft.Name = "cmdPageLeft";
			this.cmdPageLeft.Size = new System.Drawing.Size(23, 22);
			this.cmdPageLeft.Text = "Página anterior";
			this.cmdPageLeft.Click += new System.EventHandler(this.cmdPageLeft_Click);
			// 
			// lblPage
			// 
			this.lblPage.Name = "lblPage";
			this.lblPage.Size = new System.Drawing.Size(13, 22);
			this.lblPage.Text = "1";
			// 
			// cmdPageRight
			// 
			this.cmdPageRight.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
			this.cmdPageRight.Image = global::Bau.Controls.ImageControls.Properties.Resources.ArrowRight;
			this.cmdPageRight.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.cmdPageRight.Name = "cmdPageRight";
			this.cmdPageRight.Size = new System.Drawing.Size(23, 22);
			this.cmdPageRight.Text = "Página siguiente";
			this.cmdPageRight.Click += new System.EventHandler(this.cmdPageRight_Click);
			// 
			// cmdRefresh
			// 
			this.cmdRefresh.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
			this.cmdRefresh.Image = global::Bau.Controls.ImageControls.Properties.Resources.Refresh;
			this.cmdRefresh.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.cmdRefresh.Name = "cmdRefresh";
			this.cmdRefresh.Size = new System.Drawing.Size(23, 22);
			this.cmdRefresh.Text = "Actualizar";
			// 
			// ImagePrinterPreview
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.Controls.Add(this.tableLayoutPanel1);
			this.Name = "ImagePrinterPreview";
			this.Size = new System.Drawing.Size(622, 544);
			this.tableLayoutPanel1.ResumeLayout(false);
			this.tableLayoutPanel1.PerformLayout();
			this.tlbMain.ResumeLayout(false);
			this.tlbMain.PerformLayout();
			this.ResumeLayout(false);

		}

		#endregion

		private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
		private System.Windows.Forms.ToolStrip tlbMain;
		private System.Windows.Forms.PrintPreviewControl prvControl;
		private System.Windows.Forms.ToolStripButton cmdPageSetup;
		private System.Windows.Forms.ToolStripButton cmdPrint;
		private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
		private System.Windows.Forms.ToolStripSplitButton cmdPageZoom;
		private System.Windows.Forms.ToolStripMenuItem mnuZoom200;
		private System.Windows.Forms.ToolStripMenuItem mnuZoom100;
		private System.Windows.Forms.ToolStripMenuItem mnuZoom75;
		private System.Windows.Forms.ToolStripMenuItem mnuZoom50;
		private System.Windows.Forms.ToolStripMenuItem mnuZoom25;
		private System.Windows.Forms.ToolStripButton cmdPageLeft;
		private System.Windows.Forms.ToolStripButton cmdPageRight;
		private System.Windows.Forms.ToolStripButton cmdRefresh;
		private System.Windows.Forms.ToolStripSeparator toolStripMenuItem1;
		private System.Windows.Forms.ToolStripMenuItem mnuZoomAuto;
		private System.Windows.Forms.ToolStripLabel toolStripLabel1;
		private System.Windows.Forms.ToolStripTextBox txtPages;
		private System.Windows.Forms.ToolStripSeparator toolStripSeparator3;
		private System.Windows.Forms.ToolStripLabel lblPage;
	}
}
