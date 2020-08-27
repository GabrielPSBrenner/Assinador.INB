<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class FrmPreview
    Inherits System.Windows.Forms.Form

    'Form overrides dispose to clean up the component list.
    <System.Diagnostics.DebuggerNonUserCode()>
    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        Try
            If disposing AndAlso components IsNot Nothing Then
                components.Dispose()
            End If
        Finally
            MyBase.Dispose(disposing)
        End Try
    End Sub

    'Required by the Windows Form Designer
    Private components As System.ComponentModel.IContainer

    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.  
    'Do not modify it using the code editor.
    <System.Diagnostics.DebuggerStepThrough()>
    Private Sub InitializeComponent()
        Me.components = New System.ComponentModel.Container()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(FrmPreview))
        Me.OpenFileDialog1 = New System.Windows.Forms.OpenFileDialog()
        Me.SaveFileDialog1 = New System.Windows.Forms.SaveFileDialog()
        Me.Timer1 = New System.Windows.Forms.Timer(Me.components)
        Me.ToolStrip1 = New System.Windows.Forms.ToolStrip()
        Me.tsPageLabel = New System.Windows.Forms.ToolStripLabel()
        Me.ToolStripSeparator1 = New System.Windows.Forms.ToolStripSeparator()
        Me.BtnMoveFirst = New System.Windows.Forms.ToolStripButton()
        Me.tsPrevious = New System.Windows.Forms.ToolStripButton()
        Me.tsNext = New System.Windows.Forms.ToolStripButton()
        Me.BtnMoveLast = New System.Windows.Forms.ToolStripButton()
        Me.ToolStripSeparator2 = New System.Windows.Forms.ToolStripSeparator()
        Me.ToolStripLabel2 = New System.Windows.Forms.ToolStripLabel()
        Me.tsPageNum = New System.Windows.Forms.ToolStripTextBox()
        Me.ToolStripSeparator3 = New System.Windows.Forms.ToolStripSeparator()
        Me.ToolStripLabel3 = New System.Windows.Forms.ToolStripLabel()
        Me.Panel1 = New System.Windows.Forms.Panel()
        Me.PctSelo = New System.Windows.Forms.PictureBox()
        Me.tbSearchText = New System.Windows.Forms.ToolStripTextBox()
        Me.btSearch = New System.Windows.Forms.ToolStripButton()
        Me.btNext = New System.Windows.Forms.ToolStripButton()
        Me.ToolStripSeparator5 = New System.Windows.Forms.ToolStripSeparator()
        Me.CmdCancelar = New System.Windows.Forms.ToolStripButton()
        Me.CmdConfirmar = New System.Windows.Forms.ToolStripButton()
        Me.tsBottom = New System.Windows.Forms.ToolStrip()
        Me.ToolStrip1.SuspendLayout()
        Me.Panel1.SuspendLayout()
        CType(Me.PctSelo, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.tsBottom.SuspendLayout()
        Me.SuspendLayout()
        '
        'OpenFileDialog1
        '
        Me.OpenFileDialog1.FileName = "OpenFileDialog1"
        '
        'Timer1
        '
        Me.Timer1.Interval = 250
        '
        'ToolStrip1
        '
        Me.ToolStrip1.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.tsPageLabel, Me.ToolStripSeparator1, Me.BtnMoveFirst, Me.tsPrevious, Me.tsNext, Me.BtnMoveLast, Me.ToolStripSeparator2, Me.ToolStripLabel2, Me.tsPageNum, Me.ToolStripSeparator3, Me.ToolStripLabel3})
        Me.ToolStrip1.Location = New System.Drawing.Point(0, 0)
        Me.ToolStrip1.Name = "ToolStrip1"
        Me.ToolStrip1.Size = New System.Drawing.Size(866, 25)
        Me.ToolStrip1.TabIndex = 8
        Me.ToolStrip1.Text = "ToolStrip1"
        '
        'tsPageLabel
        '
        Me.tsPageLabel.Name = "tsPageLabel"
        Me.tsPageLabel.Size = New System.Drawing.Size(80, 22)
        Me.tsPageLabel.Text = "Página: 1 de 1"
        '
        'ToolStripSeparator1
        '
        Me.ToolStripSeparator1.Name = "ToolStripSeparator1"
        Me.ToolStripSeparator1.Size = New System.Drawing.Size(6, 25)
        '
        'BtnMoveFirst
        '
        Me.BtnMoveFirst.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text
        Me.BtnMoveFirst.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.BtnMoveFirst.Name = "BtnMoveFirst"
        Me.BtnMoveFirst.Size = New System.Drawing.Size(27, 22)
        Me.BtnMoveFirst.Text = "<<"
        '
        'tsPrevious
        '
        Me.tsPrevious.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text
        Me.tsPrevious.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.tsPrevious.Name = "tsPrevious"
        Me.tsPrevious.Size = New System.Drawing.Size(23, 22)
        Me.tsPrevious.Text = "<"
        Me.tsPrevious.ToolTipText = "Página anterior"
        '
        'tsNext
        '
        Me.tsNext.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text
        Me.tsNext.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.tsNext.Name = "tsNext"
        Me.tsNext.Size = New System.Drawing.Size(23, 22)
        Me.tsNext.Text = ">"
        Me.tsNext.ToolTipText = "Próxima página"
        '
        'BtnMoveLast
        '
        Me.BtnMoveLast.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text
        Me.BtnMoveLast.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.BtnMoveLast.Name = "BtnMoveLast"
        Me.BtnMoveLast.Size = New System.Drawing.Size(27, 22)
        Me.BtnMoveLast.Text = ">>"
        '
        'ToolStripSeparator2
        '
        Me.ToolStripSeparator2.Name = "ToolStripSeparator2"
        Me.ToolStripSeparator2.Size = New System.Drawing.Size(6, 25)
        '
        'ToolStripLabel2
        '
        Me.ToolStripLabel2.Name = "ToolStripLabel2"
        Me.ToolStripLabel2.Size = New System.Drawing.Size(93, 22)
        Me.ToolStripLabel2.Text = "Vá para a Página"
        '
        'tsPageNum
        '
        Me.tsPageNum.Name = "tsPageNum"
        Me.tsPageNum.Size = New System.Drawing.Size(40, 25)
        '
        'ToolStripSeparator3
        '
        Me.ToolStripSeparator3.Name = "ToolStripSeparator3"
        Me.ToolStripSeparator3.Size = New System.Drawing.Size(6, 25)
        '
        'ToolStripLabel3
        '
        Me.ToolStripLabel3.Name = "ToolStripLabel3"
        Me.ToolStripLabel3.Size = New System.Drawing.Size(10, 22)
        Me.ToolStripLabel3.Text = " "
        '
        'Panel1
        '
        Me.Panel1.AutoScroll = True
        Me.Panel1.Controls.Add(Me.PctSelo)
        Me.Panel1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Panel1.Location = New System.Drawing.Point(0, 25)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Size = New System.Drawing.Size(866, 626)
        Me.Panel1.TabIndex = 11
        '
        'PctSelo
        '
        Me.PctSelo.BackgroundImage = Global.INB.PDF.My.Resources.Resources.Selo1
        Me.PctSelo.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.PctSelo.Location = New System.Drawing.Point(102, 29)
        Me.PctSelo.Name = "PctSelo"
        Me.PctSelo.Size = New System.Drawing.Size(155, 55)
        Me.PctSelo.TabIndex = 0
        Me.PctSelo.TabStop = False
        Me.PctSelo.Visible = False
        '
        'tbSearchText
        '
        Me.tbSearchText.Name = "tbSearchText"
        Me.tbSearchText.Size = New System.Drawing.Size(100, 25)
        '
        'btSearch
        '
        Me.btSearch.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
        Me.btSearch.Image = CType(resources.GetObject("btSearch.Image"), System.Drawing.Image)
        Me.btSearch.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.btSearch.Name = "btSearch"
        Me.btSearch.Size = New System.Drawing.Size(23, 22)
        Me.btSearch.Text = "Localizar"
        '
        'btNext
        '
        Me.btNext.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
        Me.btNext.Image = CType(resources.GetObject("btNext.Image"), System.Drawing.Image)
        Me.btNext.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.btNext.Name = "btNext"
        Me.btNext.Size = New System.Drawing.Size(23, 22)
        Me.btNext.Text = "Localizar próxima"
        '
        'ToolStripSeparator5
        '
        Me.ToolStripSeparator5.Name = "ToolStripSeparator5"
        Me.ToolStripSeparator5.Size = New System.Drawing.Size(6, 25)
        '
        'CmdCancelar
        '
        Me.CmdCancelar.Image = Global.INB.PDF.My.Resources.Resources.escritorio_3237_close64
        Me.CmdCancelar.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.CmdCancelar.Name = "CmdCancelar"
        Me.CmdCancelar.Size = New System.Drawing.Size(126, 22)
        Me.CmdCancelar.Text = "&Fechar sem assinar"
        '
        'CmdConfirmar
        '
        Me.CmdConfirmar.Image = Global.INB.PDF.My.Resources.Resources.assinatura
        Me.CmdConfirmar.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.CmdConfirmar.Name = "CmdConfirmar"
        Me.CmdConfirmar.Size = New System.Drawing.Size(139, 22)
        Me.CmdConfirmar.Text = "&Confirmar Assinatura"
        '
        'tsBottom
        '
        Me.tsBottom.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.tsBottom.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.tbSearchText, Me.btSearch, Me.btNext, Me.ToolStripSeparator5, Me.CmdCancelar, Me.CmdConfirmar})
        Me.tsBottom.Location = New System.Drawing.Point(0, 651)
        Me.tsBottom.Name = "tsBottom"
        Me.tsBottom.ShowItemToolTips = False
        Me.tsBottom.Size = New System.Drawing.Size(866, 25)
        Me.tsBottom.TabIndex = 9
        Me.tsBottom.Text = "Export PDF to another file format"
        '
        'FrmPreview
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(866, 676)
        Me.ControlBox = False
        Me.Controls.Add(Me.Panel1)
        Me.Controls.Add(Me.ToolStrip1)
        Me.Controls.Add(Me.tsBottom)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow
        Me.KeyPreview = True
        Me.Name = "FrmPreview"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Assinar PDF"
        Me.TopMost = True
        Me.ToolStrip1.ResumeLayout(False)
        Me.ToolStrip1.PerformLayout()
        Me.Panel1.ResumeLayout(False)
        CType(Me.PctSelo, System.ComponentModel.ISupportInitialize).EndInit()
        Me.tsBottom.ResumeLayout(False)
        Me.tsBottom.PerformLayout()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Friend WithEvents OpenFileDialog1 As OpenFileDialog
    Friend WithEvents SaveFileDialog1 As SaveFileDialog
    Friend WithEvents Timer1 As Timer
    Friend WithEvents ToolStrip1 As ToolStrip
    Friend WithEvents tsPageLabel As ToolStripLabel
    Friend WithEvents ToolStripSeparator1 As ToolStripSeparator
    Friend WithEvents BtnMoveFirst As ToolStripButton
    Public WithEvents tsPrevious As ToolStripButton
    Public WithEvents tsNext As ToolStripButton
    Public WithEvents BtnMoveLast As ToolStripButton
    Friend WithEvents ToolStripSeparator2 As ToolStripSeparator
    Friend WithEvents ToolStripLabel2 As ToolStripLabel
    Friend WithEvents tsPageNum As ToolStripTextBox
    Friend WithEvents ToolStripSeparator3 As ToolStripSeparator
    Friend WithEvents ToolStripLabel3 As ToolStripLabel
    Friend WithEvents Panel1 As Panel
    Friend WithEvents PctSelo As PictureBox
    Friend WithEvents tbSearchText As ToolStripTextBox
    Friend WithEvents btSearch As ToolStripButton
    Friend WithEvents btNext As ToolStripButton
    Friend WithEvents ToolStripSeparator5 As ToolStripSeparator
    Friend WithEvents CmdCancelar As ToolStripButton
    Friend WithEvents CmdConfirmar As ToolStripButton
    Friend WithEvents tsBottom As ToolStrip
End Class
