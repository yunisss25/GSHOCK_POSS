<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class MANAGER
    Inherits System.Windows.Forms.Form

    'Form overrides dispose to clean up the component list.
    <System.Diagnostics.DebuggerNonUserCode()> _
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
    <System.Diagnostics.DebuggerStepThrough()> _
    Private Sub InitializeComponent()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(MANAGER))
        Me.txtId = New System.Windows.Forms.TextBox()
        Me.txtProductName = New System.Windows.Forms.TextBox()
        Me.txtPrice = New System.Windows.Forms.TextBox()
        Me.txtQuantity = New System.Windows.Forms.TextBox()
        Me.btnSelectImage = New System.Windows.Forms.Button()
        Me.btnInsertProduct = New System.Windows.Forms.Button()
        Me.cmbSeries = New System.Windows.Forms.ComboBox()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.picProductImage = New System.Windows.Forms.PictureBox()
        CType(Me.picProductImage, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'txtId
        '
        Me.txtId.Location = New System.Drawing.Point(101, 105)
        Me.txtId.Name = "txtId"
        Me.txtId.Size = New System.Drawing.Size(100, 20)
        Me.txtId.TabIndex = 0
        '
        'txtProductName
        '
        Me.txtProductName.Location = New System.Drawing.Point(101, 140)
        Me.txtProductName.Name = "txtProductName"
        Me.txtProductName.Size = New System.Drawing.Size(100, 20)
        Me.txtProductName.TabIndex = 1
        '
        'txtPrice
        '
        Me.txtPrice.Location = New System.Drawing.Point(101, 202)
        Me.txtPrice.Name = "txtPrice"
        Me.txtPrice.Size = New System.Drawing.Size(100, 20)
        Me.txtPrice.TabIndex = 2
        '
        'txtQuantity
        '
        Me.txtQuantity.Location = New System.Drawing.Point(101, 237)
        Me.txtQuantity.Name = "txtQuantity"
        Me.txtQuantity.Size = New System.Drawing.Size(100, 20)
        Me.txtQuantity.TabIndex = 3
        '
        'btnSelectImage
        '
        Me.btnSelectImage.Location = New System.Drawing.Point(126, 263)
        Me.btnSelectImage.Name = "btnSelectImage"
        Me.btnSelectImage.Size = New System.Drawing.Size(75, 23)
        Me.btnSelectImage.TabIndex = 4
        Me.btnSelectImage.Text = "Image"
        Me.btnSelectImage.UseVisualStyleBackColor = True
        '
        'btnInsertProduct
        '
        Me.btnInsertProduct.Location = New System.Drawing.Point(126, 301)
        Me.btnInsertProduct.Name = "btnInsertProduct"
        Me.btnInsertProduct.Size = New System.Drawing.Size(75, 23)
        Me.btnInsertProduct.TabIndex = 5
        Me.btnInsertProduct.Text = "Insert"
        Me.btnInsertProduct.UseVisualStyleBackColor = True
        '
        'cmbSeries
        '
        Me.cmbSeries.FormattingEnabled = True
        Me.cmbSeries.Location = New System.Drawing.Point(79, 169)
        Me.cmbSeries.Name = "cmbSeries"
        Me.cmbSeries.Size = New System.Drawing.Size(121, 21)
        Me.cmbSeries.TabIndex = 6
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(236, 108)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(18, 13)
        Me.Label1.TabIndex = 7
        Me.Label1.Text = "ID"
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(236, 147)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(72, 13)
        Me.Label2.TabIndex = 8
        Me.Label2.Text = "ProductName"
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Location = New System.Drawing.Point(236, 177)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(36, 13)
        Me.Label3.TabIndex = 9
        Me.Label3.Text = "Series"
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.Location = New System.Drawing.Point(236, 209)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(31, 13)
        Me.Label4.TabIndex = 10
        Me.Label4.Text = "Price"
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.Location = New System.Drawing.Point(236, 244)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(46, 13)
        Me.Label5.TabIndex = 11
        Me.Label5.Text = "Quantity"
        '
        'picProductImage
        '
        Me.picProductImage.Location = New System.Drawing.Point(390, 105)
        Me.picProductImage.Name = "picProductImage"
        Me.picProductImage.Size = New System.Drawing.Size(217, 188)
        Me.picProductImage.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage
        Me.picProductImage.TabIndex = 12
        Me.picProductImage.TabStop = False
        '
        'MANAGER
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackgroundImage = CType(resources.GetObject("$this.BackgroundImage"), System.Drawing.Image)
        Me.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.ClientSize = New System.Drawing.Size(800, 450)
        Me.Controls.Add(Me.picProductImage)
        Me.Controls.Add(Me.Label5)
        Me.Controls.Add(Me.Label4)
        Me.Controls.Add(Me.Label3)
        Me.Controls.Add(Me.Label2)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.cmbSeries)
        Me.Controls.Add(Me.btnInsertProduct)
        Me.Controls.Add(Me.btnSelectImage)
        Me.Controls.Add(Me.txtQuantity)
        Me.Controls.Add(Me.txtPrice)
        Me.Controls.Add(Me.txtProductName)
        Me.Controls.Add(Me.txtId)
        Me.Name = "MANAGER"
        Me.Text = "MANAGER"
        CType(Me.picProductImage, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Friend WithEvents txtId As TextBox
    Friend WithEvents txtProductName As TextBox
    Friend WithEvents txtPrice As TextBox
    Friend WithEvents txtQuantity As TextBox
    Friend WithEvents btnSelectImage As Button
    Friend WithEvents btnInsertProduct As Button
    Friend WithEvents cmbSeries As ComboBox
    Friend WithEvents Label1 As Label
    Friend WithEvents Label2 As Label
    Friend WithEvents Label3 As Label
    Friend WithEvents Label4 As Label
    Friend WithEvents Label5 As Label
    Friend WithEvents picProductImage As PictureBox
End Class
