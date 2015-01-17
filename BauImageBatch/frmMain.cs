using System;
using System.ComponentModel;
using System.Drawing;
using System.IO;
using System.Windows.Forms;

using Bau.Libraries.ImageFilters.Filters;
using Bau.Libraries.ImageFilters.Helper;

namespace Bau.Applications.BauImageBatch
{
	/// <summary>
	///		Formulario principal
	/// </summary>
	public partial class frmMain : Form
	{ // Enumerados privados
			private enum FormatType
				{ Source,
					Bmp,
					Gif,
					Jpg,
					Png
				}
				
		public frmMain()
		{	InitializeComponent();
		}

		/// <summary>
		///		Inicializa el formulario
		/// </summary>
		private void InitForm()
		{ // Inicializa la lista
				InitListThumbs();
			// Inicializa el combo de formatos
				LoadComboFormats();
			// Inicializa los combos de posición
				LoadComboAlignment(cboWatermarkTextVertical, cboWatermarkTextHorizontal);
				LoadComboAlignment(cboWatermarkImageVertical, cboWatermarkImageHorizontal);
			// Carga la configuración
				LoadConfiguration();
			// Habilita / inhabilita los controles
				EnableControls();
		}		
		
		/// <summary>
		///		Inicializa la lista
		/// </summary>
		private void InitListThumbs()
		{ // Limpia la lista
				lswImages.Clear();
			// Añade las columnas
				lswImages.AddColumn(200, "Archivo");
				lswImages.AddColumn(lswImages.Width - 220, "Directorio");
		}

		/// <summary>
		///		Carga el combo de formatos
		/// </summary>
		private void LoadComboFormats()
		{ cboFormat.Items.Clear();
			cboFormat.AddItem((int) FormatType.Source, "Original");
			cboFormat.AddItem((int) FormatType.Bmp, "Bmp");
			cboFormat.AddItem((int) FormatType.Gif, "Gif");
			cboFormat.AddItem((int) FormatType.Jpg, "Jpg");
			cboFormat.AddItem((int) FormatType.Png, "Png");
			cboFormat.SelectedID = (int) FormatType.Source;
		}
		
		/// <summary>
		///		Carga los combos de alineación de la marca de agua
		/// </summary>
		private void LoadComboAlignment(Bau.Controls.Combos.ComboBoxExtended cboVertical,
																		Bau.Controls.Combos.ComboBoxExtended cboHorizontal)
		{ // Alineación vertical
				cboVertical.Items.Clear();
				cboVertical.AddItem((int) BaseWaterMarkFilter.VAlign.Left, "Izquierda");
				cboVertical.AddItem((int) BaseWaterMarkFilter.VAlign.Center, "Centro");
				cboVertical.AddItem((int) BaseWaterMarkFilter.VAlign.Right, "Derecha");
				cboVertical.SelectedID = (int) BaseWaterMarkFilter.VAlign.Right;
			// Alineación horizontal
				cboHorizontal.Items.Clear();
				cboHorizontal.AddItem((int) BaseWaterMarkFilter.HAlign.Top, "Superior");
				cboHorizontal.AddItem((int) BaseWaterMarkFilter.HAlign.Middle, "Centro");
				cboHorizontal.AddItem((int) BaseWaterMarkFilter.HAlign.Bottom, "Inferior");
				cboHorizontal.SelectedID = (int) BaseWaterMarkFilter.HAlign.Bottom;
		}
		
		/// <summary>
		///		Carga la configuración
		/// </summary>
		private void LoadConfiguration()
		{	// Directorio de salida
				fnTargetPath.PathName = Properties.Settings.Default.PathTarget;
			// Cambio de formato
				cboFormat.SelectedID = Properties.Settings.Default.IDFormat;
			// Cambio de tamaño
				chkSetSize.Checked = Properties.Settings.Default.MustSetSize;
				nudWidthImage.Value = Properties.Settings.Default.WidthImage;
				nudHeightImage.Value = Properties.Settings.Default.HeigthImage;
				chkMaintainRelationtImage.Checked = Properties.Settings.Default.MustMaintainRelationImage;
			// Thumbs
				chkCreateThumbs.Checked = Properties.Settings.Default.MustCreateThumbs;
				nudWidthThumb.Value = Properties.Settings.Default.WidthThumbs;
				nudHeightImage.Value = Properties.Settings.Default.HeightThumbs;
				txtPrefix.Text = Properties.Settings.Default.PrefixThumbs;
				chkMaintainRelationThumb.Checked = Properties.Settings.Default.MustMaintainRelationThumbs;
			// Renombrar la imagen
				chkSetName.Checked = Properties.Settings.Default.MustRename;
				txtNameImage.Text = Properties.Settings.Default.NameRename;
			// Texto para la marca de agua
				chkWatermarkText.Checked = Properties.Settings.Default.MustCreateWatermarkText;
				txtWatermarkFontName.Text = Properties.Settings.Default.WatermarkFontName;
				nudWatermarkFontSize.Value = Properties.Settings.Default.WatermarkFontSize;
				chkWatermarkFontSizeAuto.Checked = Properties.Settings.Default.WatermarkFontSizeAuto;
				txtWatermarkText.Text = Properties.Settings.Default.WatermarkText;
				clrWatermark.Color = Properties.Settings.Default.WatermarkColor;
				nudWatermarkAlpha.Value = Properties.Settings.Default.WatermarkAlpha;
				cboWatermarkTextHorizontal.SelectedID = Properties.Settings.Default.WatermarkTextHorizontal;
				cboWatermarkTextVertical.SelectedID = Properties.Settings.Default.WatermarkTextVertical;
			// Imagen para la marca de agua			
				chkWatermarkImage.Checked = Properties.Settings.Default.MustCreateWatermarkImage;
				fnWatermarkImage.FileName = Properties.Settings.Default.WatermarkImage;
				cboWatermarkImageHorizontal.SelectedID = Properties.Settings.Default.WatermarkImageHorizontal;
				cboWatermarkImageVertical.SelectedID = Properties.Settings.Default.WatermarkImageVertical;
				clrWatermarkImageTransparent.Color = Properties.Settings.Default.WatermarkImageTransparent;
				nudWatermarkImageOpacity.Value = (decimal) Properties.Settings.Default.WatermarkOpacity;
			// Blanco y negro
				chkConvertWhiteBlack.Checked = Properties.Settings.Default.ConvertWhiteAndBlack;
				chkBrightWhiteBlack.Checked = Properties.Settings.Default.WhiteAndBlackBright;
		}
		
		/// <summary>
		///		Graba la configuración
		/// </summary>
		private void SaveConfiguration()
		{ // Pasa los valores del formulario a la configuración
				Properties.Settings.Default.PathTarget = fnTargetPath.PathName;
				Properties.Settings.Default.IDFormat = (int) cboFormat.SelectedID;
				Properties.Settings.Default.MustSetSize = chkSetSize.Checked;
				Properties.Settings.Default.WidthImage = (int) nudWidthImage.Value;
				Properties.Settings.Default.HeigthImage = (int) nudHeightImage.Value;
				Properties.Settings.Default.MustMaintainRelationImage = chkMaintainRelationtImage.Checked;
				Properties.Settings.Default.MustCreateThumbs = chkCreateThumbs.Checked;
				Properties.Settings.Default.WidthThumbs = (int) nudWidthThumb.Value;
				Properties.Settings.Default.HeightThumbs = (int) nudHeightImage.Value;
				Properties.Settings.Default.PrefixThumbs = txtPrefix.Text;
				Properties.Settings.Default.MustMaintainRelationThumbs = chkMaintainRelationThumb.Checked;
				Properties.Settings.Default.MustRename = chkSetName.Checked;
				Properties.Settings.Default.NameRename = txtNameImage.Text;
				Properties.Settings.Default.MustCreateWatermarkText = chkWatermarkText.Checked;
				Properties.Settings.Default.WatermarkFontName = txtWatermarkFontName.Text;
				Properties.Settings.Default.WatermarkFontSize = (int) nudWatermarkFontSize.Value;
				Properties.Settings.Default.WatermarkFontSizeAuto = chkWatermarkFontSizeAuto.Checked;
				Properties.Settings.Default.WatermarkText = txtWatermarkText.Text;
				Properties.Settings.Default.WatermarkColor = clrWatermark.Color;
				Properties.Settings.Default.WatermarkAlpha = (int) nudWatermarkAlpha.Value;
				Properties.Settings.Default.WatermarkTextHorizontal = (int) cboWatermarkTextHorizontal.SelectedID;
				Properties.Settings.Default.WatermarkTextVertical = (int) cboWatermarkTextVertical.SelectedID;
				Properties.Settings.Default.MustCreateWatermarkImage = chkWatermarkImage.Checked;
				Properties.Settings.Default.WatermarkImage = fnWatermarkImage.FileName;
				Properties.Settings.Default.WatermarkImageHorizontal = (int) cboWatermarkImageHorizontal.SelectedID;
				Properties.Settings.Default.WatermarkImageVertical = (int) cboWatermarkImageVertical.SelectedID;
				Properties.Settings.Default.WatermarkImageTransparent = clrWatermarkImageTransparent.Color;
				Properties.Settings.Default.WatermarkOpacity = (double) nudWatermarkImageOpacity.Value;
			// Blanco y negro
				Properties.Settings.Default.ConvertWhiteAndBlack = chkConvertWhiteBlack.Checked;
				Properties.Settings.Default.WhiteAndBlackBright = chkBrightWhiteBlack.Checked;
			// Graba la configuración
				Properties.Settings.Default.Save();
		}
		
		/// <summary>
		///		Habilita los controles 
		/// </summary>
		private void EnableControls()
		{ nudWidthImage.Enabled = nudHeightImage.Enabled = chkMaintainRelationtImage.Enabled = chkSetSize.Checked;
			nudWidthThumb.Enabled = nudHeightThumb.Enabled = chkMaintainRelationThumb.Enabled = 
															txtPrefix.Enabled = chkCreateThumbs.Checked;
			txtNameImage.Enabled = chkSetName.Checked;
			txtWatermarkFontName.Enabled = nudWatermarkFontSize.Enabled = chkWatermarkFontSizeAuto.Enabled = txtWatermarkText.Enabled = 
														clrWatermark.Enabled = nudWatermarkAlpha.Enabled = 
														cboWatermarkTextHorizontal.Enabled = cboWatermarkTextVertical.Enabled = chkWatermarkText.Checked;
			fnWatermarkImage.Enabled = clrWatermarkImageTransparent.Enabled = nudWatermarkImageOpacity.Enabled =
																 cboWatermarkImageHorizontal.Enabled = 
																 cboWatermarkImageVertical.Enabled = chkWatermarkImage.Checked;
			chkBrightWhiteBlack.Enabled = chkConvertWhiteBlack.Checked;
		}
		
		/// <summary>
		///		Añade las imágenes de una carpeta
		/// </summary>
		private void AddFolder()
		{ AddFolder(Bau.Controls.Forms.Helper.GetPathName(Application.ExecutablePath));
		}

		/// <summary>
		///		Añade una carpeta
		/// </summary>
		private void AddFolder(string strPath)
		{ if (!string.IsNullOrEmpty(strPath) && Directory.Exists(strPath))
				{ string [] arrStrFiles = Directory.GetFiles(strPath, "*.*");
				
						// Comienza la modificación de la lista
							lswImages.BeginUpdate();
						// Añade los thumbs
							foreach (string strFile in arrStrFiles)
								AddFile(strFile);
						// Finaliza la modificación
							lswImages.EndUpdate();
				}
		}

		/// <summary>
		///		Añade una imagen
		/// </summary>
		private void AddFile()
		{ AddFile(Bau.Controls.Forms.Helper.GetFileNameOpen("Archivos de imagen|*.gif;*.png;*.jpg;*.bmp"));
		}
		
		/// <summary>
		///		Añade un archivo
		/// </summary>
		private void AddFile(string strFile)
		{ if (!string.IsNullOrEmpty(strFile) && File.Exists(strFile) && IsImage(strFile))
				AddThumb(strFile);
		}
		
		/// <summary>
		///		Añade el thumb con una imagen
		/// </summary>
		private void AddThumb(string strFileName)
		{ ListViewItem lsiItem = lswImages.AddItem(strFileName, Path.GetFileName(strFileName));
		
				lsiItem.SubItems.Add(Path.GetDirectoryName(strFileName));
		}
		
		/// <summary>
		///		Elimina los archivos
		/// </summary>
		private void DeleteFiles()
		{ if (lswImages.SelectedItems == null || lswImages.SelectedItems.Count == 0)
				Bau.Controls.Forms.Helper.ShowMessage(this, "Seleccione los archivos a quitar");
			else if (Bau.Controls.Forms.Helper.ShowQuestion(this, "¿Realmente desea quitar estos archivos?"))
				for (int intIndex = lswImages.Items.Count - 1; intIndex >= 0; intIndex--)
					if (lswImages.Items[intIndex].Selected)
						lswImages.Items.RemoveAt(intIndex);
		}
		
		/// <summary>
		///		Comprueba si un archivo es una imagen
		/// </summary>
		private bool IsImage(string strFileName)
		{ return strFileName.EndsWith(".jpg", StringComparison.CurrentCultureIgnoreCase) ||
						 strFileName.EndsWith(".jpeg", StringComparison.CurrentCultureIgnoreCase) ||
						 strFileName.EndsWith(".bmp", StringComparison.CurrentCultureIgnoreCase) ||
						 strFileName.EndsWith(".gif", StringComparison.CurrentCultureIgnoreCase) ||
						 strFileName.EndsWith(".png", StringComparison.CurrentCultureIgnoreCase);
		}

		/// <summary>
		///		Procesa los archivos
		/// </summary>
		private void Process()
		{ if (string.IsNullOrEmpty(fnTargetPath.PathName))
				Bau.Controls.Forms.Helper.ShowMessage(this, "Seleccione el directorio de salida");
			else
				{ // Crea el directorio
						if (!MakePath(fnTargetPath.PathName))
							Bau.Controls.Forms.Helper.ShowMessage(this, "No se puede crear el directorio de salida");
						else // Procesa los archivos
							foreach (ListViewItem lsiItem in lswImages.Items)
								if (lsiItem.Tag != null && lsiItem.Tag is string)
									Process(fnTargetPath.PathName, lsiItem.Tag as string);
				}
		}
		
		/// <summary>
		///		Crea un directorio si no existe
		/// </summary>
		private bool MakePath(string strPath)
		{ // Crea el directorio si no existe
				try
					{ Directory.CreateDirectory(strPath);
					}
				catch {}
			// Devuelve el valor que indica si se ha creado el directorio
				return Directory.Exists(strPath);
		}
		
		/// <summary>
		///		Procesa un archivo
		/// </summary>
		private void Process(string strPath, string strFileName)
		{ string strFileTarget = GetFileNameTarget(strPath, strFileName);
			
				// Crea el thumbnail
					if (chkCreateThumbs.Checked)
						ProcessImage(strFileName, GetThumbnailName(strFileTarget), (int) nudWidthThumb.Value, 
												(int) nudHeightThumb.Value, chkMaintainRelationThumb.Checked, true);
				// Trata la imagen
					ProcessImage(strFileName, strFileTarget, (int) nudWidthImage.Value, (int) nudHeightImage.Value, 
											 chkMaintainRelationtImage.Checked, false);
		}
		
		/// <summary>
		///		Obtiene el nombre de un thumbnail
		/// </summary>
		private string GetThumbnailName(string strFileTarget)
		{ return Path.Combine(Path.GetDirectoryName(strFileTarget), txtPrefix.Text + Path.GetFileName(strFileTarget));
		}

		/// <summary>
		///		Procesa una imagen: tamaño y marca de agua
		/// </summary>
		private void ProcessImage(string strFileName, string strFileTarget, int intWidth, int intHeight, bool blnLockRelation,
															bool blnThumb)
		{	Image objImage = FiltersHelpers.Load(strFileName);
		
				if (objImage != null)
					{ // Redimensiona la imagen
							if (chkSetSize.Checked)
								{ // Log
										AddLog("Cambiando el tamaño de " + strFileName);
									// Crea la imagen
										objImage = ResizeImage(objImage, intWidth, intHeight, blnLockRelation);
								}
						// Si no es un thumbnail añade las marcas de agua
							if (!blnThumb)
								{	// Añade la marca de agua del texto
										if (chkWatermarkText.Checked)
											{ // Log
													AddLog("Añadiendo la marca de agua");
												// Marca de agua
													objImage = WaterMarkText(objImage);
											}
									// Añade la marca de agua de imagen
										if (chkWatermarkImage.Checked)
											{ // Log
													AddLog("Añadiendo la imagen de la marca de agua");
												// Marca de agua
													objImage = WaterMarkImage(objImage);
											}
								}
						// Convierte a blanco y negro
							if (chkConvertWhiteBlack.Checked)
								{ // Log
										AddLog("Pasando a blanco y negro");
									// Blanco y negro
										objImage = FiltersHelpers.WhiteAndBlack(objImage, chkBrightWhiteBlack.Checked);
								}
						// Graba la imagen
							Save(objImage, strFileTarget);
						// Línea vacía
							AddLog();
					}
				else
					AddLog("Imposible cargar " + strFileName);
		}

		/// <summary>
		///		Redimensiona una imagen
		/// </summary>
		private Image ResizeImage(Image objImage, int intWidth, int intHeight, bool blnMaintainRelation)
		{ if (blnMaintainRelation)
				return FiltersHelpers.Resize(objImage, intWidth);
			else
				return FiltersHelpers.Resize(objImage, intWidth, intHeight);
		}

		/// <summary>
		///		Crea la marca de agua con un texto
		/// </summary>
		private Image WaterMarkText(Image objImage)
		{ return FiltersHelpers.WaterMark(objImage, txtWatermarkFontName.Text, 
																			chkWatermarkFontSizeAuto.Checked ? 0 : (int) nudWatermarkFontSize.Value,
																			txtWatermarkText.Text, 
																			clrWatermark.Color, (int) nudWatermarkAlpha.Value,
																			(BaseWaterMarkFilter.HAlign) cboWatermarkTextHorizontal.SelectedID,
																			(BaseWaterMarkFilter.VAlign) cboWatermarkTextVertical.SelectedID);
		}

		/// <summary>
		///		Crea la marca de agua con una imagen
		/// </summary>
		private Image WaterMarkImage(Image objImage)
		{ return FiltersHelpers.WaterMarkImage(objImage, fnWatermarkImage.FileName,
																					 clrWatermarkImageTransparent.Color,
																					 (double) nudWatermarkImageOpacity.Value,
																					 (BaseWaterMarkFilter.HAlign) cboWatermarkImageHorizontal.SelectedID,
																					 (BaseWaterMarkFilter.VAlign) cboWatermarkImageVertical.SelectedID);
		}
		
		/// <summary>
		///		Graba una imagen
		/// </summary>
		private void Save(Image objImage, string strFileName)
		{	try
				{	// Log
						AddLog("Grabando el archivo: " + strFileName);
					// Graba el archivo
						FiltersHelpers.Save(objImage, strFileName);
				}
			catch (Exception objException)
				{ AddLog("Error al grabar: " + strFileName);
					AddLog(objException.Message);
				}
		}
		
		/// <summary>
		///		Obtiene el nombre del archivo destino
		/// </summary>
		private string GetFileNameTarget(string strPath, string strFileName)
		{ string strFileTarget = "", strExtension = "";
		
				// Obtiene la extensión
					switch ((FormatType) cboFormat.SelectedID)
						{ case FormatType.Source:
									strExtension = Path.GetExtension(strFileName);
								break;
							case FormatType.Bmp:
									strExtension = ".bmp";
								break;
							case FormatType.Gif:
									strExtension = ".gif";
								break;
							case FormatType.Jpg:
									strExtension = ".jpg";
								break;
							case FormatType.Png:
									strExtension = ".png";
								break;
						}
				// Obiene el nombre del archivo destino
					if (chkSetName.Checked)
						{ int intIndex = 1;
						
								// Busca un nombre de archivo que no exista
									while (File.Exists(GetFileName(strPath, intIndex, strExtension)))
										intIndex++;
								// Asigna el nombre de archivo
									strFileTarget = GetFileName(strPath, intIndex, strExtension);
						}
					else
						strFileTarget = Path.Combine(strPath, Path.GetFileNameWithoutExtension(strFileName) + strExtension);
				// Devuelve el nombre de archivo
					return strFileTarget;
		}
		
		/// <summary>
		///		Obtiene el nombre de un archivo a partir de un índice
		/// </summary>
		private string GetFileName(string strPath, int intIndex, string strExtension)
		{ return Path.Combine(strPath, txtNameImage.Text + string.Format("{0:0000}", intIndex) + strExtension);
		}

		/// <summary>
		///		Añade una línea de separación
		/// </summary>
		private void AddLog()
		{ AddLog("--------------------------------------" + Environment.NewLine);
		}

		/// <summary>
		///		Añade un mensaje de log
		/// </summary>
		private void AddLog(string strMessage)
		{ txtLog.AppendText(strMessage + Environment.NewLine);
			Application.DoEvents();
		}
		
		private void cmdAddFolder_Click(object sender, EventArgs e)
		{ AddFolder();
		}

		private void cmdAddFile_Click(object sender, EventArgs e)
		{ AddFile();
		}

		private void cmdDelete_Click(object sender, EventArgs e)
		{ DeleteFiles();
		}

		private void frmMain_Load(object sender, EventArgs e)
		{ InitForm();
		}

		private void chkSetSize_CheckedChanged(object sender, EventArgs e)
		{ EnableControls();
		}

		private void cmdProcess_Click(object sender, EventArgs e)
		{ Process();
		}

		private void frmMain_FormClosing(object sender, FormClosingEventArgs e)
		{ SaveConfiguration();
		}

		private void frmMain_DragDrop(object sender, DragEventArgs e)
		{	string[] arrStrFiles = (string[]) e.Data.GetData(DataFormats.FileDrop);

				// Recorre los archivos abriendo los documentos
					foreach (string strFile in arrStrFiles)
						if (Directory.Exists(strFile))
							AddFolder(strFile);
						else if (IsImage(strFile))
							AddFile(strFile);
		}

		private void frmMain_DragEnter(object sender, DragEventArgs e)
		{ // Si realmente es un archivo
				if(e.Data.GetDataPresent(DataFormats.FileDrop, false))
					e.Effect = DragDropEffects.All;
		}
	}
}
