﻿
Imports System.Text.RegularExpressions
Imports System.Drawing.Imaging
Imports System.Drawing
Imports System.Windows.Forms
Imports PDFLibNet32
Imports System.ComponentModel
Imports System.util
Imports PdfLibNetLibrary

Public Class FrmPreview

    'Private WithEvents Visualizador As PDFViewer
    'Private WithEvents m_GlobalHook As IKeyboardMouseEvents

    Private NomeArquivo As String
    Private Arquivo As Byte()
    Private _PointClick As Point
    Private _Pagina As Integer
    Private _PDFHeigh As Integer
    Private _PDFWidth As Integer

    Private mOriginalFileName
    Private mPDFFileName As String
    Private mPDFPageCount As Integer
    Private mCurrentPageNumber As Integer
    Private m_PanStartPoint As Point
    Private m_PanEndPoint As Point
    Private mBookmarks As ArrayList
    Private mAllowBookmarks As Boolean = True
    Private mUseXPDF As Boolean = True
    Private mPDFDoc As PDFWrapper
    Private FromBeginning As Boolean = True
    Private XPDFPrintingPicBox As New PictureBox
    Private mContinuousPages As Boolean = False
    Private PageInitDone As Boolean = False
    Private ScrollBarPosition As Integer = 0
    Private ScrollUnitsPerPage As Integer = 0
    Private ContinuousImages As List(Of System.Drawing.Image)
    Private mUserPassword As String = ""
    Private mOwnerPassword As String = ""
    Private mPassword As String = ""
    'Optimal render
    Private mLastPageNumber As Integer
    Private mResizeStopped As Boolean = False
    Private mResizeCheckTimer As New Timer
    Private mPDFViewerHeight As Integer
    Private mPDFViewerWidth As Integer
    Private mRotation As List(Of Integer)

    Public Event DocumentMouseDown(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs)
    Public Event DocumentMouseUp(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs)


    Private WithEvents objPictureBox As New PictureBox

    Public Event PosicaoSelo(Position As Point, Pagina As Integer, X As Integer, Y As Integer, Escala As Double, Rotation As Integer)

    Public Property AssinaturaConfirmada As Boolean

    Private _TipoSelo As eTipoSelo

    Public Enum eTipoSelo
        Selo = 1
        SeloCRM = 2
        SeloCREA = 3
        SeloCargo = 4
        SeloCargoCRM = 5
        SeloCargoCREA = 6
        SeloCertifico = 7
        Resende = 8
        RioDeJaneiro = 9
        Buena = 10
        Caldas = 11
        SaoPaulo = 12
        Caetite = 13
        Fortaleza = 14
        ConferidoOriginal = 15
        ChancelaJuridica = 16
    End Enum

    Public Sub New(PDF As String, TipoSelo As eTipoSelo)
        InitializeComponent()
        NomeArquivo = PDF
        objPictureBox.Name = "SinglePicBox"
        AssinaturaConfirmada = False
        _TipoSelo = TipoSelo
    End Sub


    Public Sub New(PDF As Byte(), TipoSelo As eTipoSelo)
        InitializeComponent()
        Arquivo = PDF
        objPictureBox.Name = "SinglePicBox"
        AssinaturaConfirmada = False
        _TipoSelo = TipoSelo
    End Sub

    Public ReadOnly Property PDFHeigh As Integer
        Get
            Return _PDFHeigh
        End Get
    End Property

    Public ReadOnly Property PDFWidth As Integer
        Get
            Return _PDFWidth
        End Get
    End Property

    Public Property Posicao As Point
        Get
            Return _PointClick
        End Get
        Set(value As Point)
            _PointClick = value
        End Set
    End Property

    Public Property Pagina As Integer
        Get
            Return _Pagina
        End Get
        Set(value As Integer)
            _Pagina = value
        End Set
    End Property

    'Private Sub Subscribe()
    '    m_GlobalHook = Hook.GlobalEvents()
    'End Sub

    'Private Sub UnSubscribe()
    '    m_GlobalHook.Dispose()
    'End Sub

    'Private Sub FrmPreview_Click(sender As Object, e As EventArgs) Handles Me.Click
    '    _PointClick.X = MousePosition.X
    '    _PointClick.Y = MousePosition.Y
    '    _Pagina = Visualizador.tsPageNum.Text
    'End Sub

    Private Sub FrmPreview_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        If Trim$(NomeArquivo) <> "" Then
            Me.FileName = NomeArquivo
        Else
            Me.File = Arquivo
        End If
        'Visualizador.Visible = True
        'Visualizador.Dock = DockStyle.Fill
        'Visualizador.tscbZoom.Visible = False
        'Visualizador.tsZoomIn.Visible = False
        'Visualizador.tsZoomOut.Visible = False

        'Visualizador.btNext_Click(sender, e)
        'Visualizador.tsZoomIn_Click(sender, e)
    End Sub


    Public ReadOnly Property PDFViewerHeight As Integer
        Get
            Return mPDFViewerHeight
        End Get
    End Property

    Public ReadOnly Property PDFViewerWidth As Integer
        Get
            Return mPDFViewerWidth
        End Get
    End Property


    Public ReadOnly Property StartPoint As Point
        Get
            Return m_PanStartPoint
        End Get
    End Property

    Public ReadOnly Property EndPoint As Point
        Get
            Return m_PanEndPoint
        End Get
    End Property

    Public Property File As Byte()
        Get
            Return Arquivo
        End Get
        Set(value As Byte())
            Arquivo = value
            Dim MS As New IO.MemoryStream(Arquivo)
            MS.Flush()

            mPDFDoc = New PDFWrapper("")

            mPDFDoc.LoadPDF(MS)

            InitPageRange()
            InitViewModes()
            InitRotation()
            InitializePageView(ViewMode.ACTUAL_SIZE)
            DisplayCurrentPage()
            Me.Enabled = True
            Cursor.Current = Cursors.Default
        End Set
    End Property



    Public Property FileName() As String
        Get
            Return mOriginalFileName
        End Get
        Set(ByVal value As String)
            If Nothing = value Or value = "" Then
                Exit Property
            End If
            mOriginalFileName = value
            mUserPassword = ""
            mOwnerPassword = ""
            mPassword = ""
            Dim userPassword As String = Nothing
            If ImageUtil.IsTiff(value) Then
                'Tiff Specific behavior
                InitBottomToolbar("TIFF")
            ElseIf ImageUtil.IsPDF(value) Then
                Cursor.Current = Cursors.WaitCursor
                If iTextSharpUtil.IsEncrypted(value) Then
FileIsEncrypted:
                    Dim frmPassword As New Password
                    Dim result As DialogResult = frmPassword.ShowDialog()
                    If result = DialogResult.OK Then
                        If Not frmPassword.OwnerPassword = "" Then
                            mOwnerPassword = frmPassword.OwnerPassword
                            If iTextSharpUtil.IsPasswordValid(value, frmPassword.OwnerPassword) = False Then
                                MsgBox("Senha incorreta.", MsgBoxStyle.Critical, "Senha incorreta.")
                                Cursor.Current = Cursors.Default
                                Exit Property
                            Else
                                mOwnerPassword = frmPassword.OwnerPassword
                                mPassword = mOwnerPassword
                            End If
                        End If
                        If Not frmPassword.UserPassword = "" Then
                            If iTextSharpUtil.IsPasswordValid(value, frmPassword.UserPassword) = False Then
                                MsgBox("Senha incorreto.", MsgBoxStyle.Critical, "Senha incorreta.")
                                Cursor.Current = Cursors.Default
                                Exit Property
                            Else
                                mUserPassword = frmPassword.UserPassword
                                mPassword = mUserPassword
                            End If
                        End If
                    End If
                End If
                If mUseXPDF Then
                    If Not Nothing Is mPDFDoc Then
                        mPDFDoc.Dispose()
                    End If
                    Try
                        mPDFDoc = New PDFWrapper("")
                        If mOwnerPassword <> "" Then
                            mPDFDoc.OwnerPassword = mOwnerPassword
                            mPassword = mOwnerPassword
                        End If
                        If mUserPassword <> "" Then
                            mPDFDoc.UserPassword = mUserPassword
                            mPassword = mUserPassword
                        End If

                        mPDFDoc.LoadPDF(value)

                    Catch ex As System.Security.SecurityException
                        GoTo FileIsEncrypted
                    Catch ex As Exception
                        If Not Nothing Is mPDFDoc Then
                            mPDFDoc.Dispose()
                        End If
                        GoTo GhostScriptFallBack
                    End Try
                    InitBottomToolbar("XPDF")
                Else
GhostScriptFallBack:
                    InitBottomToolbar("GS")
                End If
            Else
                Me.Enabled = False
            End If
            mPDFFileName = value
            InitViewModes()
            InitPageRange()
            InitRotation()
            InitializePageView(ViewMode.ACTUAL_SIZE)
            'If mAllowBookmarks And ImageUtil.IsPDF(mOriginalFileName) Then
            '    InitBookmarks()
            'Else
            '    HideBookmarks()
            'End If
            DisplayCurrentPage()
            'tscbZoom.SelectedIndex = 1
            Me.Enabled = True
            Cursor.Current = Cursors.Default
        End Set
    End Property

    'Public Property ContinuousPages() As Boolean
    '    Get
    '        Return mContinuousPages
    '    End Get
    '    Set(ByVal value As Boolean)
    '        mContinuousPages = value
    '    End Set
    'End Property

    Public Property UseXPDF() As Boolean
        Get
            Return mUseXPDF
        End Get
        Set(ByVal value As Boolean)
            mUseXPDF = value
        End Set
    End Property

    Public Property AllowBookmarks() As Boolean
        Get
            Return mAllowBookmarks
        End Get
        Set(ByVal value As Boolean)
            mAllowBookmarks = value
            'If value = False Then
            '    HideBookmarks()
            'End If
        End Set
    End Property

    Public ReadOnly Property PageCount(ByVal FileName As String)
        Get
            Return ImageUtil.GetImageFrameCount(FileName)
        End Get
    End Property

    Public ReadOnly Property Print(ByVal FileName As String)
        Get
            PrinterUtil.PrintImagesToPrinter(FileName)
            Return 1
        End Get
    End Property

    Public Sub SelectFile()
        OpenFileDialog1.Filter = "PDF (*.pdf;)|*.pdf;"
        OpenFileDialog1.FileName = ""
        OpenFileDialog1.Title = "Selecionar arquivo PDF, desbloqueado e sem senha."
        OpenFileDialog1.Multiselect = False
        OpenFileDialog1.ShowDialog()
        FileName = OpenFileDialog1.FileName
    End Sub

    '    Public Function OCRCurrentPage() As String
    '        Cursor.Current = Cursors.WaitCursor
    '        Dim TempFile As String = System.IO.Path.GetTempPath & Now.Ticks & ".txt"
    '        OCRCurrentPage = ""
    '        Try
    '            AFPDFLibUtil.ExportPDF(mPDFDoc, TempFile, mCurrentPageNumber, mCurrentPageNumber)
    '            OCRCurrentPage = System.IO.File.ReadAllText(TempFile)
    '            System.IO.File.Delete(TempFile)
    '            If Regex.IsMatch(OCRCurrentPage, "\w") = False Then
    '                GoTo OCRCurrentImage
    '            End If
    '        Catch ex As Exception
    '            If Regex.IsMatch(OCRCurrentPage, "\w") = False Then
    '                GoTo OCRCurrentImage
    '            End If
    '        End Try
    '        Cursor.Current = Cursors.Default
    '        Exit Function
    'OCRCurrentImage:
    '        Try
    '            ' OCRCurrentPage = TesseractOCR.OCRImage(FindPictureBox("SinglePicBox").Image, TesseractOCR.Language.English)
    '        Catch ex As Exception
    '            'OCR failed
    '        End Try
    '        Cursor.Current = Cursors.Default
    '    End Function

    Private Sub ConvertGraphicsToPDF()
        OpenFileDialog1.Filter = "Image Files(*.BMP;*.JPG;*.GIF;*.PNG;*.TIF)|*.BMP;*.JPG;*.GIF;*.PNG;*.TIF"
        OpenFileDialog1.FileName = ""
        OpenFileDialog1.Title = "Select multiple image files to convert to PDF"
        OpenFileDialog1.Multiselect = True
        If OpenFileDialog1.ShowDialog() = Windows.Forms.DialogResult.OK Then
            Dim exportOptionsDialog As New ExportImageOptions(OpenFileDialog1.FileNames)
            exportOptionsDialog.ShowDialog()
            Try
                FileName = exportOptionsDialog.SavedFileName
            Catch ex As Exception
                'do nothing
            End Try
        End If

    End Sub

    Private Sub PDFViewer_ControlRemoved(ByVal sender As Object, ByVal e As System.Windows.Forms.ControlEventArgs) Handles Me.ControlRemoved
        If Not Nothing Is mPDFDoc Then
            Try
                mPDFDoc.Dispose()
            Catch ex As Exception
            End Try
        End If
    End Sub

    Private Sub PDFViewer_Disposed(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Disposed
        If Not Nothing Is mPDFDoc Then
            Try
                mPDFDoc.Dispose()
            Catch ex As Exception
            End Try

        End If
    End Sub

    Private Sub PDFViewer_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        ''Do something on control load
        'HideBookmarks()
    End Sub

    Private Sub Timer1_Tick(ByVal sender As Object, ByVal e As System.EventArgs) Handles Timer1.Tick
        If Me.Height = mPDFViewerHeight And Me.Width = mPDFViewerWidth Then
            If mResizeStopped = False Then
                DisplayCurrentPage()
                mResizeStopped = True
            End If
            Timer1.Enabled = False
        End If
    End Sub

    Private Sub PDFViewer_Resize(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Resize
        If PageInitDone = True Then
            CenterPicBoxInPanel(FindPictureBox("SinglePicBox"))
        End If
    End Sub

    Private Sub tsPrevious_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles tsPrevious.Click
        mLastPageNumber = mCurrentPageNumber
        mCurrentPageNumber -= 1
        DisplayCurrentPage()
    End Sub

    Private Sub tsNext_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles tsNext.Click
        mLastPageNumber = mCurrentPageNumber
        mCurrentPageNumber += 1
        DisplayCurrentPage()
    End Sub

    Private Sub tsZoomOut_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        Dim objPictureBox As PictureBox = FindPictureBox(mCurrentPageNumber)
        ImageUtil.PictureBoxZoomOut(objPictureBox)
        objPictureBox.Refresh()
        'tscbZoom.Text = GetCurrentScalePercentage() & " %"
        DisplayCurrentPage()
    End Sub

    Public Sub tsZoomIn_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)

        'If GetCurrentScalePercentage() > 500 Then
        '  Exit Sub
        'End If
        Dim objPictureBox As PictureBox = FindPictureBox(mCurrentPageNumber)
        ImageUtil.PictureBoxZoomIn(objPictureBox)
        objPictureBox.Refresh()
        'tscbZoom.Text = GetCurrentScalePercentage() & " %"
        DisplayCurrentPage()
    End Sub

    Private Sub tsPageNum_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles tsPageNum.KeyPress
        If (Asc(e.KeyChar) = Keys.Enter) Then
            If tsPageNum.Text = "" Then
                mCurrentPageNumber = 1
            Else
                mCurrentPageNumber = tsPageNum.Text
            End If
            DisplayCurrentPage()
        Else
            e.Handled = TrapKey(Asc(e.KeyChar))
        End If
    End Sub

    Private Sub tsPageNum_Leave(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles tsPageNum.Leave
        mCurrentPageNumber = tsPageNum.Text
        DisplayCurrentPage()
    End Sub

    Private Sub tscbZoom_Change(ByVal sender As System.Object, ByVal e As System.EventArgs)
        'Select Case tscbZoom.Text
        '    Case "Fit To Screen"
        '        If mContinuousPages Then
        '            InitializePageView(ViewMode.FIT_TO_SCREEN)
        '        Else
        '            ApplyZoom(ViewMode.FIT_TO_SCREEN)
        '        End If
        '    Case "Actual Size"
        '        If mContinuousPages Then
        '            InitializePageView(ViewMode.ACTUAL_SIZE)
        '        Else
        '            ApplyZoom(ViewMode.ACTUAL_SIZE)
        '        End If
        '    Case "Fit To Width"
        '        If mContinuousPages Then
        '            InitializePageView(ViewMode.FIT_WIDTH)
        '        Else
        '            ApplyZoom(ViewMode.FIT_WIDTH)
        '        End If
        'End Select
        'CenterPicBoxInPanel(FindPictureBox("SinglePicBox"))
        'DisplayCurrentPage()
    End Sub

    Private Sub tsPrint_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        If ImageUtil.IsPDF(mOriginalFileName) Or ImageUtil.IsTiff(mOriginalFileName) Then
            PrinterUtil.PrintImagesToPrinter(mOriginalFileName)
        End If
    End Sub

    Private Sub tsRotateCC_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        mRotation(mCurrentPageNumber - 1) -= 1
        ImageUtil.RotateImageCounterclockwise(FindPictureBox(mCurrentPageNumber))
    End Sub

    Private Sub tsRotateC_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        mRotation(mCurrentPageNumber - 1) += 1
        ImageUtil.RotateImageClockwise(FindPictureBox(mCurrentPageNumber))
    End Sub

    Private Sub tsExport_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        If ImageUtil.IsPDF(mOriginalFileName) Then
            Dim exportOptionsDialog As New ExportOptions(mPDFFileName, mPDFDoc, mPassword)
            exportOptionsDialog.ShowDialog()
        ElseIf ImageUtil.IsTiff(mOriginalFileName) Then
            Dim FileArray(0) As String
            FileArray(0) = mPDFFileName
            Dim exportOptionsDialog As New ExportImageOptions(FileArray)
            exportOptionsDialog.ShowDialog()
        End If
        Me.Focus()
    End Sub

    Private Sub tsImport_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        ConvertGraphicsToPDF()
    End Sub

#Region "Constraints"

    Private Sub CheckPageBounds()

        If mCurrentPageNumber >= mPDFPageCount Then
            mCurrentPageNumber = mPDFPageCount
            tsNext.Enabled = False
        ElseIf mCurrentPageNumber <= 1 Then
            mCurrentPageNumber = 1
            tsPrevious.Enabled = False
        End If

        If mCurrentPageNumber < mPDFPageCount And mPDFPageCount > 1 And mCurrentPageNumber > 1 Then
            tsNext.Enabled = True
            tsPrevious.Enabled = True
        End If

        If mCurrentPageNumber = mPDFPageCount And mPDFPageCount > 1 And mCurrentPageNumber > 1 Then
            tsPrevious.Enabled = True
        End If

        If mCurrentPageNumber = 1 And mPDFPageCount > 1 Then
            tsNext.Enabled = True
        End If

        If mPDFPageCount = 1 Then
            tsNext.Enabled = False
            tsPrevious.Enabled = False
        End If

    End Sub

#End Region

#Region "Helper Functions"
    Private Function GetImageFromFile(File As Byte(), ByVal iFrameNumber As Integer, Optional ByVal DPI As Integer = 72) As System.Drawing.Image
        'Cursor.Current = Cursors.WaitCursor
        Dim REtorno As Image = Nothing
        REtorno = AFPDFLibUtil.GetImageFromPDF(mPDFDoc, iFrameNumber + 1, DPI)
        ' ImageUtil.ApplyRotation(REtorno, mRotation(iFrameNumber))
        Return REtorno
    End Function

    Private Function GetImageFromFile(ByVal sFileName As String, ByVal iFrameNumber As Integer, Optional ByVal DPI As Integer = 72) As System.Drawing.Image
        'Cursor.Current = Cursors.WaitCursor
        GetImageFromFile = Nothing
        If mUseXPDF And ImageUtil.IsPDF(sFileName) Then 'Use AFPDFLib (XPDF)
            Try
                GetImageFromFile = AFPDFLibUtil.GetImageFromPDF(mPDFDoc, iFrameNumber + 1, DPI)
            Catch ex As Exception
                InitBottomToolbar("GS")
                GoTo GhostScriptFallBack
            End Try
        Else 'Use Ghostscript
GhostScriptFallBack:
            If ImageUtil.IsPDF(sFileName) Then 'convert one frame to a tiff for viewing
                GetImageFromFile = ConvertPDF.PDFConvert.GetPageFromPDF(sFileName, iFrameNumber + 1, DPI, mPassword)
            ElseIf ImageUtil.IsTiff(sFileName) Then
                GetImageFromFile = ImageUtil.GetFrameFromTiff(sFileName, iFrameNumber)
            End If
        End If
        ' ImageUtil.ApplyRotation(GetImageFromFile, mRotation(iFrameNumber))
    End Function



    Private Sub InitPageRange()
        If Trim$(mPDFFileName) = "" Then
            mPDFPageCount = ImageUtil.GetImageFrameCount(Arquivo, mPassword)
        Else
            mPDFPageCount = ImageUtil.GetImageFrameCount(mPDFFileName, mPassword)
        End If

        mCurrentPageNumber = 1
    End Sub

    'Private Sub InitBookmarks()
    '    TreeView1.Nodes.Clear()
    '    Dim HasBookmarks As Boolean = False
    '    Try
    '        If mUseXPDF Then
    '            HasBookmarks = AFPDFLibUtil.FillTree(TreeView1, mPDFDoc)
    '            AddHandler TreeView1.NodeMouseClick, AddressOf AFPDFLib_NodeMouseClick
    '            RemoveHandler TreeView1.NodeMouseClick, AddressOf ItextSharp_NodeMouseClick
    '        Else
    '            HasBookmarks = iTextSharpUtil.BuildBookmarkTreeFromPDF(mOriginalFileName, TreeView1.Nodes, mPassword)
    '            AddHandler TreeView1.NodeMouseClick, AddressOf ItextSharp_NodeMouseClick
    '            RemoveHandler TreeView1.NodeMouseClick, AddressOf AFPDFLib_NodeMouseClick
    '        End If
    '    Catch ex As Exception
    '        'Some bookmark structures do not parse from XML yet.
    '        'TODO
    '    End Try
    '    If HasBookmarks Then
    '        ShowBookmarks()
    '        TreeView1.ExpandAll()
    '        TreeView1.SelectedNode = TreeView1.Nodes.Item(0)
    '    Else
    '        HideBookmarks()
    '    End If
    'End Sub

    Private Sub InitBottomToolbar(ByVal Mode As String)
        If Mode = "TIFF" Then
            btSearch.Visible = False
            btNext.Visible = False
            tbSearchText.Visible = False
            ToolStripSeparator5.Visible = False
            ' tsExport.Visible = True
            ' tsExport.ToolTipText = "Exportar TIFF para formato PDF"
        ElseIf Mode = "GS" Then
            btSearch.Visible = False
            btNext.Visible = False
            tbSearchText.Visible = False
            ToolStripSeparator5.Visible = False
            ' tsExport.Visible = False
        ElseIf Mode = "XPDF" Then
            btSearch.Visible = True
            btNext.Visible = True
            tbSearchText.Visible = True
            ToolStripSeparator5.Visible = True
            ' tsExport.Visible = True
            ' tsExport.ToolTipText = "Exportar PDF para outro tipo de formato"
        End If
    End Sub

    'Private Sub HideBookmarks()
    '    TreeView1.Visible = False
    '    TableLayoutPanel1.SetColumnSpan(TreeView1, 2)
    '    TableLayoutPanel1.SetColumnSpan(Panel1, 2)
    '    TableLayoutPanel1.SetColumn(Panel1, 0)
    '    TableLayoutPanel1.SetColumn(TreeView1, 1)
    'End Sub

    'Private Sub ShowBookmarks()
    '    HideBookmarks()
    '    'TreeView1.Visible = True
    '    'TableLayoutPanel1.SetColumnSpan(TreeView1, 1)
    '    'TableLayoutPanel1.SetColumnSpan(Panel1, 1)
    '    'TableLayoutPanel1.SetColumn(Panel1, 1)
    '    'TableLayoutPanel1.SetColumn(TreeView1, 0)
    'End Sub

    Public Enum ViewMode
        FIT_TO_SCREEN
        FIT_WIDTH
        ACTUAL_SIZE
    End Enum

    Private Sub InitRotation()
        mRotation = New List(Of Integer)
        For i As Integer = 1 To mPDFPageCount
            mRotation.Add(0)
        Next
    End Sub

    Private Sub InitViewModes()
        ' tscbZoom.Items.Clear()
        ' tscbZoom.Items.Add("Fit To Screen")
        ' tscbZoom.Items.Add("Fit To Width")
        ' If ImageUtil.IsTiff(mPDFFileName) Then
        ' tscbZoom.Items.Add("Actual Size")
        ' End If
    End Sub

    Private Sub InitializePageView(Optional ByVal Mode As ViewMode = ViewMode.ACTUAL_SIZE)
        Dim myFlowLayoutPanel As New FlowLayoutPanel
        Panel1.SuspendLayout()
        Panel1.Controls.Clear()
        If mContinuousPages Then
            myFlowLayoutPanel.Dock = DockStyle.Fill
            myFlowLayoutPanel.FlowDirection = FlowDirection.TopDown
            myFlowLayoutPanel.AutoScroll = True
            myFlowLayoutPanel.Width = Panel1.Width - 20
            myFlowLayoutPanel.Height = Panel1.Height - 20
            myFlowLayoutPanel.WrapContents = False
            AddHandler myFlowLayoutPanel.Scroll, AddressOf FlowPanel_Scroll
            For i As Integer = 1 To mPDFPageCount
                Dim ObjPictureBox As New PictureBox
                ObjPictureBox.Name = i.ToString
                ObjPictureBox.SizeMode = PictureBoxSizeMode.Normal '.Zoom
                ObjPictureBox.Height = myFlowLayoutPanel.Height - 14
                ObjPictureBox.Width = myFlowLayoutPanel.Width - 14
                ObjPictureBox.Location = New Point(0, 0)
                AddHandler ObjPictureBox.MouseUp, AddressOf picImage_MouseUp
                AddHandler ObjPictureBox.MouseDown, AddressOf picImage_MouseDown
                AddHandler ObjPictureBox.MouseMove, AddressOf picImage_MouseMove
                'ObjPictureBox.Image = New System.Drawing.Bitmap(1, 1)
                'ShowImageFromFile(mPDFFileName, i - 1, ObjPictureBox)
                myFlowLayoutPanel.Controls.Add(ObjPictureBox)
            Next
            Dim EndPictureBox As New PictureBox
            EndPictureBox.Name = "0"
            EndPictureBox.Height = 1
            EndPictureBox.Width = 1
            EndPictureBox.Location = New Point(0, 0)
            myFlowLayoutPanel.Controls.Add(EndPictureBox)
            ApplyToAllPictureBoxes(myFlowLayoutPanel, Mode)
            Panel1.Controls.Add(myFlowLayoutPanel)
            ScrollUnitsPerPage = FindFlowLayoutPanel().VerticalScroll.Maximum / mPDFPageCount
        Else
            Panel1.Controls.Add(objPictureBox)
            objPictureBox.SizeMode = PictureBoxSizeMode.Normal '.Zoom
            objPictureBox.Dock = DockStyle.None
            objPictureBox.Height = Panel1.Height - 14
            objPictureBox.Width = Panel1.Width - 14
            objPictureBox.Location = New Point(0, 0)
            AddHandler objPictureBox.MouseUp, AddressOf picImage_MouseUp
            AddHandler objPictureBox.MouseDown, AddressOf picImage_MouseDown
            AddHandler objPictureBox.MouseMove, AddressOf picImage_MouseMove
            ApplyToAllPictureBoxes(Panel1, Mode)
        End If
        Panel1.ResumeLayout()
        PageInitDone = True
    End Sub

    Private Sub ApplyZoom(ByVal Mode As ViewMode)
        Dim oPictureBox As PictureBox = FindPictureBox("SinglePicBox")
        If Mode = ViewMode.FIT_TO_SCREEN Then
            Dim picHeight As Integer = oPictureBox.Height
            Dim picWidth As Integer = oPictureBox.Width
            Dim parentHeight As Integer = oPictureBox.Parent.ClientSize.Height - 14
            Dim parentWidth As Integer = oPictureBox.Parent.ClientSize.Width - 14
            Dim HScale As Single = parentWidth / picWidth
            Dim VScale As Single = parentHeight / picHeight
            If VScale > HScale Then
                oPictureBox.Height = picHeight * HScale
                oPictureBox.Width = picWidth * HScale
            Else
                oPictureBox.Height = picHeight * VScale
                oPictureBox.Width = picWidth * VScale
            End If
        ElseIf Mode = ViewMode.FIT_WIDTH And Not Nothing Is oPictureBox.Image Then
            oPictureBox.Width = oPictureBox.Parent.ClientSize.Width - 18
            Dim ScaleAmount As Double = (oPictureBox.Width / oPictureBox.Image.Width)
            oPictureBox.Height = CInt(oPictureBox.Image.Height * ScaleAmount)
        ElseIf Mode = ViewMode.ACTUAL_SIZE And Not Nothing Is oPictureBox.Image Then
            oPictureBox.Width = oPictureBox.Image.Width
            oPictureBox.Height = oPictureBox.Image.Height
        End If
        CenterPicBoxInPanel(FindPictureBox("SinglePicBox"))
        'tscbZoom.Text = GetCurrentScalePercentage() & " %"
    End Sub

    Private Sub MakePictureBox1To1WithImage(ByRef oPictureBox As PictureBox, Optional ByRef newImage As Drawing.Image = Nothing)

        If Not Nothing Is newImage Then
            oPictureBox.Width = newImage.Width
            oPictureBox.Height = newImage.Height
        Else
            oPictureBox.Width = oPictureBox.Image.Width
            oPictureBox.Height = oPictureBox.Image.Height
        End If

    End Sub

    Private Sub AutoRotatePicBox(ByRef oPictureBox As PictureBox, ByRef newImage As Drawing.Image)
        Dim picHeight As Integer = oPictureBox.Height
        Dim picWidth As Integer = oPictureBox.Width
        Dim imgHeight As Integer = newImage.Height
        Dim imgWidth As Integer = newImage.Width
        If (picWidth > picHeight And imgWidth < imgHeight) Or (picWidth < picHeight And imgWidth > imgHeight) Then
            oPictureBox.Width = picHeight
            oPictureBox.Height = picWidth
            oPictureBox.Width = imgWidth 'Alterado para redimensionar a picture com o tamnho da imagem
            oPictureBox.Height = imgHeight
        End If
    End Sub

    Private Sub CenterPicBoxInPanel(ByRef oPictureBox As PictureBox)
        ImageUtil.RecalcPageLocation(oPictureBox)
    End Sub

    Private Delegate Sub ShowImage(ByVal sFileName As String, ByVal iFrameNumber As Integer, ByRef oPictureBox As PictureBox, ByVal XPDFDPI As Integer)

    Private Sub FlowPanel_Scroll(ByVal sender As Object, ByVal e As System.Windows.Forms.ScrollEventArgs)
        ScrollBarPosition = e.NewValue()
        Dim ImagesWereLoaded As Boolean = False
        For i As Integer = 0 To 1
            Dim pageNumber As Integer = (System.Math.Floor(ScrollBarPosition / ScrollUnitsPerPage) + 1) + i
            If pageNumber >= 1 And pageNumber <= mPDFPageCount Then
                If Nothing Is FindPictureBox(pageNumber).Image Then
                    FindPictureBox(pageNumber).Image = GetImageFromFile(mPDFFileName, pageNumber - 1, 100)
                    ImagesWereLoaded = True
                    FindPictureBox(pageNumber).Refresh()
                End If
            End If
        Next
        If ImagesWereLoaded Then
            Dim lastPage As Integer = System.Math.Floor(ScrollBarPosition / ScrollUnitsPerPage) + 2
            If lastPage > mPDFPageCount Then
                lastPage = mPDFPageCount
            End If
            Dim firstPage As Integer = System.Math.Floor(ScrollBarPosition / ScrollUnitsPerPage) + 1
            If firstPage < 1 Then
                firstPage = 1
            End If
            ClearAllPictureBoxes(firstPage, lastPage)
        End If
        mCurrentPageNumber = System.Math.Floor(ScrollBarPosition / ScrollUnitsPerPage) + 1
        UpdatePageLabel()
    End Sub

    Private Sub UpdatePageLabel()
        tsPageLabel.Text = "Página " & mCurrentPageNumber & " de " & mPDFPageCount
        tsPageNum.Text = mCurrentPageNumber
    End Sub

    'Private Sub DisplayCurrentPage()
    '  CheckPageBounds()
    '  UpdatePageLabel()
    '  ShowImageFromFile(mPDFFileName, mCurrentPageNumber - 1, FindPictureBox(mCurrentPageNumber))
    '  If mContinuousPages Then
    '    FindFlowLayoutPanel().ScrollControlIntoView(FindPictureBox(mCurrentPageNumber))
    '    ClearAllPictureBoxes(mCurrentPageNumber, mCurrentPageNumber)
    '  End If
    'End Sub

    Private Sub DisplayCurrentPage()
        Dim oPict As PictureBox = FindPictureBox(mCurrentPageNumber)
        If mLastPageNumber <> mCurrentPageNumber Then
            CheckPageBounds()
            UpdatePageLabel()
            Dim newImage As Drawing.Image
            If mUseXPDF Then
                If Trim$(mPDFFileName) <> "" Then
                    newImage = GetImageFromFile(mPDFFileName, mCurrentPageNumber - 1, mPDFDoc.RenderDPI)
                Else
                    newImage = GetImageFromFile(Arquivo, mCurrentPageNumber - 1, AFPDFLibUtil.GetOptimalDPI(mPDFDoc, oPict))
                End If
            Else
                Dim optimalDPI As Integer = 0
                If Trim$(mPDFFileName) <> "" Then
                    If ImageUtil.IsPDF(mPDFFileName) Then
                        optimalDPI = mPDFDoc.RenderDPI ' iTextSharpUtil.GetOptimalDPI(mPDFFileName, mCurrentPageNumber, oPict, mPassword)
                    End If
                Else
                    optimalDPI = iTextSharpUtil.GetOptimalDPI(Arquivo, mCurrentPageNumber, oPict, mPassword)
                End If

                If Trim$(mPDFFileName) <> "" Then
                    newImage = GetImageFromFile(mPDFFileName, mCurrentPageNumber - 1, mPDFDoc.RenderDPI)
                Else
                    newImage = GetImageFromFile(Arquivo, mCurrentPageNumber - 1, AFPDFLibUtil.GetOptimalDPI(mPDFDoc, oPict))
                End If
            End If

            If Not Nothing Is newImage Then
                AutoRotatePicBox(oPict, newImage)
                MakePictureBox1To1WithImage(oPict, newImage)
                CenterPicBoxInPanel(oPict)
                oPict.Image = newImage
            End If
            oPict.Refresh()
            If mContinuousPages Then
                FindFlowLayoutPanel().ScrollControlIntoView(oPict)
                ClearAllPictureBoxes(mCurrentPageNumber, mCurrentPageNumber)
            End If
            Exit Sub
        End If

        AutoRotatePicBox(oPict, oPict.Image)
        MakePictureBox1To1WithImage(oPict, oPict.Image)
        CenterPicBoxInPanel(oPict)
    End Sub

    Public Function GetCurrentScalePercentage() As Double
        GetCurrentScalePercentage = 1
        Dim objPictureBox As PictureBox = FindPictureBox("SinglePicBox")
        If Not Nothing Is objPictureBox.Image Then
            Dim OriginalWidth As Double = objPictureBox.Image.Width
            Dim CurrentWidth As Double = objPictureBox.Width
            Return Double.Parse((CurrentWidth / OriginalWidth) * 100)
        End If
    End Function

    Private Function FindPictureBox(ByVal controlName As String) As PictureBox
        If mContinuousPages Then
            FindPictureBox = FindControl(Panel1, controlName)
        Else
            FindPictureBox = FindControl(Panel1, "SinglePicBox")
        End If
    End Function

    Public Function FindControl(ByVal container As Control, ByVal name As String) As Control
        If container.Name = name Then
            Return container
        End If

        For Each ctrl As Control In container.Controls
            Dim foundCtrl As Control = FindControl(ctrl, name)
            If foundCtrl IsNot Nothing Then
                Return foundCtrl
            End If
        Next
        Return Nothing
    End Function

    Private Sub ClearAllPictureBoxes(ByVal StartingPageNumber As Integer, ByVal EndingPageNumber As Integer)
        Dim PagesToKeep As New List(Of String)
        PagesToKeep.Add("0")
        For i As Integer = StartingPageNumber To EndingPageNumber
            PagesToKeep.Add(i.ToString)
        Next
        For Each oControl As Control In Panel1.Controls
            For Each childControl As Control In oControl.Controls
                If TypeOf childControl Is PictureBox And Not PagesToKeep.Contains(childControl.Name) Then
                    CType(childControl, PictureBox).Image = Nothing
                End If
            Next
        Next
        GC.Collect()
    End Sub

    Private Sub ApplyToAllPictureBoxes(ByRef oControl As Control, ByVal Mode As ViewMode)
        Dim dummyPictureBox As New PictureBox
        Dim optimalDPI As Integer = 0
        If ImageUtil.IsPDF(mPDFFileName) Then
            If mUseXPDF Then
                optimalDPI = AFPDFLibUtil.GetOptimalDPI(mPDFDoc, dummyPictureBox)
            End If
        End If

        'Dim oPage As PDFPage = mPDFDoc.Pages(mCurrentPageNumber)
        ''MsgBox(oPage.Rotation)
        'Dim backbuffer As System.Drawing.Bitmap
        'Dim Rotation As Integer = oPage.Rotation
        'If Rotation = 270 Or Rotation = 90 Then
        '    dummyPictureBox.Width = oPage.Height
        '    dummyPictureBox.Height = oPage.Width
        'Else
        '    dummyPictureBox.Width = oPage.Width
        '    dummyPictureBox.Height = oPage.Height
        'End If


        If Trim$(mPDFFileName) = "" Then
            dummyPictureBox.Image = GetImageFromFile(Arquivo, mCurrentPageNumber - 1, optimalDPI)
        Else
            dummyPictureBox.Image = GetImageFromFile(mPDFFileName, mCurrentPageNumber - 1, optimalDPI)
        End If

        For Each childControl In oControl.Controls
            If TypeOf childControl Is PictureBox Then
                If Mode = ViewMode.FIT_TO_SCREEN Then
                    childControl.Height = childControl.Parent.ClientSize.Height - 14
                    childControl.Width = childControl.Parent.ClientSize.Width - 14
                ElseIf Mode = ViewMode.FIT_WIDTH And Not Nothing Is dummyPictureBox.Image Then
                    childControl.Width = childControl.Parent.ClientSize.Width - 18
                    Dim ScaleAmount As Double = (childControl.Width / dummyPictureBox.Image.Width)
                    childControl.Height = CInt(dummyPictureBox.Image.Height * ScaleAmount)
                ElseIf Mode = ViewMode.ACTUAL_SIZE And Not Nothing Is dummyPictureBox.Image Then
                    childControl.Width = dummyPictureBox.Image.Width
                    childControl.Height = dummyPictureBox.Image.Height
                End If
            End If
        Next
        GC.Collect()
    End Sub

    Private Function FindFlowLayoutPanel() As FlowLayoutPanel
        FindFlowLayoutPanel = Nothing
        For Each oControl In Panel1.Controls
            If TypeOf oControl Is FlowLayoutPanel Then
                Return oControl
            End If
        Next
    End Function

    Private Sub FitToScreen(Optional ByVal drawPage As Boolean = False)
        If mContinuousPages Then
            InitializePageView(ViewMode.FIT_TO_SCREEN)
            DisplayCurrentPage()
        Else
            ApplyZoom(ViewMode.FIT_TO_SCREEN)
            If drawPage Then
                DisplayCurrentPage()
            End If
        End If

        UpdatePageLabel()
    End Sub

    Private Function TrapKey(ByVal KCode As String) As Boolean
        If (KCode >= 48 And KCode <= 57) Or KCode = 8 Then
            TrapKey = False
        Else
            TrapKey = True
        End If
    End Function

#End Region

#Region "XPDF specific events"

    Private Sub AFPDFLib_NodeMouseClick(ByVal sender As Object, ByVal e As TreeNodeMouseClickEventArgs)
        Dim ol As OutlineItem = DirectCast(e.Node.Tag, OutlineItem)
        If ol IsNot Nothing Then
            Dim ret As Long = ol.DoAction()
            Select Case ol.GetKind()
                Case LinkActionKind.actionGoTo, LinkActionKind.actionGoToR
                    If Not mContinuousPages Then
                        ScrolltoTop(CInt(ret))
                    End If
                    Exit Select
                Case LinkActionKind.actionLaunch
                    Exit Select
                Case LinkActionKind.actionMovie
                    Exit Select
                Case LinkActionKind.actionURI
                    Exit Select
            End Select
            mCurrentPageNumber = mPDFDoc.CurrentPage
            DisplayCurrentPage()
        End If
    End Sub

    Private Sub ScrolltoTop(ByVal y As Integer)
        Dim dr As Point = Panel1.AutoScrollPosition
        If mPDFDoc.PageHeight > Panel1.Height Then
            dr.Y = y '* (GetCurrentScalePercentage() / 100)
        End If
        Panel1.AutoScrollPosition = dr
    End Sub

    Private Sub btSearch_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btSearch.Click
        Dim res As Integer = 0
        Dim searchArgs As New SearchArgs(tbSearchText.Text, True, False, True, False, False)
        res = SearchCallBack(sender, searchArgs)
    End Sub

    Public Sub btNext_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btNext.Click
        Dim res As Integer = 0
        res = SearchCallBack(sender, New SearchArgs(tbSearchText.Text, FromBeginning, False, True, True, False))
        FromBeginning = False
        If res = 0 Then
            If MessageBox.Show("Resultado não encontrato. Deseja reiniciar do início?", Text, MessageBoxButtons.YesNo, MessageBoxIcon.Question) = DialogResult.Yes Then
                FromBeginning = True
                btNext_Click(Nothing, Nothing)
            End If
        End If
    End Sub

    Private Function SearchCallBack(ByVal sender As Object, ByVal e As SearchArgs) As Integer
        Cursor.Current = Cursors.WaitCursor
        Dim lFound As Integer = 0
        If mPDFDoc IsNot Nothing Then
            mPDFDoc.SearchCaseSensitive = e.Exact

            If e.FromBegin Then
                lFound = mPDFDoc.FindFirst(e.Text, If(e.WholeDoc, PDFSearchOrder.PDFSearchFromdBegin, PDFSearchOrder.PDFSearchFromCurrent), e.Up, False)
            ElseIf e.FindNext Then

                If e.Up Then
                    lFound = mPDFDoc.FindPrevious(e.Text)
                Else
                    lFound = mPDFDoc.FindNext(e.Text)

                End If
            Else

                lFound = mPDFDoc.FindText(e.Text, mPDFDoc.CurrentPage, (If(e.WholeDoc, PDFSearchOrder.PDFSearchFromdBegin, PDFSearchOrder.PDFSearchFromCurrent)), e.Exact, e.Up, True,
         e.WholeDoc)
            End If
            If lFound > 0 Then

                mPDFDoc.CurrentPage = mPDFDoc.SearchResults(0).Page
                mCurrentPageNumber = mPDFDoc.CurrentPage
                DisplayCurrentPage()
                If Not mContinuousPages Then
                    FocusSearchResult(mPDFDoc.SearchResults(0))
                End If
            End If
        End If
        Return lFound
        Cursor.Current = Cursors.Default
    End Function

    Private Sub FocusSearchResult(ByVal res As PdfSearchResult)
        Dim objPanel As Panel = Panel1
        Dim objPictureBox As PictureBox = FindPictureBox("SinglePicBox")
        Dim dr As Point = objPanel.AutoScrollPosition
        Dim XPercentage As Single = objPictureBox.Width / mPDFDoc.PageWidth
        Dim YPercentage As Single = objPictureBox.Height / mPDFDoc.PageHeight
        Dim WidthOffset As Integer = (objPanel.HorizontalScroll.Maximum - (mPDFDoc.PageWidth * YPercentage)) / 2
        Dim HeightOffset As Integer = (objPanel.VerticalScroll.Maximum - objPictureBox.Height) / 2
        If (mPDFDoc.PageWidth * XPercentage) > objPanel.Width Then
            dr.X = (res.Position.Left * YPercentage)
            If WidthOffset > 1 Then
                dr.X += WidthOffset
            End If
        End If
        If (mPDFDoc.PageHeight * YPercentage) > objPanel.Height Then
            dr.Y = res.Position.Top * YPercentage
            If HeightOffset > 1 Then
                dr.Y += HeightOffset
            End If
        End If

        objPanel.AutoScrollPosition = dr
    End Sub


#End Region

#Region "ITextSharp specific events"

    Private Sub ItextSharp_NodeMouseClick(ByVal sender As Object, ByVal e As TreeNodeMouseClickEventArgs)
        Dim item As iTextOutline = CType(e.Node.Tag, iTextOutline)
        If item.Page <> "" Then
            mCurrentPageNumber = Regex.Replace(item.Page, "(^\d+).+$", "$1")
            DisplayCurrentPage()
        End If
    End Sub

#End Region

#Region "Panning the image with left mouse click held down"

    Private Sub picImage_MouseDown(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs)
        'Capture the initial point 	
        Cursor = Cursors.Hand
        m_PanStartPoint = New Point(e.X, e.Y)
        RaiseEvent DocumentMouseDown(sender, e)
    End Sub

    Private Sub picImage_MouseUp(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs)
        'Capture the initial point 	
        Cursor = Cursors.Default
        m_PanEndPoint = New Point(e.X, e.Y)
        RaiseEvent DocumentMouseUp(sender, e)
    End Sub

    Private Sub picImage_MouseMove(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs)
        'Verify Left Button is pressed while the mouse is moving	
        If e.Button = Windows.Forms.MouseButtons.Left Then
            'Here we get the change in coordinates.		
            Dim DeltaX As Integer = (m_PanStartPoint.X - e.X)
            Dim DeltaY As Integer = (m_PanStartPoint.Y - e.Y)
            'Then we set the new autoscroll position.		
            'ALWAYS pass positive integers to the panels autoScrollPosition method		
            sender.Parent.AutoScrollPosition = New Drawing.Point((DeltaX - sender.Parent.AutoScrollPosition.X), (DeltaY - sender.Parent.AutoScrollPosition.Y))
        End If
    End Sub

    'Private Sub MouseWheel(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles Me.MouseWheel
    '  Dim iClicks As Integer = e.Delta
    '  If iClicks > 0 Then
    '    ImageUtil.PictureBoxZoomOut(PictureBox1)
    '  Else
    '    ImageUtil.PictureBoxZoomIn(PictureBox1)
    '  End If
    '  ShowCurrentScalePercentage()
    'End Sub

#End Region


    Private Sub BtnMoveLast_Click(sender As Object, e As EventArgs) Handles BtnMoveLast.Click
        mLastPageNumber = mCurrentPageNumber
        mCurrentPageNumber = mPDFPageCount
        DisplayCurrentPage()
    End Sub

    Private Sub BtnMoveFirst_Click(sender As Object, e As EventArgs) Handles BtnMoveFirst.Click
        mLastPageNumber = mCurrentPageNumber
        mCurrentPageNumber = 1
        DisplayCurrentPage()
    End Sub

    Private Sub Panel1_Paint(sender As Object, e As PaintEventArgs)

    End Sub

    Private Sub Panel1_Paint_1(sender As Object, e As PaintEventArgs) Handles Panel1.Paint

    End Sub

    Private Sub Panel1_MouseMove(sender As Object, e As MouseEventArgs) Handles Panel1.MouseMove


    End Sub

    Private Sub OpenFileDialog1_FileOk(sender As Object, e As CancelEventArgs) Handles OpenFileDialog1.FileOk

    End Sub

    Private Sub objPictureBox_MouseMove(sender As Object, e As MouseEventArgs) Handles objPictureBox.MouseMove




    End Sub

    Private Sub objPictureBox_MouseClick(sender As Object, e As MouseEventArgs) Handles objPictureBox.MouseClick
        Select Case _TipoSelo
            Case eTipoSelo.Selo
                PctSelo.BackgroundImage = My.Resources.selo
                PctSelo.Height = 55
            Case eTipoSelo.SeloCargo
                PctSelo.BackgroundImage = My.Resources.seloCargo
                PctSelo.Height = 68
            Case eTipoSelo.SeloCREA
                PctSelo.BackgroundImage = My.Resources.seloCargoCREA
                PctSelo.Height = 68
            Case eTipoSelo.SeloCRM
                PctSelo.BackgroundImage = My.Resources.seloCRM
                PctSelo.Height = 68
            Case eTipoSelo.SEloCargoCREA
                PctSelo.BackgroundImage = My.Resources.seloCargoCREA
                PctSelo.Height = 85
            Case eTipoSelo.SeloCargoCRM
                PctSelo.BackgroundImage = My.Resources.seloCargoCRM
                PctSelo.Height = 85
            Case eTipoSelo.SeloCertifico
                PctSelo.BackgroundImage = My.Resources.seloCertifico
                PctSelo.Height = 85
            Case eTipoSelo.Buena
                PctSelo.BackgroundImage = My.Resources.CarimboBuena
                PctSelo.Height = 85
            Case eTipoSelo.Caetite
                PctSelo.BackgroundImage = My.Resources.CarimboCaetite
                PctSelo.Height = 85
            Case eTipoSelo.Caldas
                PctSelo.BackgroundImage = My.Resources.CarimboCaldas
                PctSelo.Height = 85
            Case eTipoSelo.Fortaleza
                PctSelo.BackgroundImage = My.Resources.CarimboFortaleza
                PctSelo.Height = 85
            Case eTipoSelo.Resende
                PctSelo.BackgroundImage = My.Resources.CarimboResende
                PctSelo.Height = 85
            Case eTipoSelo.RioDeJaneiro
                PctSelo.BackgroundImage = My.Resources.CarimboRJ
                PctSelo.Height = 85
            Case eTipoSelo.SaoPaulo
                PctSelo.BackgroundImage = My.Resources.CarimboSP
                PctSelo.Height = 85
            Case eTipoSelo.ConferidoOriginal
                PctSelo.BackgroundImage = My.Resources.ConferidoOriginal
                PctSelo.Height = 85
            Case eTipoSelo.ChancelaJuridica
                PctSelo.BackgroundImage = My.Resources.SeloChancela
                PctSelo.Height = 85
            Case Else
                PctSelo.BackgroundImage = My.Resources.selo
                PctSelo.Height = 55
        End Select

        PctSelo.Visible = True
        PctSelo.Parent = objPictureBox
        PctSelo.Location = e.Location
        Dim escala As Double = GetCurrentScalePercentage() / 100

        Dim oPage As PDFPage = mPDFDoc.Pages(tsPageNum.Text)
        Dim Rotation As Integer = oPage.Rotation
        RaiseEvent PosicaoSelo(e.Location, tsPageNum.Text, objPictureBox.Width, objPictureBox.Height, escala, Rotation)

    End Sub

    Private Sub CmdCancelar_Click(sender As Object, e As EventArgs) Handles CmdCancelar.Click
        AssinaturaConfirmada = False
        Me.Close()
    End Sub

    Private Sub CmdConfirmar_Click(sender As Object, e As EventArgs) Handles CmdConfirmar.Click
        AssinaturaConfirmada = True
        Me.Close()
    End Sub

    Private Sub FrmPreview_FormClosed(sender As Object, e As FormClosedEventArgs) Handles MyBase.FormClosed
        Try

        Catch ex As Exception

        End Try
    End Sub

    Private Sub FrmPreview_Activated(sender As Object, e As EventArgs) Handles Me.Activated
        Me.BringToFront()
        Me.TopMost = True
    End Sub
End Class